using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.DTO
{
    public partial class RegistrationDTO
    {
        public string FirstName
        {
            get; set;
        }
        public string emailRegsiter
        {
            get; set;
        }
        public string regpassword
        {
            get; set;
        }
        public string regconpasswrd
        {
            get; set;
        }

        public string emailLogin
        {
            get; set;
        }
        public string LoginPaaswrd
        {
            get; set;
        }

        public string Gender { get; set; }
    }
}
