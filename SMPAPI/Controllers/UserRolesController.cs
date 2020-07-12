using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMP.BAL.ILogic;
using SMP.DATA.Models;
using SMPAPI.Model;
using SMPAPI.ViewModels;

namespace SMPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        protected IUserLogic _userBAL { get; private set; }

        public UserRolesController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserLogic userBAL
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._userBAL = userBAL;
        }

        /// <summary>
        /// Get a user roles
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [Route("get/{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id).ConfigureAwait(false);
            return Ok(await _userManager.GetRolesAsync(user).ConfigureAwait(false));
        }

        /// <summary>
        /// Add a user to existing role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("AssignexistingRoleToUser")]
        public async Task<IActionResult> Post([FromBody]UserViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                ApplicationUser user = await _userManager.FindByIdAsync(model.UserId).ConfigureAwait(false);
                if (user == null)
                    return BadRequest(new string[] { "Could not find user!" });

                IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId).ConfigureAwait(false);
                if (role == null)
                    return BadRequest(new string[] { "Could not find role!" });

                IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return Ok(role.Name);
                }
                return BadRequest(result.Errors.Select(x => x.Description));
            }
            catch(Exception ex)
            {
                 return BadRequest();
            }

        }

        /// <summary>
        /// Delete a user from an existing role
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("delete/{Id}/{RoleId}")]
        public async Task<IActionResult> Delete(string Id, string RoleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            ApplicationUser user = await _userManager.FindByIdAsync(Id).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            IdentityRole role = await _roleManager.FindByIdAsync(RoleId).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new string[] { "Could not find role!" });

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }



        [HttpPost]
        [Route("AssignRoleToUser")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleViewModel Model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                IdentityUser user = await _userManager.FindByIdAsync(Model.UserId).ConfigureAwait(false);
                if (user == null)
                    return BadRequest(new string[] { "Could not find user!" });
                int Returndata = 0;
                foreach (var Roleid in Model.MultiRole)
                {

                    IdentityRole role = await _roleManager.FindByIdAsync(Roleid.id).ConfigureAwait(false);
                    if (role == null)
                        return BadRequest(new string[] { "Could not find role!" });

                     Returndata = _userBAL.AssignRole(Model.UserId, Roleid.id);
                }
                return Ok(Returndata);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }

           // return Ok("1");

        }



        [HttpPost]
        [Route("GetRoleByuserid")]
        public async Task<IActionResult> GetRoleByuserid([FromBody] AssignRoleViewModel Model)
        {
            try
            {

                 var  Returndata = _userBAL.GetAllAssignRolesUserByid( Model.UserId);

                return Ok(Returndata);

            }
            catch (Exception ex)
            {
                return Ok(0);
            }

            // return Ok("1");

        }


        [HttpPost]
        [Route("GetRoleByRoleid")]
        public async Task<IActionResult> GetRoleByRoleid([FromBody] AssignRoleViewModel Model)
        {
            try
            {

              var RoleName= _userBAL.GetUserRoleByRoleId(Model.Roleid);

                return Ok(RoleName);

            }
            catch (Exception ex)
            {
                return Ok(0);
            }

            // return Ok("1");

        }



    }
}