using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MigrationsDemo
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
