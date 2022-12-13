using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using webCMS3.Models;
using MvcPaging;

namespace webCMS3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Tags"] = MyCacheFunc.GetTags();
            tblConfig tbl = MyCacheFunc.GetConfigWeb();
            ViewBag.Title = tbl.TitleWeb;
            ViewBag.Description = tbl.DescriptionWeb;

            string url = WebConfigurationManager.AppSettings["UrlHost"];
            ViewBag.url = url;
            ViewBag.Image = url + "Content/Images/logohome.png";
            ViewBag.showHead = 1;
            ViewBag.isHomeFooter = 1;
            ViewBag.Zones= MyCacheFunc.GetZones().Take(4).ToList();
            return View();
        }
        public ActionResult GetArticlesFeature()
        {
            return PartialView(MyCacheFunc.GetArticlesFeature());
        }

        public ActionResult GetThreeArticles()
        {
            return PartialView(MyCacheFunc.GetNewsHome().Take(3));
        }

        public ActionResult GetListNews(int page = 1,int pagesize = 10)
        {
            return PartialView(MyCacheFunc.GetNewsHome().Skip(3).ToPagedList(page - 1, pagesize));
        }

        public ActionResult GetLatesNews()
        {
            return PartialView(MyCacheFunc.GetLastNews());
        }

        public ActionResult GetTagsView()
        {
            return PartialView(MyCacheFunc.GetTags());
        }

        public ActionResult GetTagsHeader()
        {
            return PartialView(MyCacheFunc.GetTags().Take(4));
        }
    }
}