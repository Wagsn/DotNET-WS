using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Stores;
using WS.Music.Models;

namespace WS.Music.Stores
{
    /// <summary>
    /// 歌单歌曲存储
    /// </summary>
    public class RelPlayListSongStore
    {
        public ApplicationDbContext Context { get; set; }

        public RelPlayListSongStore(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 通过歌单ID和歌曲ID查询歌单歌曲关联表
        /// </summary>
        /// <param name="playListId">歌单ID</param>
        /// <param name="songId">歌曲ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IQueryable<RelPlayListSong> ByDoubleId ([Required]string playListId, [Required]string songId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = from rps in Context.RelPlayListSongs
                               where !rps._IsDeleted && rps.PlayListId == playListId && rps.SongId == songId
                               select new RelPlayListSong(rps);
            return query;
        }

        /// <summary>
        /// 创建歌单与歌曲的关联
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="playListId"></param>
        /// <param name="songId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RelPlayListSong> Create([Required]string userId, [Required]string playListId, [Required]string songId, CancellationToken cancellationToken = default(CancellationToken))
        {
            // 查询是否存在
            var p = (from pls in Context.RelPlayListSongs
                    where pls.PlayListId == playListId && pls.SongId == songId
                    select new RelPlayListSong(pls)).SingleOrDefault();
            if (p != null)
            {
                // 刷新数据 删除记录
                p._Reset();
                p._CreateTime = DateTime.Now;
                p._CreateUserId = userId;
                p.PlayListId = playListId;
                p.SongId = songId;
                return p;
            }
            var playListSong = new RelPlayListSong
            {
                _CreateTime = DateTime.Now,
                _CreateUserId = userId,
                _IsDeleted = false,
                PlayListId = playListId,
                SongId = songId
            };
            Context.Add(playListSong);
            try
            {
                await Context.SaveChangesAsync(cancellationToken);  // TODO 添加日志
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception("WS------ StoreBase中保存改变时: \r\n", e);
            }
            return playListSong;
        }

        /// <summary>
        /// 软删除歌单与歌曲的关联
        /// versoin= "1.0.1"
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="playListId">歌单ID</param>
        /// <param name="songId">歌曲ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RelPlayListSong> Delete(string userId, string playListId, string songId, CancellationToken cancellationToken= default(CancellationToken))
        {
            var playListSong = (from rpls in Context.RelPlayListSongs
                                where !rpls._IsDeleted && rpls.PlayListId == playListId && rpls.SongId == songId
                                select new RelPlayListSong
                                {
                                    PlayListId = rpls.PlayListId,
                                    SongId = rpls.SongId
                                }).SingleOrDefault();
            if (playListSong == null)
            {
                return null;  // 没找到
            }
            else
            {
                // 软删除 TODO 改为用户操作表
                playListSong._DeleteTime = DateTime.Now;
                playListSong._DeleteUserId = userId;
                playListSong._IsDeleted = true;
                Context.Attach(playListSong);
                Context.Update(playListSong);
                try
                {
                    await Context.SaveChangesAsync(cancellationToken);  // TODO 添加操作日志输出
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new Exception("WS------ StoreBase中保存改变时: \r\n", e);
                }
                return playListSong;
            }
        }
    }
}
