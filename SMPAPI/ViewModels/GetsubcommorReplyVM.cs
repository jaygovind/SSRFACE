using SSRFACE.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSRFACE.ViewModels
{
    public class GetsubcommorReplyVM
    {
        public GetsubcommorReplyVM()
        {
            Subcommntreplymodel = new List<SubcommentOrReplyDTO>();
        }

     public  List<SubcommentOrReplyDTO>  Subcommntreplymodel { get; set; }
    }
}
