using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt2.Controllers
{
    [Authorize(Roles = "admin")]
    public class CRUDAdminController : Controller
    {
        public ActionResult ConfigureUsers()
        {
            return View();
        }
    }
}