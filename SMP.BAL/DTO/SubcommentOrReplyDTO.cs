using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.DTO
{
   public class SubcommentOrReplyDTO
    {
        public long SubComID { get; set; }
        public DateTime CommentedDate { get; set; }

        public string CommentMsg { get; set; }

        public long UserId { get; set; }

        public string Username { get; set; }

        public string imageProfileUrl { get; set; }
    }
}
