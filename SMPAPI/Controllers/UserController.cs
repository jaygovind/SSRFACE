using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMP.COMMON;
using SMP.DATA.Models;
using SMPAPI.Helpers;
using SMPAPI.Model;
using SMPAPI.ViewModels;

namespace SMPAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        protected IUserLogic _userBAL { get; private set; }

        public IOptions<AppSettingsDTO> AppSettings;

        private readonly IOptions<ApplicationSettings> _appSettings;

        public UserController(
            IUserLogic userBAL,
            IOptions<ApplicationSettings> appSettings,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager

            )
        {
            _userBAL = userBAL;
            _appSettings = appSettings;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        #region Interface Implementation

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IdentityUser>), 200)]
        [Route("getAspnetusers")]
        public IActionResult Get()
        {


          var userDtoList= _userBAL.GetAllAssignRolesUser();

            return Ok(userDtoList);
        }
        /// <summary>
        /// Get a user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IdentityUser), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("get/{Id}")]
        public IActionResult GetByuserId(string Id)
        {
            if (String.IsNullOrEmpty(Id))
                return BadRequest(new string[] { "Empty parameter!" });

            return Ok(_userManager.Users
                .Where(user => user.Id == Id)
                .Select(user => new
                {
                    user.Id,
                    user.Email,
                    user.PhoneNumber,
                    user.EmailConfirmed,
                    user.LockoutEnabled,
                    user.TwoFactorEnabled
                })
                .FirstOrDefault());
        }


        /// <summary>
        /// Insert a user with an existing role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("InsertWithRole")]
        public async Task<IActionResult> Post([FromBody]UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash= EncryptDecrypt.Encrypt(model.Password)
            };
                model.Password = EncryptDecrypt.Encrypt(model.Password);

                IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId).ConfigureAwait(false);
                if (role == null)
                {
                    return BadRequest(new string[] { "Could not find role!" });
                }

                IdentityResult result = await _userManager.CreateAsync(user).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    IdentityResult result2 = await _userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
                    if (result2.Succeeded)
                    {
                        return Ok(new
                    {
                        statuscode="1",
                        user.Id,
                        user.Email,
                        user.PhoneNumber,
                        user.EmailConfirmed,
                        user.LockoutEnabled,
                        user.TwoFactorEnabled
                    });
                    }
                }

                return Ok(new
                {
                    statuscode = "2",
                    msg = result.Errors.Select(x => x.Description).FirstOrDefault()
                }); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("update/{Id}")]
        public async Task<IActionResult> Put(string Id, [FromBody]EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            ApplicationUser user = await _userManager.FindByIdAsync(Id).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            // Add more fields to update
            user.Email = model.Email;
            user.UserName = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumber = model.PhoneNumber;
            user.LockoutEnabled = model.LockoutEnabled;
            user.TwoFactorEnabled = model.TwoFactorEnabled;

            IdentityResult result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        /// <summary>
        /// Delete a user (Will also delete link to roles)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("delete/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            if (String.IsNullOrEmpty(Id))
                return BadRequest(new string[] { "Empty parameter!" });

            ApplicationUser user = await _userManager.FindByIdAsync(Id).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            IdentityResult result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpGet]
        [Route("GetuserById")]
        public async Task<IActionResult> Get(string id)
        {
            int userid = 0;
            if (id != null)
            {
                userid = Convert.ToInt32(id);
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var currentUser = await _userBAL.GetById(userid);
                if (currentUser.UserId == 0)
                    return NotFound();
                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("CreateRole")]
        public async Task<List<UserRoleDTO>> CreateRole([FromBody] RoleViewmodel model)
        {
            try
            {
               return _userBAL.CreateRole(model.RoleName);
            }

            catch(Exception ex)
            {
                return null;
            }


        }





        [Route("GetUserRoleList")]
        public List<UserRoleDTO> GetUserRoleList()
        {
           return _userBAL.GetUserRoles();
        }



        /// <summary>
        /// User Sign In
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Model.LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _userBAL.AuthenticateUser(model.Email, model.Password);
                if (responseObj == null)
                {
                    return NotFound();
                }
                // Generate JWT Authentication Token
                var token = JwtAuthManager.GenerateJWTToken(responseObj.UserId.ToString(), _appSettings.Value.Issuer, _appSettings.Value.Audience, _appSettings.Value.JWT_Secret);
                var _refreshTokenObj = new RefreshTokenDTO
                {
                    UserId = responseObj.UserId,
                    Refreshtoken = Guid.NewGuid().ToString()
                };

                //Save Refresh Token
                var responseModel = await _userBAL.SaveRefreshToken(_refreshTokenObj);

                return Ok(new
                {
                    AllUser= _userBAL.GetAll(),
                    AllDetails = responseObj,
                    UserId = responseObj.UserId,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = _refreshTokenObj.Refreshtoken
                });
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _userBAL.GetAll();

                if (responseObj == null)
                {
                    return NotFound();
                }

                return Ok(responseObj);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Reset User Password
        /// </summary>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword(string oldpassword, string newpassword, long userid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _userBAL.ResetPassword(oldpassword, newpassword, userid);
                if (string.IsNullOrEmpty(result) || result != "passwordresetsuccessfully")
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Generate Jwt If Refresh Token Exists
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("{refreshToken}/refresh")]
        public async Task<IActionResult> RefreshToken([FromRoute]string refreshToken)
        {
            var responseObj = await _userBAL.AuthenticateToken(refreshToken);
            if (responseObj == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var token = JwtAuthManager.GenerateJWTToken(responseObj.UserId.ToString(), AppSettings.Value.Issuer, AppSettings.Value.Audience, AppSettings.Value.JwtSecretKey);
            var _refreshTokenObj = new RefreshTokenDTO
            {
                UserId = responseObj.UserId,
                Refreshtoken = Guid.NewGuid().ToString()
            };
            //Update Refresh Token
            var responseModel = await _userBAL.SaveRefreshToken(_refreshTokenObj);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = _refreshTokenObj.Refreshtoken
            });
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion



    }
}