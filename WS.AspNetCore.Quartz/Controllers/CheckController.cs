using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WS.AspNetCore.Quartz
{
    public class CheckController : Controller
    {
        [HttpOptions]
        [HttpHead]
        [HttpGet]
        public ActionResult Index()
        {
            return Content("OK");
        }
    }
}
