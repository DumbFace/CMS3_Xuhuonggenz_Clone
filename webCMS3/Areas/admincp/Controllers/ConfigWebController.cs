using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Areas.admincp.Controllers
{
    public class ConfigWebController : Controller
    {
        public ActionResult Index()
        {
            return View(new myfunc().GetConfigWeb());
        }

        public ActionResult UpdateConfig(tblConfig tbl)
        {
            new myfunc().UpdateConfigWeb(tbl);
            MyCacheFunc.SetConfigWeb();
            return Content("ok");
        }
    }
}