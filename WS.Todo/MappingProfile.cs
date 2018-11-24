using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Todo.Dto;
using WS.Todo.Models;

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
            CreateMap<UserBase, UserJson>();
            CreateMap<UserJson, UserBase>();

            CreateMap<TodoItem, TodoItemJson>();
            CreateMap<TodoItemJson, TodoItem>();
        }
    }
}
