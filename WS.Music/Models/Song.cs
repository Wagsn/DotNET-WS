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
    /// 歌曲实体，逻辑信息（在文件服务器上的位置就不要写在这里），歌曲信息的更新必须要知道其ID，如何处理现场或翻唱之类的行为（有些是分成两首歌，歌名或歌手不同）
    /// </summary>
    public class Song : TraceUpdateBase
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string Id { get; set; }

        /// <summary>
        /// 歌曲名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(511)]
        public string Description { get; set; }

        /// <summary>
        /// 歌曲的持续时长，可能不需要，因为通过歌曲文件可以得到
        /// </summary>
        public long? Duration { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 歌曲文件的URL，标准输出，如果想听其他规格的歌曲文件，请在SongFile中查找
        /// </summary>
        [MaxLength(255)]
        public string Url { get; set; }

        // 还有个来源Link，比如来源（ID+Type）于专辑，歌单、排行榜、分享

        /// <summary>
        /// 艺人
        /// </summary>
        [NotMapped]
        public List<Artist> Artists { get; set; }

        /// <summary>
        /// 专辑（歌曲可能属于几个专辑，不过当前歌曲只会提示属于哪个专辑）
        /// </summary>
        [NotMapped]
        public Album Album { get; set; }

        // User（哪个用户上传的） TagList（标签集）

        public Song() { }

        public Song (Song s)
        {
            Id = s.Id;
            Name = s.Name;
            Description = s.Description;
            Duration = s.Duration;
            ReleaseTime = s.ReleaseTime;
            Url = s.Url;
        }

        public Song(string id, string name, string url)
        {
            Init(id, name, url);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public void Init(string id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }

        /// <summary>
        /// 相等，查询用的比较函数，需要被迁移到_Equals函数中去
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if(obj!=null && !obj.GetType().Equals(GetType()) && ((Song)obj).Id == Id)
            {
                return true;
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// 相似度匹配
        /// </summary>
        /// <param name="update">比较对象</param>
        /// <returns></returns>
        public override bool _Equals(ITraceUpdate update)
        {
            // 需要判断update是不是Song类
            if (update != null && ((Song)update).Id == Id)
            {
                return true;
            }
            return base._Equals(update);
        }

        /// <summary>
        /// 生成HashCode，用于比较是否相同
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, ReleaseTime, Url);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="update"></param>
        public override void _Update(ITraceUpdate update)
        {
            base._Update(update);
            var song = (Song)update;
            Name = song.Name;
            Description = song.Description;
            Duration = song.Duration;
            ReleaseTime = song.ReleaseTime;
        }
    }
}
