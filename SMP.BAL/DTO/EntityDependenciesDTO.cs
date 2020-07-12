using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
  public  class EntityDependenciesDTO
    {
        public int SourceId { get; set; }

        public string EntityId { get; set; }

        public  string SourceName { get; set; }

        public string HeaderText { get; set; }
        public string EntityName { get; set; }

        public string IntroText { get; set; }

        public string GearCode { get; set; }
        public int DataTypeId { get; set; }

        public string GearDesc { get; set; }
        public string DataTypeDesc { get; set; }

        public  List<FieldListDTO> FieldListReference { get; set; }
    }
}
