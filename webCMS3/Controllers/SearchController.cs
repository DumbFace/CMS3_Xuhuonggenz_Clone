using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webCMS3.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string name = "")
        {
            ViewBag.key = name;
            ViewBag.Zones = MyCacheFunc.GetZones().Take(4).ToList();
            ViewBag.isSearchingSite = 1;
            return View();
        }

        public ActionResult GetList(string key, int page = 1, int pagesize = 10)
        {
            ViewBag.key = key;
            return PartialView(new myfunc().SearchNewsbyKey(key, page, pagesize));
        }
    }
}