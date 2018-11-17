using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Models;
using WS.Music.Dto;

namespace WS.Music.Models
{
    /// <summary>
    /// 用户，艺人与用户是分离的
    /// </summary>
    public class User : TraceUpdateBase
    {
        /// <summary>
        /// ID，主键
        /// </summary>
        [Key]
        [MaxLength(36, ErrorMessage = "GUID最长不超过36")]
        public string Id { get; set; }

        /// <summary>
        /// 昵称
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
        [MaxLength(320, ErrorMessage ="邮箱地址不能超过最长320个字符限制")]
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

        public User(){ }

        public User([Required]User user)
        {
            _Update(user);
        }

        public User([Required]UserJson user)
        {
            // 外部可以采用AutoMapper映射
            Id = user.Id;
            Name = user.Name;
            Pwd = user.Pwd;
            Email = user.Email;
            Description = user.Description;
            Sex = user.Sex;
            BirthTime = user.BirthTime;
        }

        /// <summary>
        /// 用于Read等比较
        /// </summary>
        /// <param name="update"></param>
        public override void _Update(ITraceUpdate update)
        {
            base._Update(update);
            _Update(update as User);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="user"></param>
        public void _Update(User user)
        {
            Id = user.Id??Id;
            Name = user.Name??Id;
            Pwd = user.Pwd??Pwd;
            Email = user.Email??Email;
            Description = user.Description??Description;
            Sex = user.Sex??Sex;
            BirthTime = user.BirthTime??BirthTime;
        }


        // Fans:List<User> Follows:List<User> Event
    }
}
