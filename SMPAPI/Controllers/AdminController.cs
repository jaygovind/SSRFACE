using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMP.DATA.Models;
using SMPAPI.Services;
using SMPAPI.ViewModels;

namespace SMPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        protected IUserLogic _userBAL { get; private set; }
        protected IAdminControl _AdminBal { get; private set; }
        public AdminController(
          UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager,
          IUserLogic userBAL,
          IAdminControl AdminBal,
           IEmailService emailService
          )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            _userBAL = userBAL;
            _AdminBal = AdminBal;
            _emailService = emailService;

        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("ApprovedUser")]
        public async Task<IActionResult> ApprovedUser([FromBody]ApprovedUserDTO model)
        {
            if(model!=null && (model.UserId!=null && model.Loginid !=null))

            {
              var Returndata= _AdminBal.ApprovedUserbyadmin(model.Loginid, model.UserId,model.Ischecked);

                return Ok();
            }

            return null;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("RegisterUserByAdmin")]
        public async Task<IActionResult> RegisterUserByAdmin([FromBody]UserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));
            }

            try
            {
                int ReturndataRole = 0;
                model.Password = GenerateRandomPassword();

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


                var UserReturnResult = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);

                if (UserReturnResult.Succeeded)
                {

                    //sending password with mail
                    await _emailService.SendEmailWithPasswordCreatedByAdminAsync(model.Email, model.Password, user.UserName).ConfigureAwait(false);

                    //now assging role to user

                    ApplicationUser user_data = await _userManager.FindByIdAsync(user.Id).ConfigureAwait(false);
                    if (user == null)
                        return BadRequest(new string[] { "Could not find user!" });

                    IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId).ConfigureAwait(false);
                    if (role == null)
                        return BadRequest(new string[] { "Could not find role!" });

                    ReturndataRole = _userBAL.AssignRole(user.Id, model.RoleId);
                    if (ReturndataRole==1)
                    {
                        var Userrolelist = _userBAL.GetAllAssignRolesUser().ToList();


                        return Ok(new
                        {
                            statuscode = "1",
                           Totaluser= Userrolelist
                        }) ;
                    }


                }
                else
                {
                    return Ok( new
                    {
                  statuscode = "2",
                  msg = UserReturnResult.Errors.Select(x => x.Description).FirstOrDefault()
                    }
                    );
           }

                }
            catch(Exception ex)
            {

            }
            return null;
         }



        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody]ApprovedUserDTO model)
        {
            try
            {

                if (model != null && (model.UserId != null && model.Loginid != null))

                {
                    var Returndata = _AdminBal.DeleteUser(model.Loginid, model.UserId);
                    return Ok( Returndata);
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;

        }




        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

    }
}