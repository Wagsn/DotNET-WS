using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Music.Models;

namespace WS.Music.Dto
{
    public class UserJson
    {
        /// <summary>
        /// ID，主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 昵称，主键
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 介绍，可空（Empty=Blank>Null）
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 性别，可空（Null：未知，true：男，false：女）
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// 生日，可空
        /// </summary>
        public DateTime? BirthTime { get; set; }

        /// <summary>
        /// 更新数据：从用户信息那里
        /// </summary>
        /// <param name="user">用户信息</param>
        public void _Update(UserJson user)
        {
            Id = user.Id;
            Name = user.Name ?? Name;
            Description = user.Description ?? Description;
            Sex = user.Sex ?? Sex;
            BirthTime = user.BirthTime ?? BirthTime;
        }

        /// <summary>
        /// 更新数据：从用户实体那里
        /// </summary>
        /// <param name="user">用户实体</param>
        public void _Update(User user)
        {
            Id = user.Id;
            Name = user.Name ?? Name;
            Description = user.Description ?? Description;
            Sex = user.Sex ?? Sex;
            BirthTime = user.BirthTime ?? BirthTime;
        }
    }
}
