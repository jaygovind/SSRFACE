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
        public long? ComentID { get; set; }
        public long? SubComUserID { get; set; }

        public bool Isdeleted { get; set; } = false;
    }
}
