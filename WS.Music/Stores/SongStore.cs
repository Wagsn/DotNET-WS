﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Stores;
using WS.Music.Models;

namespace WS.Music.Stores
{
    /// <summary>
    /// 歌曲存储，写一些特殊的数据库存取操作
    /// </summary>
    public class SongStore : StoreBase<ApplicationDbContext, Song>
    {
        public ArtistStore _ArtistStore { get; }

        /// <summary>
        /// 歌曲存储
        /// </summary>
        /// <param name="ArtistStore"></param>
        /// <param name="context"></param>
        public SongStore(ArtistStore ArtistStore, ApplicationDbContext context) : base(context)
        {
            _ArtistStore = ArtistStore;
        }

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<Song>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            CheckNull(query);

            return query.Invoke(Context.Songs.Where(i => i._IsDeleted == false).AsNoTracking()).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 通过所有有关联的名称查询Song，模糊查询，效率较低
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Song> ByAllName(string name)
        {
            // TODO 优化查询速度
            var query = from s in Context.Songs
                        where (s.Name.Contains(name)
                        || (from rsoar in Context.RelSongArtists
                            where (from a in Context.Artists
                                   where a.Name.Contains(name)
                                   select a.Id).Contains(rsoar.ArtistId)
                            select rsoar.SongId).Contains(s.Id)
                            || (from rsoal in Context.RelSongAlbums
                                where (from a in Context.Albums
                                       where a.Name.Contains(name)
                                       select a.Id).Contains(rsoal.AlbumId)
                                select rsoal.SongId).Contains(s.Id))
                        select new Song(s);
            return query;
        }

        /// <summary>
        /// 找到Song通过歌名，模糊查询
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IQueryable<Song> ByName(string Name)
        {
            var query = from s in Context.Songs
                        where s.Name.Contains(Name)
                        select new Song(s);
            return query;
        }

        /// <summary>
        /// 找到Song通过艺人名，模糊查询
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IQueryable<Song> ByArtistName(string Name)
        {
            var query = from s in Context.Songs
                        where (from rsoar in Context.RelSongArtists
                               where (from a in Context.Artists
                                      where a.Name.Contains(Name)  
                                      select a.Id).Contains(rsoar.ArtistId)
                               select rsoar.SongId).Contains(s.Id)
                        select new Song(s);
            return query;
        }

        /// <summary>
        /// 通过专辑名模糊查询Song
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IQueryable<Song> ByAlbumName(string Name)
        {
            var query = from s in Context.Songs
                        where (from rsoal in Context.RelSongAlbums
                               where (from a in Context.Albums
                                      where a.Name.Contains(Name)
                                      select a.Id).Contains(rsoal.AlbumId)
                               select rsoal.SongId).Contains(s.Id)
                        select new Song(s);
            return query;
        }
    }
}
