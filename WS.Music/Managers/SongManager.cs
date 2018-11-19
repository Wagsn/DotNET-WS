using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

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

        public IMapper _Mapper { get; set; }

        public SongManager(SongStore songStore, IMapper Mapper)
        {
            Store = songStore;
            _Mapper = Mapper;
        }

        /// <summary>
        /// 歌曲信息录入(OK)|更新(OK)必须要歌曲ID，该方法会根据是否有ID来判断是创建还是删除
        /// TODO：如果后面录入了一个与以前创建的歌曲一致的歌曲，如何将两者的信息合并，建议的方法调用 Merge(long extSongId, long newSongId)//合并两首歌曲 MergeIfName(SongInfo songinfo)//录入时如果有重名的就覆盖掉原有的
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public async Task SongEntryAsync([Required]ResponseMessage response, [Required]SongInfoEntryRequest request)
        {
            // 权限检查在Controller中验证
            // 参数检查，空检查和存在检查（操作数据是否存在于数据库）
            var songId = await Store.ReadAsync(a => a.Where(b => b.Id == request.SongInfo.Id).Select(c=>c.Id), CancellationToken.None);
            // 如果没有被创建
            if (songId == null)
            {
                // 数据创建
                await Store.CreateAsync(new Models.Song
                {
                    Name = request.SongInfo.Name,
                    Description = request.SongInfo.Description,
                    ReleaseTime = request.SongInfo.ReleaseTime,
                    Duration = request.SongInfo.Duration ?? 0,
                    _CreateUserId = request.User.Id
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
                    _UpdateUserId = request.User.Id
                }, CancellationToken.None);
                response.Message += "\r\n" + Define.Song.UpdatedMsg;
            }
        }
        /// <summary>
        /// Fuzzy search Songs by Song Name, Artist Name, Album Name, Lyric, Tag or All the Names.
        /// </summary>
        /// <param name="response">Response Message</param>
        /// <param name="request">Song Search Request</param>
        public void Serch([Required]ResponseMessage<List<SongJson>> response, [Required]SongSearchRequest request)
        {
            // Check arguments.
            if (request.Word == null)
            {
                Define.Response.Wrap(response, Define.Response.BadRequsetCode, "Keywords can not be empty!");
            }

            // Search Song by Song Name.
            if (request.Type == null || request.Type.ToLower() == "song")
            {
                response.Extension = Store.ByName(request.Word).Select(a => _Mapper.Map<SongJson>(a)).ToList();
            }
            // Search Song by Other ways.
            switch (request.Type.ToLower())
            {
                case "artist":
                    response.Extension = Store.ByArtistName(request.Word).Select(a => _Mapper.Map<SongJson>(a)).ToList();
                    break;
                case "album":
                    response.Extension = Store.ByAlbumName(request.Word).Select(a => _Mapper.Map<SongJson>(a)).ToList();
                    break;
                case "all":
                    response.Extension = Store.ByAllName(request.Word).Select(a => _Mapper.Map<SongJson>(a)).ToList();
                    break;
                case "lyric":
                default:
                    if (response.Extension == null)
                    {
                        Define.Response.Wrap(response, Define.Response.NotSupportCode, "Does not support query songs by other ways!");
                    }
                    break;
            }
        }
    }
}
