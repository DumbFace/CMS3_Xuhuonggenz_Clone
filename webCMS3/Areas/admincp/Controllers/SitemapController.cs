using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using webCMS3.Models;
namespace webCMS3.Areas.admincp.Controllers
{
    public class SitemapController : Controller
    {
        // GET: admincp/Sitemap
        public ActionResult Index()
        {
            string strPhysical = HttpContext.Request.PhysicalApplicationPath + "\\sitemap.xml";
            InsertXML(strPhysical, new myfunc().GetListSitemap());
            return View();
        }

        private void InsertXML(string strFile, List<tblNews> lst)
        {
            XmlTextWriter xtw = new XmlTextWriter(strFile, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.Indentation = 2;
            xtw.WriteStartDocument();
            xtw.WriteStartElement("urlset");
            xtw.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xtw.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xtw.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

            DateTime dt = DateTime.Now;
            string strDatetime = myCommon.FormatDateTimeYYYY(dt);
            xtw.WriteStartElement("url");
            xtw.WriteElementString("loc", "https://xuhuonggenz.com/");
            xtw.WriteElementString("lastmod", strDatetime);
            xtw.WriteElementString("changefreq", "hourly");
            xtw.WriteElementString("priority", "1.00");
            xtw.WriteEndElement();  //end url

            foreach (tblNews item in lst)
            {
                string DistributionDate = myCommon.FormatDateTimeYYYY(item.PublishDate);
                string strLink = "https://xuhuonggenz.com/" + item.UrlLink + "-" + item.Id + ".html";
                xtw.WriteStartElement("url");
                xtw.WriteElementString("loc", strLink);
                xtw.WriteElementString("lastmod", DistributionDate);
                xtw.WriteElementString("changefreq", "hourly");
                xtw.WriteElementString("priority", "0.80");
                xtw.WriteEndElement();  //end url
            }
            //ket thuc xml
            xtw.WriteEndElement(); //end url
            xtw.WriteEndDocument();//end urlset
            xtw.Flush();
            xtw.Close();
        }

    }
}