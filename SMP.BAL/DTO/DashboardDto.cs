using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
   public class DashboardDto
    {
        public string Counts { get; set; }

        public int Usercount { get; set; }
        public int RolesCount { get; set; }
        public int RolesAssigntousercount { get; set; }
    }
}
