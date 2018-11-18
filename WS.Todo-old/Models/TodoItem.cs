using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Models
{
    public class TodoItem : TraceUpdateBase
    {
        /// <summary>
        /// 待办ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 待办名
        /// </summary>
        [MaxLength(31)]
        public string Name { get; set; }

        /// <summary>
        /// 待办内容
        /// </summary>
        [MaxLength(255)]
        public string Content { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        [Required]
        public bool IsComplete { get; set; }

        /// <summary>
        /// 预期完成时间
        /// </summary>
        public DateTime? ExpectTime { get; set; } 

        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActualTime { get; set; }
        
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj==null|| !(obj.GetType().Equals(this.GetType())))
            {
                return false;
            }
            var temp = (TodoItem)obj;
            if (temp.Id == Id)
            {
                return true;
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="update"></param>
        public override void Update (ITraceUpdate update)
        {
            var temp = (TodoItem)update;
            Name = temp.Name??Name;
            Content = temp.Content ?? Content;
            IsComplete = temp.IsComplete;
            ExpectTime = temp.ExpectTime ?? ExpectTime;
            ActualTime = temp.ActualTime ?? ActualTime;
        }
    }
}
