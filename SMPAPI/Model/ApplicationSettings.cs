using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMPAPI.Model
{
    public class ApplicationSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}
