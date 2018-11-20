using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Music.Dto;
using WS.Music.Models;
using WS.Music.Stores;

namespace WS.Music.Managers
{
    /// <summary>
    /// 发送消息管理
    /// </summary>
    public class SendManager
    {
        private readonly SendStore Store;

        private readonly MessageStore _MessageStore;

        private readonly UserStore _UserStore;

        private readonly ITransaction _transaction;

        public SendManager(SendStore store, MessageStore MessageStore, UserStore UserStore, ITransaction transaction)
        {
            Store = store;
            _MessageStore = MessageStore;
            _UserStore = UserStore;
            _transaction = transaction;
        }

        /// <summary>
        /// 发送消息，验证发送体，不验证登陆用户（Controller验证）
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SendMessageAsync([Required]ResponseMessage response, [Required]SendMsgRequest request, CancellationToken cancellationToken)
        {
            // 参数验证：发送体不为null，发送人id和接收人id存在，消息内容不能为 string.IsNullOrWhiteSpace，消息不能重复发送（客户端监控）
            // 判断请求体是否存在错误
            if (request.Send==null)
            {
                response.Wrap(ResponseDefine.BadRequset, "发送体不能为空");
                return;
            }
            if (request.Send.Msg == null)
            {
                response.Wrap(ResponseDefine.BadRequset, "消息体不能为空");
                return;
            }
            if (request.Send.FromUserId==null || request.Send.ToUserId == null)
            {
                response.Wrap(ResponseDefine.BadRequset, "发送或接收人的ID不能为0");
                return;
            }
            if (string.IsNullOrWhiteSpace(request.Send.Msg.Detail))
            {
                response.Wrap(ResponseDefine.BadRequset, "消息内容不能为空");
                return;
            }
            // 判断发信人与收信人存在
            User fromUser =  _UserStore.ReadAsync(a => a.Where(b => b.Id == request.Send.FromUserId), CancellationToken.None).Result;
            if (fromUser == null) Def.Response.UserNotFound(response, request.Send.FromUserId);
            User toUser = _UserStore.ReadAsync(a => a.Where(b => b.Id == request.Send.ToUserId), CancellationToken.None).Result;
            if (toUser == null) Def.Response.UserNotFound(response, request.Send.ToUserId);
            // 消息不可重复：在数据库中比较与上一条消息的发送时间是否一致，TODO: 另外可以比较当前时间与上一条的内容一致但是时间间隔过短

            // 改造，生成LastMessage(long )方法
            var lastSend = await Store.ReadAsync(a => a.Where(b => b.FromUserId == fromUser.Id && b.ToUserId == toUser.Id && b.FromTime == a.Max(c => c.FromTime)), CancellationToken.None);
            if (lastSend!=null && request.Send.FromTime == lastSend.FromTime)
            {
                response.Wrap(ResponseDefine.PostRepeat, "消息不可以重复发送");
            }
            else
            {
                // 使用事务刷新数据，保持数据一致性
                using (var trans = await _transaction.BeginTransaction())
                {
                    try
                    {
                        // 先保存Msg获取MsgId
                        var msg = await _MessageStore.CreateAsync(new Message
                        {
                            Detail = request.Send.Msg.Detail
                        });
                        // 再保存Send获取保存后的Send
                        if (msg.Id == null)
                        {
                            throw new Exception("WS------ 再数据库中创建Message时返回保存的Message不完整：\r\n" + msg);
                        }
                        else
                        {
                           //Thread.Sleep(5);
                            var send = await Store.CreateAsync(new Send
                            {
                                Type = request.Send.Type??"Private",
                                FromUserId = request.Send.FromUserId,
                                FromTime = request.Send.FromTime??DateTime.Now,
                                ToUserId = request.Send.ToUserId,
                                MsgId = msg.Id,
                                _CreateUserId = "1"
                            }, cancellationToken);
                        }
                        trans.Commit();
                    }
                    catch ( Exception e)
                    {
                        trans.Rollback();
                        throw new Exception("使用事务保存数据时出现异常：\r\n", e);
                    }
                }
            }
        }
    }
}
