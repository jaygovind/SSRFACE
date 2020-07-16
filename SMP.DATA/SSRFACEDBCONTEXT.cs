
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMP.Data.ModelForTables;
using SSRFACE.DATA.ModelForTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.DATA.Models
{

    //public class SMPDbContext : IdentityDbContext<ApplicationUser> /when we need to alter aspnetuser table
   public class SSRFACEDBCONTEXT : IdentityDbContext<ApplicationUser>
    {
        #region Constructor and Configuration

        /// <summary>
        /// Initializes a new instance of the <see cref="SSRFACEDBCONTEXT"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>

        public SSRFACEDBCONTEXT(DbContextOptions<SSRFACEDBCONTEXT> options)
           : base(options)
        {

        }

        public DbSet<UserPost> UserPosts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<SubComment> SubComments { get; set; }
        public DbSet<Users> Users { get; set; }


        public DbSet<Status> Status { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        #endregion
    }
}
