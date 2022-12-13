using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index(int id)
        {
            tblNews obj = MyCacheFunc.GetTopTagNews(id);
            tblTag tbl = MyCacheFunc.GetTag(id);
            ViewBag.Title = tbl.TagName + ": " + obj.Title;
            ViewBag.Description = tbl.TagName + ": " + obj.Teaser;
            ViewBag.PubDate = obj.PublishDate;
            ViewBag.Zones = MyCacheFunc.GetZones().Take(4).ToList();
            ViewBag.isTaggingSite = 1;
            return View(tbl);
        }

        public ActionResult GetList(int Id, int page = 1, int pagesize = 10)
        {
            ViewBag.page = page;
            ViewBag.pagesize = pagesize;
            ViewBag.Id = Id;
            return PartialView(MyCacheFunc.GetNewsByIdTag(Id).ToPagedList(page-1,pagesize));
        }
    }
}