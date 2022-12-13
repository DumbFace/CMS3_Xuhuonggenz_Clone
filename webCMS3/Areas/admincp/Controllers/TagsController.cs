using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webCMS3;
using webCMS3.Models;

namespace webCMS3.Areas.admincp.Controllers
{
    public class TagsController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetList(int page = 1, int pagesize = 20, string name = "")
        {
            ViewBag.page = page;
            ViewBag.pagesize = pagesize;
            ViewBag.name = name;
            return PartialView(new myfunc().GetListTag(page, pagesize, name));
        }

        public ActionResult SaveTag(tblTag tbl)
        {
            int Id = tbl.Id;
            if (tbl.Id == 0)
                Id = new myfunc().InsertTag(tbl);
            else
                new myfunc().UpdateTag(tbl);
            MyCacheFunc.SetTag(Id);
            return Content("ok");
        }

        public ActionResult AddTag(int Id)
        {
            if (Id == 0)
            {
                return PartialView("AddTag", new tblTag() { Id = 0 });
            }
            else
            {
                return PartialView("AddTag", new myfunc().GetTagbyId(Id));
            }
        }

        public ActionResult DeleteTag(int Id)
        {
            new myfunc().DeleteTag(Id);
            return Content("ok");
        }

        public ActionResult GetListTag(string q = "")
        {
            return Json(new myfunc().GetListTags(q), JsonRequestBehavior.AllowGet);
        }
    }
}