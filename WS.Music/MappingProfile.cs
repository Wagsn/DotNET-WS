using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Music.Dto;
using WS.Music.Models;

namespace WS.Music
{
    /// <summary>
    /// AutoMapper 映射文件
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 从User映射到UserJson
            CreateMap<User, UserJson>();
            CreateMap<UserJson, User>();
            CreateMap<Song, SongJson>();
        }
    }
}
