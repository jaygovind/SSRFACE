using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSRFACE.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult newsfeed()
        {
            return View();
        }
    }
}