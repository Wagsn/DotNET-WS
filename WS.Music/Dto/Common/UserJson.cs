using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(36, ErrorMessage ="GUID最大不能超过36个字符")]
        public string Id { get; set; }

        /// <summary>
        /// 昵称，主键
        /// </summary>
        [MaxLength(31)]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(63)]
        public string Pwd { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(320)]
        public string Email { get; set; }

        /// <summary>
        /// 介绍，可空（Empty=Blank>Null）
        /// </summary>
        [MaxLength(511)]
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
            Pwd = user.Pwd ?? Pwd;
            Email = user.Email ?? Email;
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
            Pwd = user.Pwd ?? Pwd;
            Email = user.Email ?? Email;
            Description = user.Description ?? Description;
            Sex = user.Sex ?? Sex;
            BirthTime = user.BirthTime ?? BirthTime;
        }
    }
}
