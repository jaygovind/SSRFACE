using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
   public class EntityDTO
    {
        public int EntityId { get; set; }


        public string EntityName { get; set; }


        public string GearCode { get; set; }

        public int DataTypeId { get; set; }

        public string IntroText { get; set; }

        public string HeaderText { get; set; }

        public DateTime? DateLastModified { get; set; }

        public int SourceId { get; set; }

        public int? DocPageNum { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
