using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Controllers
{
    public class ReadController : Controller
    {
        // GET: Read
        public ActionResult Index(string url, string id)
        {
            int contentId = -1;

            if (!string.IsNullOrEmpty(id))
                contentId = int.Parse(id);

            tblNews tbl = MyCacheFunc.GetNews(contentId);

            if (tbl == null)
            {
                return RedirectToAction("index", "home");
            }

            string sessionContent = "Page_" + contentId;
            if (Session[sessionContent] == null)
            {
                new myfunc().UpdateViewCount(contentId);
                Session[sessionContent] = 1;
            }
            string strUrl = WebConfigurationManager.AppSettings["UrlHost"];
            ViewBag.Title = tbl.Title;
            ViewBag.Description = tbl.Teaser.Replace("\"", "'");
            ViewBag.url = strUrl + tbl.UrlLink + "-" + tbl.Id.ToString() + ".html";
            ViewBag.Image = tbl.AvatarFB;
            ViewBag.Published = tbl.PublishDate;

            ViewBag.lstRelated = MyCacheFunc.GetLastNews().Where(m =>m.Id != contentId);
            ViewData["Tags"] = MyCacheFunc.GetTags();
            List<tblZone> tblZones = MyCacheFunc.GetZones().Take(4).ToList();
            ViewBag.Zones = tblZones;
            ViewBag.whichZoneHighlight = tblZones.Where(m => m.Id == tbl.IdZone).FirstOrDefault().Id;
            ViewBag.isReadingSite = 1;
            return View(tbl);
        }
    }
}