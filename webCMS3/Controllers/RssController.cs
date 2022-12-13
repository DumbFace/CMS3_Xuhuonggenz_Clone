using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;
using webCMS3.Models;

namespace webCMS3.Controllers
{
    public class RssController : Controller
    {
        [HttpGet]
        public ActionResult Index() {
            return View(new myfunc().GetListZone());
        }


        [HttpGet]
        public ActionResult NewsRss()
        {
            tblConfig tblConfig = MyCacheFunc.GetConfigWeb();
            List<SyndicationItem> items = new List<SyndicationItem>();
            string url = WebConfigurationManager.AppSettings["UrlHost"];
            foreach (tblHome tbl in new myfunc().GetNewsRss())
            {
                SyndicationItem item = new SyndicationItem()
                {
                    Id = tbl.Id.ToString(),
                    Title = SyndicationContent.CreatePlaintextContent(tbl.Title),
                    Content = SyndicationContent.CreateHtmlContent(tbl.Teaser 
                                                                    + new XElement("img", new XAttribute("src", tbl.Avatar))),
                    PublishDate = tbl.PublishDate,
                    
                };
                item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(url + tbl.UrlLink + "-" + tbl.Id + ".html")));
                items.Add(item);
            }


           return new RssFeed(title: string.Format("{0} - RSS Feed",tblConfig.TitleWeb),
                               items: items,
                               contentType: "application/xml",
                               currentUrl: url ,
                               description: tblConfig.DescriptionWeb);
        }

        [HttpGet]
        public ActionResult CategoryRss(int id)
        {
            tblConfig tblConfig = MyCacheFunc.GetConfigWeb();
            List<SyndicationItem> items = new List<SyndicationItem>();
            string urlHost = WebConfigurationManager.AppSettings["UrlHost"];
            foreach (tblHome tbl in new myfunc().GetCategoryRss(id))
            {
                SyndicationItem item = new SyndicationItem()
                {
                    Id = tbl.Id.ToString(),
                    Title = SyndicationContent.CreatePlaintextContent(tbl.Title),
                    Content = SyndicationContent.CreateHtmlContent(tbl.Teaser
                                                                    + new XElement("img", new XAttribute("src", tbl.Avatar))),
                    PublishDate = tbl.PublishDate,

                };
                item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(urlHost + tbl.UrlLink)));
                items.Add(item);
            }


            return new RssFeed(title: string.Format("{0} - RSS Feed", tblConfig.TitleWeb),
                                items: items,
                                contentType: "application/xml",
                                currentUrl: urlHost,
                                description: tblConfig.DescriptionWeb);
        }

        [MyAuthorize]
        public ActionResult InstantArticle(int Id)
        {
            myfunc func = new myfunc();
            tblNews tbl = func.GetContentNewsbyId(Id);
            ViewBag.lstRelated = func.GetRelatedNews(tbl.IdZone, tbl.Id);
            return View(tbl);
        }

    }
}