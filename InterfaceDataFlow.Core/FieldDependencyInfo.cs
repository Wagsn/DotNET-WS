using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDataFlow
{
    /// <summary>
    /// 字段依赖信息
    /// </summary>
    public class FieldDependencyInfo
    {


        /// <summary>
        /// 占位符
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// 字段所在位置
        /// </summary>
        public FieldLocationEnum FieldLocation { get; set; }
    }
}
