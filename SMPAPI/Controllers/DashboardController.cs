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

namespace SMPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected IUserLogic _userBAL { get; private set; }

        protected Idashboard _Dashboard { get; private set; }

        public DashboardController(UserManager<ApplicationUser> userManager, IUserLogic userBAL, Idashboard Dashboard)
        {
            _userManager = userManager;
            _userBAL = userBAL;
            _Dashboard = Dashboard;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IdentityUser>), 200)]
        [Route("Getregisterusercount")]
        public IActionResult Get()
        {

            var Userlist = 0;
            try
            {
                Userlist = _userManager.Users.Where(i => i.IsActive == true).ToList().Count();
            }
            catch(Exception ex)
            {
                throw;
            }

            return Ok(Userlist);

        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IdentityUser>), 200)]
        [Route("Getdashboardcount")]
        public IActionResult GetCount()
        {
            DashboardDto Getdata;
            try
            {
              Getdata= _Dashboard.Getdashboarddata();
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok(Getdata);

        }


    }
  }