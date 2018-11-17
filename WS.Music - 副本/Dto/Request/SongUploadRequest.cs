using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 歌曲上载请求，暂时不提供
    /// </summary>
    public class SongUploadRequest : RequestBase
    { 

        /// <summary>
        /// 歌曲名称
        /// </summary>
        [Required]
        public string SongName { get; set; }

        // public 
    }
}
