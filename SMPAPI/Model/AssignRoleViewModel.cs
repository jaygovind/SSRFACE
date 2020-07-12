using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMPAPI.Model
{
    public class AssignRoleViewModel
    {
        public string Roleid { get; set; }
        public List<AssignMultiRole> MultiRole { get; set; }
        public string UserId { get; set; }
    }
}
