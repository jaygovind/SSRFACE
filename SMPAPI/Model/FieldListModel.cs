using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMPAPI.Model
{
    public class FieldListModel
    {
        public string KeyField_Id { get; set; }

        public string Entityname { get; set; }

        public string FieldName { get; set; }

        public string Instructions { get; set; }

        public string Format { get; set; }

        public string FormatNotes { get; set; }

        public string XmlTag { get; set; }
        public string FieldOrder { get; set; }

        public bool WCPFC_Required { get; set; }
        public bool DCC_Required { get; set; }
        public DateTime? DateLastModified { get; set; }
        public string CurrentEntrySource { get; set; }

        public string FutureEntrySource { get; set; }
        public string Notes { get; set; }
        public string NafCode { get; set; }
    }
}
