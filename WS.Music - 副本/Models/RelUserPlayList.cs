using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Models;

namespace WS.Music.Models
{
    /// <summary>
    /// 用户与歌单的关联实体，（多对多）
    /// </summary>
    public class RelUserPlayList : TraceUpdateBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 歌单ID
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string PlayListId { get; set; }

        /// <summary>
        /// 关联类型，创建：Create（喜欢：Like），收藏：Collection，推荐：Recommend（用户被推荐）
        /// </summary> 
        public string Type { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [NotMapped]
        public User User { get; set; }

        /// <summary>
        /// 歌单
        /// </summary>
        [NotMapped]
        public PlayList PlayList { get; set; }
    }
}
