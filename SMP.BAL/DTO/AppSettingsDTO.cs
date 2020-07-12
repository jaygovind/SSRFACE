using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.DTO
{
    public class AppSettingsDTO
    {
        #region Jwt Authentication Settings

        /// <summary>
        /// Issuer
        /// </summary>
        public String Issuer { get; set; }
        /// <summary>
        /// Audience
        /// </summary>
        public String Audience { get; set; }
        /// <summary>
        /// Jwt Secret Key
        /// </summary>
        public String JwtSecretKey { get; set; }

        #endregion
    }
}
