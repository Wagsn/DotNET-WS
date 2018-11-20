using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WS.Music.Models;

namespace WS.Music.Stores
{
    /// <summary>
    /// 歌单歌曲存储
    /// </summary>
    public class RelPlayListSongStore
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public ApplicationDbContext Context { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="context"></param>
        public RelPlayListSongStore(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 通过歌单ID和歌曲ID查询歌单歌曲关联表
        /// </summary>
        /// <param name="playListId">歌单ID</param>
        /// <param name="songId">歌曲ID</param>
        /// <returns></returns>
        public IQueryable<RelPlayListSong> ById ([Required]string userId, [Required]string playListId, [Required]string songId)
        {
            // TODO 添加操作日志
            var query = from rps in Context.RelPlayListSongs
                               where rps.PlayListId == playListId && rps.SongId == songId
                               select new RelPlayListSong(rps);
            return query;
        }

        /// <summary>
        /// 根据歌单ID查找关联
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="playListId"></param>
        /// <returns></returns>
        public IQueryable<RelPlayListSong> ByPlayListId([Required]string userId, [Required]string playListId)
        {
            // TODO 添加操作日志
            var query = from rps in Context.RelPlayListSongs
                        where rps.PlayListId == playListId
                        select new RelPlayListSong(rps);
            return query;
        }

        /// <summary>
        /// 根据歌曲ID找到关联
        /// </summary>
        /// <param name="songId"></param>
        /// <returns></returns>
        public IQueryable<RelPlayListSong> BySongId([Required]string userId, [Required]string songId)
        {
            // TODO 添加操作日志
            var query = from rps in Context.RelPlayListSongs
                        where rps.SongId == songId
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
                // 添加操作日志
                return p;
            }
            var playListSong = new RelPlayListSong
            {
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
        public async Task<RelPlayListSong> Delete([Required]string userId, [Required]string playListId, [Required]string songId, CancellationToken cancellationToken= default(CancellationToken))
        {
            var playListSong = (from rpls in Context.RelPlayListSongs
                                where rpls.PlayListId == playListId && rpls.SongId == songId
                                select new RelPlayListSong
                                {
                                    PlayListId = rpls.PlayListId,
                                    SongId = rpls.SongId
                                }).SingleOrDefault();
            if (playListSong == null)
            {
                // TODO 添加操作日志输出
                return null;  // 没找到
            }
            else
            {
                // TODO 软删除-用户操作记录
                Context.Remove(playListSong);
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

        /// <summary>
        /// 删除歌单歌曲关联
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RelPlayListSong> Delete([Required]string userId, [Required]RelPlayListSong rel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Delete(userId, rel.PlayListId, rel.SongId, cancellationToken);
        }
    }
}
