using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMP.DATA.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public DateTime? Creatdate { get; set; }
        public bool? IsActive { get; set; }

        public bool? IsAdminApproved{ get; set; }

        public string OrganisationName { get; set; }
    }
}
