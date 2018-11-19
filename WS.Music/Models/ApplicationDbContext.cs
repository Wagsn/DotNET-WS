using Microsoft.EntityFrameworkCore;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Models;

namespace WS.Music.Models
{
    /// <summary>
    /// 应用数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Send>(b=> 
            {
                b.ToTable("ws_music_send");
                b.Property<bool>("_IsDeleted");
            });

            builder.Entity<Artist>(b =>
            {
                b.ToTable("ws_music_artist");
                b.Property<bool>("_IstDeleted");
            });

            builder.Entity<Album>(b =>
            {
                b.ToTable("ws_music_album");
                b.Property<bool>("_IstDeleted");
            });

            builder.Entity<Message>(b =>
            {
                b.ToTable("ws_music_message");
                b.Property<bool>("_IsDeleted");
            });

            builder.Entity<Song>(b =>
            {
                b.ToTable("ws_music_song");
                b.Property<bool>("_IsDeleted");
            });

            builder.Entity<User>(b=> 
            {
                b.ToTable("ws_music_user");  // 映射到ws_music_user表中
                b.Property<bool>("_IsDeleted");  // 指明有额外列_IsDeleted
                //b.HasKey(new string[] { "Id", "Name" });  // 双主键
                //b.HasQueryFilter(a => EF.Property<bool>(User, "_IsDeleted") == false);  // 字段过滤器，过滤之后恢复删除有点麻烦，现在的情况是写在Store
            });

            builder.Entity<PlayList>(b =>
            {
                b.ToTable("ws_music_palylist");
                b.Property<bool>("_IsDeleted");
            });

            builder.Entity<RelUserPlayList>(b =>
            {
                b.ToTable("ws_music_rel_userplaylist");
                b.Property<bool>("_IsDeleted");
                b.HasKey(new string[] { "UserId", "PlayListId" });
            });

            builder.Entity<RelPlayListSong>(b =>
            {
                b.ToTable("ws_music_rel_playlistsong");
                b.Property<bool>("_IsDeleted");
                b.HasKey(new string[] { "PlayListId", "SongId" });
            });

            builder.Entity<RelSongArtist>(b =>
            {
                b.ToTable("ws_music_rel_songartist");
                b.Property<bool>("_IsDeleted");
                b.HasKey(new string[] { "SongId", "ArtistId" });
            });

            builder.Entity<RelSongAlbum>(b =>
            {
                b.ToTable("ws_music_rel_songalbum");
                b.Property<bool>("_IsDeleted");
                b.HasKey(new string[] { "SongId", "AlbumId" });
            });
        }

        /// <summary>
        /// 歌曲
        /// </summary>
        public DbSet<Song> Songs { get; set; }

        /// <summary>
        /// 艺人
        /// </summary>
        public DbSet<Artist> Artists { get; set; }

        /// <summary>
        /// 专辑
        /// </summary>
        public DbSet<Album> Albums { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 歌单
        /// </summary>
        public DbSet<PlayList> PlayLists { get; set; }

        /// <summary>
        /// 发送
        /// </summary>
        public DbSet<Send> Sends { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// 歌曲专辑关联
        /// </summary>
        public DbSet<RelSongAlbum> RelSongAlbums { get; set; }

        /// <summary>
        /// 歌曲艺人关联
        /// </summary>
        public DbSet<RelSongArtist> RelSongArtists { get; set; }

        /// <summary>
        /// 用户歌单关联
        /// </summary>
        public DbSet<RelUserPlayList> RelUserPlayLists { get; set; }

        /// <summary>
        /// 歌单歌曲关联
        /// </summary>
        public DbSet<RelPlayListSong> RelPlayListSongs { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            //OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 在保存改变之前
        /// </summary>
        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["_IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["_IsDeleted"] = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 通过泛型的方式查询目标类型的DbSet，可能会影响效率，未完成
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public virtual DbSet<TResult> Models<TResult>() where TResult : TraceUpdateBase
        {
            throw new NotSupportedException();
        }
    }
}
