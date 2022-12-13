using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Areas.admincp.Controllers
{
    [MyAuthorize]
    public class ContentController : BaseController
    {
        // GET: admincp/Content
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View(new tblNews());
        }

        public ActionResult Update(int Id)
        {
            tblNews tbl = new myfunc().GetNewsbyId(Id);
            ViewBag.lstTag = tbl.Tags;
            return View(tbl);
        }

        [ValidateInput(false)]
        public ActionResult SaveNews(tblNews tbl, int intstatus = 0, int intFlag = 0, string dtPublic = "", string tags = "")
        {
            tblUser obj = GetLoggedUser;

            tbl.Title = myCommon.RemoveSpecial(tbl.Title);
            tbl.Teaser = myCommon.RemoveSpecial(tbl.Teaser);
            tbl.UrlLink = myCommon.FriendlyUrl(tbl.Title);
            tbl.Status = intstatus;
            tbl.IdUser = obj.Id;
            tbl.PublishDate = DateTime.ParseExact(dtPublic, "yyyy/MM/dd HH:mm", null);

            int Id = 0;
            myfunc myfunc = new myfunc();
            if (intFlag == 0)
            {
                Id = myfunc.InsertNews(tbl);
              
            }
            else
            {
                Id = tbl.Id;
                foreach (tblTag tag in myfunc.GetListTagsByIdNews(Id))
                {
                    MyCacheFunc.RemoveKeyNewsTag(tag.Id);
                    MyCacheFunc.RemoveKeyTopTagNews(tag.Id);
                }
                MyCacheFunc.RemoveKeyZoneNews(myfunc.GetIdZoneByIdNews(Id));
                myfunc.UpdateNews(tbl);
                myfunc.DeleteNewsTags(Id);
            }

            if (!string.IsNullOrEmpty(tags))
            {
                myfunc.InsertNewsTags(Id, tags);
            }
            myCommon.CachedNews(Id);
            return Content("ok");
        }

        public ActionResult ListActive()
        {
            return View();
        }
        public ActionResult GetListActive(string key, string fromdate, string todate, string idzone, int page = 1, int pagesize = 10)
        {
            ViewBag.page = page;
            ViewBag.pagesize = pagesize;
            ViewBag.key = key;
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.idzone = idzone;
            return PartialView(new myfunc().FilterContent(fromdate, todate, key, 1, idzone, page, pagesize));
        }
        public ActionResult GetListDelay(string key, string fromdate, string todate, string idzone, int page = 1, int pagesize = 10)
        {
            ViewBag.page = page;
            ViewBag.pagesize = pagesize;
            ViewBag.key = key;
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.idzone = idzone;
            return PartialView(new myfunc().FilterContent(fromdate, todate, key, 0, idzone, page, pagesize));
        }

        public ActionResult ListDelay()
        {
            return View();
        }

        public ActionResult UpdateStatus(int Id)
        {
            new myfunc().UpdateStatus(Id);
            myCommon.CachedNews(Id);
            return Content("ok");
        }

        public ActionResult DeleteNews(int Id)
        {
            new myfunc().DeleteNews(Id);
            myCommon.CachedNews(0);
            MyCacheFunc.RemoveNews(Id);
            return Content("ok");
        }

        public ActionResult ShowUrlImage(int Id)
        {
            ViewBag.IdType = Id;
            return PartialView();
        }
    }
}