using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
    public class RefreshTokenDTO
    {
        public long UserId { get; set; }
        public string Refreshtoken { get; set; }
        public long TenantAccountId { get; set; }
    }
}
