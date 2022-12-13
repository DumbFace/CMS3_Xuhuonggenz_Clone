using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webCMS3.Areas.admincp.Controllers
{
    [MyAuthorize]
    public class adhomeController : BaseController
    {
        // GET: admincp/adhome
        public ActionResult Index()
        {
            return View();
        }
    }
}