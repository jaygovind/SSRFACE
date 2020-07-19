using SSRFACE.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSRFACE.ViewModels
{
    public class GetCommentVm
    {
        public GetCommentVm()
        {
            GetcommentModel = new List<CommentsDTO>();
        }

        public List<CommentsDTO> GetcommentModel { get; set; }
    }
}
