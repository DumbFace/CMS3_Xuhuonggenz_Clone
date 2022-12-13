using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace webCMS1.Controllers
{
    public class ListCatController : Controller
    {
        // GET: Category
        public ActionResult Index(int Id)
        {
            ViewBag.IdZone = Id;
            ViewBag.Title = WebConfigurationManager.AppSettings["WebTitle"];
            ViewBag.Description = WebConfigurationManager.AppSettings["WebDescription"];
            return View();
        }

        public ActionResult GetList(int IdZone, int page = 1, int pagesize = 10)
        {
            ViewBag.page = page;
            ViewBag.pagesize = pagesize;
            ViewBag.IdZone = IdZone;
            return PartialView(new myfunc().GetNewsByZone(IdZone, page, pagesize));
        }
    }
}