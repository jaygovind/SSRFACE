using SMP.Data.ModelForTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
   public class KeyFieldDTO
    {

        public int KeyFieldId { get; set; }
        public string KeyName { get; set; }

        public string Format { get; set; }
        public string Description { get; set; }
        public string NAFCode { get; set; }
        public string ValidationRule { get; set; }
        public string XMLTag { get; set; }


       // public List<KeyFields> KeyFieldsList { get; set; }
    }
}
