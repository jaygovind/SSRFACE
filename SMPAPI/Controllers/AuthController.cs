using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMP.COMMON;
using SMP.DATA.Models;
using SMPAPI.Helpers;
using SMPAPI.Model;
using SMPAPI.Services;
using SMPAPI.Settings;
using SMPAPI.ViewModels;

namespace SMPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ClientAppSettings _client;
        private readonly JwtSecurityTokenSettings _jwt;
        protected IUserLogic _userBAL { get; private set; }

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailService emailService,
            IOptions<ClientAppSettings> client,
            IOptions<JwtSecurityTokenSettings> jwt,
            IUserLogic userBAL
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
            this._emailService = emailService;
            this._client = client.Value;
            this._jwt = jwt.Value;
            _userBAL = userBAL;

        }

        /// <summary>
        /// Confirms a user email address
        /// </summary>
        /// <param name="model">ConfirmEmailViewModel</param>
        /// <returns></returns>

        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string uid,string code)
        {

            if (uid == null || code == null)
            {
                return BadRequest(new string[] { "Error retrieving information!" });
            }

            ConfirmEmailViewModel model = new ConfirmEmailViewModel();
            model.UserId = uid;
            model.Code = code;
            var user = await _userManager.FindByIdAsync(model.UserId).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            var result = await _userManager.ConfirmEmailAsync(user, model.Code).ConfigureAwait(false);
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors.Select(x => x.Description));
        }

        /// <summary>
        /// Register an account
        /// </summary>
        /// <param name="model">RegisterViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));
            }

            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumber = model.PhoneNumber,
                    IsActive = true,
                    OrganisationName = model.OrganisationName,
                    Creatdate = DateTime.Now
                    //PasswordHash = EncryptDecrypt.Encrypt(model.Password)
                };


                    var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);



                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
                    var callbackUrl = $"{_client.Url}{_client.EmailConfirmationPath}?uid={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";

                    await _emailService.SendEmailConfirmationAsync(model.Email, callbackUrl).ConfigureAwait(false);

                    return Ok(new
                    {
                        statuscode = "1",
                        user.Id,
                        user.Email,
                        user.PhoneNumber,
                        user.EmailConfirmed,
                        user.LockoutEnabled,
                        user.TwoFactorEnabled
                    });
                }
                else
                {

                    return Ok(new
                    {
                        statuscode = "2",
                        msg = result.Errors.Select(x => x.Description).FirstOrDefault()
                    });
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Log into account
        /// </summary>
        /// <param name="model">LoginViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Login")]
        public async Task<IActionResult> CreateToken([FromBody]Model.LoginViewModel model)
        {

            try
            {
                bool Isadminapproved = false;
                bool IsAdminOrNot = false;
                ApplicationUser user = new ApplicationUser();

                if (model.Username != null)
                {
                    user = await _userManager.FindByNameAsync(model.Username);

                    if(user==null)
                    {
                        user = await _userManager.FindByEmailAsync(model.Username);
                    }
                }
                else
                {
                    return BadRequest(new string[] { "This is not valid user name." });
                }



               // var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user == null)
                    return BadRequest(new string[] { "Invalid credentials." });

                var tokenModel = new TokenModel()
                {
                    HasVerifiedEmail = false
                };

                // Only allow login if email is confirmed // commented this code bcz of not requiremnet

                //if (!user.EmailConfirmed)
                //{

                //    return Ok(
                //        new
                //        {
                //            statuscode="2",
                //            msg="Your Email is not Verified Yet.Go Your mail and verified first"
                //        }

                //        );
                //}


                //Here we check user is admin or its approved by admin  or not

              var Getadminapproveddata= _userBAL.GetIsadminApproveddata(user.Id);



                if(Getadminapproveddata!=null)
                {
                   var isadmin=Getadminapproveddata.Where(i => i.RoleName == "Admin").ToList().Count;


                    if (isadmin > 0)
                    {
                        Isadminapproved = true;
                        IsAdminOrNot = true;
                    }
                    else
                    {


                        if(user.IsAdminApproved==true)
                        {
                            Isadminapproved = true;
                        }
                        else
                        {
                            return Ok(new
                            {
                                statuscode = "2",
                                msg="This user is not approved by Admin Yet."
                            });
                        }
                   }
                }

                // Used as user lock
                if (user.LockoutEnabled)
                    return BadRequest(new string[] { "This account has been locked." });

                // model.Password = EncryptDecrypt.Encrypt(model.Password);


                if (Isadminapproved == true)
                {
                    var a = await _userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(false);

                    if (await _userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(false))
                    {
                        tokenModel.HasVerifiedEmail = true;

                        if (user.TwoFactorEnabled)
                        {
                            tokenModel.TFAEnabled = true;
                            return Ok(tokenModel);
                        }
                        else
                        {
                            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user).ConfigureAwait(false);
                            tokenModel.TFAEnabled = false;
                            tokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                           var Userrolelist= _userBAL.GetAllAssignRolesUser().ToList();

                            return Ok(new
                            {
                                statuscode = "1",
                                Isadmin= IsAdminOrNot,
                                AllUser = Userrolelist,
                                AllDetails = user,
                                UserId = user.Id,
                                token = tokenModel.Token,

                            }
                        );
                        }
                    }
                }
                return null;
            }

            catch(Exception ex)
            {

            }
            return null;
            //return BadRequest(new string[] { "Invalid login attempt." });
        }



        public List<UsersDTO> GetAllAspnetUser()
        {
            var Userlist = _userManager.Users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                user.EmailConfirmed,
                user.LockoutEnabled,
                user.TwoFactorEnabled
            });

            var userDtoList = new List<UsersDTO>();
            int i = 0;
            foreach (var item in Userlist)
            {
                i = i + 1;

                UsersDTO Objuser = new UsersDTO();
                Objuser.serialno = i;
                Objuser.UserIdString = item.Id;
                Objuser.UserName = item.UserName;
                userDtoList.Add(Objuser);
            }
            return userDtoList;
        }

        /// <summary>
        /// Log in with TFA
        /// </summary>
        /// <param name="model">LoginWith2faViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("tfa")]
        public async Task<IActionResult> LoginWith2fa([FromBody]LoginWith2faViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new string[] { "Invalid credentials." });

            if (await _userManager.VerifyTwoFactorTokenAsync(user, "Authenticator", model.TwoFactorCode).ConfigureAwait(false))
            {
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user).ConfigureAwait(false);

                var tokenModel = new TokenModel()
                {
                    HasVerifiedEmail = true,
                    TFAEnabled = false,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };

                return Ok(tokenModel);
            }
            return BadRequest(new string[] { "Unable to verify Authenticator Code!" });
        }

        /// <summary>
        /// Forgot email sends an email with a link containing reset token
        /// </summary>
        /// <param name="model">ForgotPasswordViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                //**  start// this code commented because its check email is confirmed or not **//

                //if (user == null || !(await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false)))

                //{
                //    return Ok(new
                //    {
                //        statuscode = "2",
                //        msg = "There is no such email or Your Email is not Verified Yet!"
                //    });
                //}
                //**  end//this code commented because its check email is confirmed or not **//

                if (user == null)
                {
                    return Ok(new
                    {
                        statuscode = "2",
                        msg = "There is no such email or Your Email is not Verified Yet!"
                    });
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
                var callbackUrl = $"{_client.ResetUrl}{_client.ResetPasswordPath}{user.Id}/{System.Net.WebUtility.UrlEncode(code)}";

                await _emailService.SendPasswordResetAsync(model.Email, callbackUrl).ConfigureAwait(false);

                return Ok(new
                {
                    statuscode = "1",
                    msg = "Please check your inbox, we've sent you reset password url."
                });

            }
            catch(Exception ex)
            {
                return Ok(new
                {
                    statuscode = "2",
                    msg = "Your Email is not Verified Yet.Go Your mail and verified first"
                });
            }
        }

        /// <summary>
        /// Reset account password with reset token
        /// </summary>
        /// <param name="model">ResetPasswordViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ViewModels.ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            var user = await _userManager.FindByIdAsync(model.UserId).ConfigureAwait(false);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest(new string[] { "Invalid credentials." });
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }

        /// <summary>
        /// Resend email verification email with token link
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("resendVerificationEmail")]
        public async Task<IActionResult> resendVerificationEmail([FromBody]UserViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
            var callbackUrl = $"{_client.Url}{_client.EmailConfirmationPath}?uid={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl).ConfigureAwait(false);

            return Ok();
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}