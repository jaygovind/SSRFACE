using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.DTO
{
    public class PostDto
    {
        public long UserPostId { get; set; }
        public string PostContent { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }

        public long PostedByUserId { get; set; }
        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
