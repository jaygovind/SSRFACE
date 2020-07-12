namespace SMP.DATA.Seeder
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using SMP.COMMON;

    using SMP.DATA.Models;
    using System;
    using System.Linq;

    public class SMPSeed
    {
        private readonly SSRFACEDBCONTEXT _context;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env { get; set; }

        public SMPSeed(SSRFACEDBCONTEXT context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public void SeedData()
        {

            if (!_context.Users.Any())
            {
                _context.Users.Add(
                    new Users
                    {
                        UserName = "User",
                        FirstName = "User",
                        LastName = "Name",
                        Password = EncryptDecrypt.Encrypt("Admin@123"), //
                        Email = "test@abc.com",
                        RefreshToken = "",
                        IsActive = true,
                        CreatedBy = 1,
                        CreatedDate = DateTime.UtcNow
                    }
               );

                _context.SaveChanges();
            }
        }
    }
}
