using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Areas.admincp.Controllers
{
    public class CrawDataController : Controller
    {
        // GET: admincp/CrawData
        public ActionResult Index()
        {
            return View();
        }



        public async Task<JsonResult> AutoFilter(string url, string whichWeb)
        {
            tblNews tbl = new tblNews();
            HtmlDocument htmlDoc = new HtmlDocument();

            string htmlString = await GetHtmlAsync(url);
            if (htmlString != null && htmlString != "")
            {
                htmlDoc.LoadHtml(htmlString);
            }
            //Lấy tiêu đề, tóm tắt, hình ảnh
            tbl = GetMetaTag(htmlDoc);
            //Lấy nội dung bài viết
            tbl.Body = GetBody(htmlDoc, whichWeb);

            return Json(new { Status = true, Tbl = tbl }, JsonRequestBehavior.AllowGet);
        }

        public string GetBody(HtmlDocument htmlDoc, string whichWeb)
        {
            string bodyHtml = "";
            switch (whichWeb)
            {
                case "tuoitre":
                    bodyHtml = GetContentTuoiTre(htmlDoc);
                    break;
                case "thanhnien":
                    bodyHtml = GetContentThanhNien(htmlDoc);
                    break;
                case "kenh14":
                    bodyHtml =  GetContentKenh14(htmlDoc);
                    break;
            }

            return bodyHtml;
        }

        public tblNews GetMetaTag(HtmlDocument htmlDoc)
        {
            tblNews tbl = new tblNews();
            HtmlNodeCollection metaNodes = SelectMetaNodes("meta", "property", "og:", htmlDoc);

            if (metaNodes != null)
            {
                foreach (HtmlNode node in metaNodes)
                {
                    if (node.Attributes["property"].Value.ToLower() == "og:title")
                    {
                        //Giải mã các kí tự " & < >... có trong title
                        tbl.Title = WebUtility.HtmlDecode(node.Attributes["content"].Value);
                    }
                    if (node.Attributes["property"].Value.ToLower() == "og:description")
                    {
                        tbl.Teaser = WebUtility.HtmlDecode(node.Attributes["content"].Value);
                    }
                    if (node.Attributes["property"].Value.ToLower() == "og:image")
                    {
                        tbl.Avatar = node.Attributes["content"].Value;
                    }
                }
            }
            return tbl;
        }

        public string GetContentKenh14(HtmlDocument htmlDoc)
        {
            string bodyContent = "";
            HtmlNode bodyNode = SelectSingleNode("div", "class", "knc-content", htmlDoc);
            if (bodyNode != null)
            {
                //Bỏ quảng cáo, tin tức liên quan...
                //...


                bodyContent = bodyNode.InnerHtml;
            }
            return bodyContent;
        }

        public string GetContentTuoiTre(HtmlDocument htmlDoc)
        {
            string bodyContent = "";
            HtmlNode bodyNode = SelectSingleNode("div", "id", "main-detail-body", htmlDoc);
            if (bodyNode != null)
            {
                //Bỏ quảng cáo, tin tức liên quan...
                RemoveChild("div", "type", "RelatedOneNews", bodyNode);

                bodyContent = bodyNode.InnerHtml;
            }
            return bodyContent;
        }

        public string GetContentThanhNien(HtmlDocument htmlDoc)
        {
            string bodyContent = "";
            HtmlNode bodyNode = SelectSingleNode("div", "id", "abdf", htmlDoc);
            if (bodyNode != null)
            {
                //Bỏ quảng cáo, tin tức liên quan...
                RemoveChild("div", "id", "adsWeb_AdsArticleAfterBody", bodyNode);
                RemoveChild("div", "id", "adsWeb_AdsArticleMiddle", bodyNode);
                RemoveChild("div", "class", "morenews", bodyNode);

                bodyContent = bodyNode.InnerHtml;
            }
            return bodyContent;
        }

        public async Task<string> GetHtmlAsync(string url)
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using (var request = new HttpClient(handler))
            {
                request.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                return await request.GetStringAsync(url);
            }
        }


        public void RemoveChild(string element, string attribute, string name, HtmlNode HtmlDocument)
        {
            HtmlDocument.SelectSingleNode($"//{element}[@{attribute}='{name}']").Remove();
        }

        public HtmlNode SelectSingleNode(string element, string attribute, string name, HtmlDocument HtmlDocument)
        {
            return HtmlDocument.DocumentNode.SelectSingleNode($"//{element}[@{attribute}='{name}']");
        }
        public HtmlNodeCollection SelectMetaNodes(string element, string attribute, string name, HtmlDocument HtmlDocument)
        {
            return HtmlDocument.DocumentNode.SelectNodes($"//{element}[contains(@{attribute},'{name}')]");
        }
    }
}