using System;
using System.ComponentModel.DataAnnotations;

namespace WS.Blog.Models
{
    public class Blog
    {
        /// <summary>
        /// 博客的ID
        /// </summary>
        [Key]
        public int BlogId { get; set; }

        /// <summary>
        /// 博客的名称
        /// </summary>
        [MaxLength(31)]
        public string Name { get; set; }

        /// <summary>
        /// 创建的时间
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// 博客的正文
        /// </summary>
        //[MaxLength(255)]  // 可以被Migration识别
        [StringLength(maximumLength: 511, MinimumLength =15)]  // 可以被Migration识别
        //[Range(0,511)]  // 不能被Migration识别
        //[RegularExpression(@"[\s\S]{,255}")]  // 不能被Migration识别
        public string Content { get; set; }
    }
}
