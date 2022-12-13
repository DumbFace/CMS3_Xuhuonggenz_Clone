using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webCMS3.Models;

namespace webCMS3
{
    public class MyAuthorize: AuthorizeAttribute
    {
        public const string hsCookieName = "webCMS3";
        public const string hsSessionName = "TuiKhon_User";
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = GetLoggedUser(httpContext);

            if (user != null)
            {
                return true;
            }
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string strCookie = hsCookieName;

            if (filterContext.HttpContext.Request.Cookies[strCookie] == null)
            {
                filterContext.HttpContext.Response.Redirect("/dang-nhap");
            }
            else
            {
                var tbl = GetLoggedUser(filterContext.HttpContext);
                if (tbl == null)
                    filterContext.HttpContext.Response.Redirect("/dang-nhap");
            }
        }

        public static tblUser GetLoggedUser(HttpContextBase httpContext)
        {
            if (httpContext.Session[hsSessionName] != null)
            {
                return (tblUser)httpContext.Session[hsSessionName];
            }
            else
            {
                string strCookie = hsCookieName;
                if (httpContext.Request.Cookies[strCookie] != null && httpContext.Request.Cookies[strCookie].Value != "")
                {
                    int userId = int.Parse(httpContext.Request.Cookies[strCookie].Value);
                    tblUser tbl = new myfunc().GetUserbyId(userId);
                    httpContext.Session[hsSessionName] = tbl;
                    return tbl;
                }
            }
            return null;
        }

        public static void SetLoggedUser(HttpContextBase httpContext, tblUser tbl)
        {
            string strCookie = hsCookieName;

            httpContext.Response.Cookies[strCookie].Value = tbl.Id.ToString();
            httpContext.Response.Cookies[strCookie].Expires = DateTime.Now.AddHours(8);
            httpContext.Response.Cookies[strCookie].Path = "/";

            httpContext.Session[hsSessionName] = tbl;
        }

        public static void LogOut(HttpContextBase httpContext)
        {
            string strCookie = hsCookieName;
            httpContext.Session[hsSessionName] = null;

            if (httpContext.Request.Cookies[strCookie] != null)
            {
                httpContext.Response.Cookies[strCookie].Value = null;
                httpContext.Response.Cookies[strCookie].Expires = DateTime.Now;
            }
        }
    }
}