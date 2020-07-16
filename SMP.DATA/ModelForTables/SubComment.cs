using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SSRFACE.DATA.ModelForTables
{
    [Table("SubComment")]
    public class SubComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SubComID { get; set; }
        public string CommentMsg { get; set; }
        public DateTime? CommentedDate { get; set; }
        public int? ComentID { get; set; }
        public int? SubComUserID { get; set; }

        public bool Isdeleted { get; set; } = false;
    }
}
