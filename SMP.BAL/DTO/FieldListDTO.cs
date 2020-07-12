using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
   public class FieldListDTO
    {
        public int FieldListId { get; set; }
        public int EntityId { get; set; }
        public int KeyFieldId { get; set; }

        public string FieldList_Id { get; set; }

        public string Entityname { get; set; }

        public string FieldName { get; set; }

        public string Instructions { get; set; }

        public string Format { get; set; }

        public string FormatNotes { get; set; }

        public string XmlTag { get; set; }
        public string FieldOrder { get; set; }

        public string WCPFC_Required { get; set; }
        public string DCC_Required { get; set; }
        public DateTime DateLastModified { get; set; }
        public string CurrentEntrySource { get; set; }

        public string FutureEntrySource { get; set; }
        public string Notes { get; set; }
        public string NafCode { get; set; }
    }
}
