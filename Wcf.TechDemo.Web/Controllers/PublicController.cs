using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wcf.TechDemo.Web.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Landing()
        {
            return View();
        }

    }
}
