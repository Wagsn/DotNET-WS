using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.FileServer
{
    public class CheckController : Controller
    {
        [HttpOptions]
        [HttpHead]
        public ActionResult Index()
        {
            return Content("OK");
        }
    }
}
