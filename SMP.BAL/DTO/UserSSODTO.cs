using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.DTO
{
   public class UserSSODTO
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Password { get; set; }


        public string Email { get; set; }


        public string RefreshToken { get; set; }

        public string Gender { get; set; }

        public string VCode { get; set; }
    }
}
