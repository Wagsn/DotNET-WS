using Microsoft.AspNetCore.Mvc;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Dto;

namespace Music.Controllers
{
    /// <summary>
    /// 我的音乐控制器，主要用于获取我的音乐相关数据
    /// </summary>
    public class MusicController : ControllerBase
    {
        /// <summary>
        /// 连接测试
        /// </summary>
        [HttpGet]
        public ActionResult<string> Get() {
            return "Connected Successful";
        }
    }
}
