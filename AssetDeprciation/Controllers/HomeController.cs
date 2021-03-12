using AssetDeprciation.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
      [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}