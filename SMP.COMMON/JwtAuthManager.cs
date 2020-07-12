using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SMP.COMMON
{
    public static class JwtAuthManager
    {
        /// <summary>
        /// Generate Jwt Security Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static JwtSecurityToken GenerateJWTToken(string UserID,string issuer,string audience,string JwtSecretKey)
        {
            var claims = new[] {
                     //new Claim(JwtRegisteredClaimNames.Sub,email),
                    //new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                     new Claim(ClaimTypes.Name, UserID)
                };
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecretKey));
            var key = Encoding.ASCII.GetBytes(JwtSecretKey);

            var token = new JwtSecurityToken(
                            issuer: issuer,
                            audience: audience,
                            claims: claims,
                            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                            expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                            //Using HS256 Algorithm to encrypt Token
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                                                SecurityAlgorithms.HmacSha256Signature)
                             );
            return token;
        }
    }
}
