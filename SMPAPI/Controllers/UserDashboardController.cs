using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSRFACE.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult newsfeed()
        {
            return View();
        }
    }
}