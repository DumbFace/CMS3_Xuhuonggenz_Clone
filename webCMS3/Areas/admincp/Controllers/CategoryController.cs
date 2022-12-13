using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Areas.admincp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: admincp/Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList()
        {
            return PartialView(new myfunc().GetListCategory());
        }

        public ActionResult SaveCategory(tblCategory tbl)
        {
            if (tbl.Id == 0)
                new myfunc().InsertCategory(tbl);
            else
                new myfunc().UpdateCategory(tbl);

            return Content("ok");
        }

        public ActionResult AddCategory(int Id)
        {
            if (Id == 0)
            {
                return PartialView(new tblCategory() { Id = 0 });
            }
            else
            {
                return PartialView(new myfunc().GetCategorybyId(Id));
            }
        }

        public ActionResult DeleteCategory(int Id)
        {
            new myfunc().DeleteCategory(Id);
            return Content("ok");
        }

        public JsonResult GetCategorybyIdZone(int Id)
        {
            return Json(new myfunc().GetListCategoryByIdZone(Id), JsonRequestBehavior.AllowGet);
        }
    }
}