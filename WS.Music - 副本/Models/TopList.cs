using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Models
{
    /// <summary>
    /// 榜单实体
    /// </summary>
    public class TopList
    {
        [MaxLength(63)]
        public string Id { get; set; }
    }
}
