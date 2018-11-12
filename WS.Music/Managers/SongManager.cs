﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Music.Dto;
using WS.Music.Stores;

namespace WS.Music.Managers
{
    /// <summary>
    /// 音乐管理，需要一个超强的Merge操作
    /// </summary>
    public class SongManager
    {
        public SongStore Store { get; set; }

        public SongManager(SongStore songStore)
        {
            Store = songStore;
        }

        /// <summary>
        /// 歌曲信息录入(OK)|更新(OK)必须要歌曲ID，该方法会根据是否有ID来判断是创建还是删除
        /// TODO：如果后面录入了一个与以前创建的歌曲一致的歌曲，如何将两者的信息合并，建议的方法调用 Merge(long extSongId, long newSongId)//合并两首歌曲 MergeIfName(SongInfo songinfo)//录入时如果有重名的就覆盖掉原有的
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public async Task SongInfoEntryAsync([Required]ResponseMessage response, [Required]SongInfoEntryRequest request)
        {
            // 权限检查在Controller中验证
            // 参数检查，空检查和存在检查（操作数据是否存在于数据库）
            var songId = await Store.ReadAsync(a => a.Where(b => b.Id == request.SongInfo.Id).Select(c=>c.Id), CancellationToken.None);
            // 如果没有被创建
            if (songId == 0)
            {
                // 数据创建
                await Store.CreateAsync(new Models.Song
                {
                    Name = request.SongInfo.Name,
                    Description = request.SongInfo.Description,
                    ReleaseTime = request.SongInfo.ReleaseTime,
                    Duration = request.SongInfo.Duration ?? 0,
                    _CreateUserId = request.UserId
                }, CancellationToken.None);
                response.Message += "\r\n" + ResponseDefine.CreatedMsg + "\r\n" + Define.Song.CreatedMsg;
            }
            else
            {
                // 数据更新
                await Store.UpdateAsync(new Models.Song
                {
                    Id = songId,
                    Name = request.SongInfo.Name,
                    Description = request.SongInfo.Description,
                    ReleaseTime = request.SongInfo.ReleaseTime,
                    Duration = request.SongInfo.Duration ?? 0,
                    _UpdateUserId = request.UserId
                }, CancellationToken.None);
                response.Message += "\r\n" + Define.Song.UpdatedMsg;
            }
        }
    }
}
