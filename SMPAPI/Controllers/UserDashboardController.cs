using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSRFACE.BAL.DTO;
using SSRFACE.BAL.ILogic;
using SSRFACE.ViewModels;

namespace SSRFACE.Controllers
{
    public class UserDashboardController : Controller
    {
        protected Ipost _Ipost { get; private set; }

        public UserDashboardController(Ipost Ipost)
        {
            _Ipost = Ipost;
        }
        public IActionResult newsfeed()
        {
            HttpContext.Session.SetInt32("Userid", 1);
            NewsFeedViewModel Viewmodeldata = new NewsFeedViewModel();
            Viewmodeldata.Postlistdata = _Ipost.Getposts();

            return View(Viewmodeldata);
        }
    }
}