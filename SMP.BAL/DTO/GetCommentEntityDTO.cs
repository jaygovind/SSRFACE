using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
   public class GetCommentEntityDTO
    {
        public string UserId { get; set; }

        public int EntityId { get; set; }

        public string Comment { get; set; }

        public string UserName { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
