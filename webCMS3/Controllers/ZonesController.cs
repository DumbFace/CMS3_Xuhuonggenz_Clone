using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Controllers
{
    public class ZonesController : Controller
    {
        // GET: Zone
        public ActionResult Index(string url,string id)
        {
            ViewBag.Zones = MyCacheFunc.GetZones().Take(4).ToList();
            tblZone tbl = MyCacheFunc.GetZone(int.Parse(id));
            ViewBag.whichZoneHighlight = int.Parse(id);
            ViewBag.Title = tbl.ZoneName;
            ViewBag.Description = tbl.ZoneName ;
            ViewBag.PubDate = DateTime.Now ;
            return View(tbl);
        }

        public ActionResult GetList(int Id, int page = 1, int pagesize = 10)
        {
            ViewBag.Id = Id; 
            ViewBag.page = page;
            ViewBag.pagesize = pagesize;
            return PartialView(MyCacheFunc.GetNewsByIdZone(Id).ToPagedList(page-1,pagesize));
        }
    }
}