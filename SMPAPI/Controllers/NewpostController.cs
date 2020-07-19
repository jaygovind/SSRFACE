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
    public class NewpostController : Controller
    {
        protected Iuserpostorstatus _Iuserpostorstatus { get; private set; }

        public NewpostController(Iuserpostorstatus Iuserpostorstatus)
        {
            _Iuserpostorstatus = Iuserpostorstatus;
        }

        [HttpPost]
        public IActionResult AddnewpostStatus(PostDto Postorstatus)
        {

            long userid = 0;
            userid =Convert.ToInt64(HttpContext.Session.GetInt32("Userid"));

            if (Postorstatus != null && userid != null)
            {
                NewsFeedViewModel Viewmodeldata = new NewsFeedViewModel();
                var Returnobj = _Iuserpostorstatus.AddStatusorpost(Postorstatus, userid);

                Viewmodeldata.NewPostdata = Returnobj;
                return PartialView("~/Views/Newpost/_AddnewpostStatus.cshtml", Viewmodeldata);
            }
            else
            {
                return null;
            }
        }
    }
}