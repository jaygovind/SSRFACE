using SMP.DATA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SSRFACE.DATA.ModelForTables
{
   public class UserPost : BaseEntity<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserPostId { get; set; }

        public string PostContent { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }

        public long PostedByUserId { get; set; }
    }
}
