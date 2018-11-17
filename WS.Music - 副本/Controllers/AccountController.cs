using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Dto;

namespace WS.Music.Controllers
{
    /// <summary>
    /// 账户控制器，用来登入、登出、注册，（前面两个就不管了，用客户端清除登陆的账号数据，这里先写注册API）
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public ResponseMessage SignUp()
        {
            return new ResponseMessage();
        }
        public ResponseMessage SignIn()
        {
            return new ResponseMessage();
        }

        public ResponseMessage SignOut()
        {
            return new ResponseMessage();
        }
    }
}
