using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSRFACE.BAL.DTO;
using Microsoft.AspNetCore.Http;
using SSRFACE.BAL.ILogic;
using SSRFACE.ViewModels;

namespace SSRFACE.Controllers
{
    public class CommentsController : Controller
    {
        protected Icomment _Icomment { get; private set; }

        public CommentsController(Icomment Icomment)
        {
            _Icomment = Icomment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetComments(int postId)
        {

            GetCommentVm Commentvmmodel = new GetCommentVm();
            Commentvmmodel.GetcommentModel = _Icomment.GetCommentsBypostid(postId);



            return PartialView("~/Views/Comments/_MyComments.cshtml", Commentvmmodel);
        }


        [HttpPost]
        public ActionResult AddComment(CommentsDTO comment, int postId)
        {

            var userid = HttpContext.Session.GetInt32("Userid");
            //bool result = false;


            int userId = Convert.ToInt32(userid);
            long? postid = 0;


            if (comment != null)
            {
               var commentid= _Icomment.Addcomment(comment, postId, userId);

            }

            return RedirectToAction("GetComments", "Comments", new { postId = postId });
        }


        [HttpGet]
        public PartialViewResult GetSubComments(int ComID)
        {
            GetsubcommorReplyVM SubCommentvmmodel = new GetsubcommorReplyVM();
            SubCommentvmmodel.Subcommntreplymodel = _Icomment.GetSubcommentOrReplyByCommentId(ComID);

            return PartialView("~/Views/Comments/_MySubComments.cshtml", SubCommentvmmodel);
        }


        [HttpPost]
        public ActionResult AddSubComment(SubcommentOrReplyDTO subComment, int ComID)
        {
            var userid = HttpContext.Session.GetInt32("Userid");

            long? Commentidreturnid = 0;

            if (subComment != null)
            {

                Commentidreturnid = _Icomment.AddSubcommentOrReply(subComment, ComID,Convert.ToInt32(userid));
            }

            return RedirectToAction("GetSubComments", "Comments", new { ComID = ComID });

        }


}
}