using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3.Areas.admincp.Controllers
{
    public class ManageUsersController : Controller
    {
        // GET: admincp/ManageUsers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListUser(int page = 1, int pagesize = 20, string role = "%", string name = "")
        {
            ViewBag.page = page;
            ViewBag.pagesize = pagesize;
            ViewBag.role = role;
            ViewBag.name = name;

            return PartialView(new myfunc().GetListUser(page, pagesize, role, name));
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult IsExistUserName(string str)
        {
            return Content(new myfunc().IsExistUserName(str) ? "1" : "ok");
        }

        public ActionResult InsUser(tblUser tbl)
        {
            tbl.Password = myCommon.Encrypt(tbl.Password);
            new myfunc().InsertUser(tbl);
            return Content("ok");
        }

        public ActionResult ResetPass(int Id)
        {
            ViewBag.IdUser = Id;
            return PartialView();
        }

        public ActionResult UpdatePass(int Id, string str)
        {
            new myfunc().ResetPass(Id, myCommon.Encrypt(str));
            return Content("ok");
        }

        public ActionResult GetPassword()
        {
            return Content(myCommon.RandomPass());
        }

        public ActionResult ShowRoleUser(int Id)
        {
            return PartialView(new myfunc().GetUserbyId(Id));
        }

        public ActionResult UpdateRoleUser(int Id, int IdRole)
        {
            new myfunc().UpdateRole(Id, IdRole);
            return Content("ok");
        }

        public ActionResult RemoveUser(int Id)
        {
            new myfunc().DeleteUser(Id);
            return Content("ok");
        }
    }
}