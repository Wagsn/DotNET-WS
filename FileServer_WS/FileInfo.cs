﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileServer
{
    /// <summary>
    /// 文件描述
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// GUID
        /// </summary>
        [MaxLength(36)]
        public string FileGuid { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
        [MaxLength(511)]
        public string Path { get; set; }

        /// <summary>
        /// 局部路径
        /// </summary>
        [MaxLength(511)]
        public string RelPath { get; set; }

        /// <summary>
        /// 原路径
        /// </summary>
        [MaxLength(511)]
        public string SrcPath { get; set; }

        /// <summary>
        /// 内容（audio/mpeg、image/jpeg、type/format）
        /// </summary>
        [MaxLength(63)]
        public string ContentType { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        [MaxLength(15)]
        public string FileExt { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// 内部路径
        /// </summary>
        [MaxLength(511)]
        public string Url { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime{ get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime? VisitTime { get; set; }

    }
}
