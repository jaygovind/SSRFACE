using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMPAPI.Model
{
    public class ResetPasswordViewModel
    {
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
        public long Id { get; set; }
    }
}
