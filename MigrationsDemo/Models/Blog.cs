using System;
using System.ComponentModel.DataAnnotations;

namespace MigrationsDemo
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [MaxLength(31)]
        public string Name { get; set; }

        public DateTime? Time { get; set; }

        //[MaxLength(255)]  // 可以被Migration识别
        [StringLength(maximumLength: 511, MinimumLength =15)]  // 可以被Migration识别
        //[Range(0,511)]  // 不能被Migration识别
        //[RegularExpression(@"[\s\S]{,255}")]  // 不能被Migration识别
        public string Content { get; set; }
    }
}
