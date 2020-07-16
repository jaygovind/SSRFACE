using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.DTO
{
    public class CommentsDTO
    {
        public int ComID { get; set; }
        public string CommentMsg { get; set; }

        public DateTime CommentedDate { get; set; }
        public long UserId { get; set; }

        public string Username { get; set; }

        public long UserPostId { get; set; }

        public string imageProfileUrl { get; set; }
    }
}
