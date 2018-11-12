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
        public SendStore Store { get; }

        public MessageStore _MessageStore { get; }

        public UserStore _UserStore { get; }

        public SendManager(SendStore store, MessageStore MessageStore, UserStore UserStore)
        {
            Store = store;
            _MessageStore = MessageStore;
            _UserStore = UserStore;
        }



        /// <summary>
        /// 发送消息，验证发送体，不验证登陆用户（Controller验证）
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public async Task SendMessageAsync([Required]ResponseMessage response, [Required]SendMsgRequest request)
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
            if (request.Send.FromUserId==0 || request.Send.ToUserId == 0)
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
            User fromUser = await _UserStore.ReadAsync(a => a.Where(b => b.Id == request.Send.FromUserId), CancellationToken.None);
            if (fromUser == null) Define.Response.UserNotFound(response, request.Send.FromUserId);
            User toUser = await _UserStore.ReadAsync(a => a.Where(b => b.Id == request.Send.ToUserId), CancellationToken.None);
            if (toUser == null) Define.Response.UserNotFound(response, request.Send.ToUserId);
            // 消息不可重复：在数据库中比较与上一条消息的发送时间是否一致

            //var fromUser = _UserStore.ReadAsync(a=>a.Where(b=>b.Id==request.Send.FromUserId))
            //var send = new Send(request.Send);
        }
    }
}
