using SSRFACE.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSRFACE.ViewModels
{
    public class NewsFeedViewModel
    {
        public List<PostDto> Postlistdata { get; set; }

        public PostDto NewPostdata { get; set; }
    }
}
