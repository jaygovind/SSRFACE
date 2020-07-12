using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
   public class SourceDTO
    {

        public string Name { get; set; }
        public string Note { get; set; }

        public IFormFile fileSource { set; get; }
    }
}
