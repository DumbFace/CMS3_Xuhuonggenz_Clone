using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Areas.admincp.Controllers
{
    public class ZoneController : Controller
    {
        // GET: admincp/Zone
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            return PartialView(new myfunc().GetListZone());
        }

        public ActionResult SaveZone(tblZone tbl)
        {
            int Id = tbl.Id;
            if (tbl.Id == 0)
                Id = new myfunc().InsertZone(tbl);
            else
                new myfunc().UpdateZone(tbl);
            MyCacheFunc.SetZone(Id);
            return Content("ok");
        }

        public ActionResult AddZone(int Id)
        {
            if (Id == 0)
            {
                return PartialView(new tblZone() { Id = 0 });
            }
            else
            {
                return PartialView(new myfunc().GetZonebyId(Id));
            }
        }

        public ActionResult DeleteZone(int Id)
        {
            new myfunc().DeleteZone(Id);
            return Content("ok");
        }

    }
}