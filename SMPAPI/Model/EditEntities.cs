using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMPAPI.Model
{
    public class EditEntities
    {
        public int SourceId { get; set; }

        public string EntityId { get; set; }


        public string HeaderText { get; set; }
        public string EntityName { get; set; }

        public string IntroText { get; set; }

        public string GearCode { get; set; }
        public int DataTypeId { get; set; }

    }
}
