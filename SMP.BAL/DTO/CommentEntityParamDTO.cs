using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
   public class CommentEntityParamDTO
    {
        public string Userid { get; set; }
        public string Entityid { get; set; }
        public string Comment { get; set; }

        public int Entityid_Int { get; set; }

        public int ReferenceField_Int { get; set; }
    }
}
