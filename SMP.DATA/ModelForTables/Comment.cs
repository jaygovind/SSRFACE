using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SSRFACE.DATA.ModelForTables
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ComID { get; set; }
        public string CommentMsg { get; set; }
        public DateTime? CommentedDate { get; set; }
        public long? PostID { get; set; }
        public long? CommentedUserID { get; set; }

        public bool IsDeleted { get; set; } = true;

    }
}
