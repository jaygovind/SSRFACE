using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSRFACE.BAL.DTO;
using SSRFACE.BAL.ILogic;

namespace SSRFACE.Controllers
{
    public class AccountController : Controller
    {
        protected IAccount _IAccount { get; private set; }

        public AccountController(IAccount IAccount)
        {
            _IAccount = IAccount;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationDTO userRegister)
        {
            try
            {

             var Returndata= _IAccount.Register(userRegister);
                if (Returndata == 1)
                {
                    ViewBag.data = "Regsiter Succeful. Now you can Login";
                    return View();
                }
                else if(Returndata == 2)
                {
                    ViewBag.data = "Already Regsiterd";
                    return View();
                }
                else if (Returndata == 3)
                {
                    ViewBag.data = "Password Miss matched";
                    return View();
                }
                else
                {
                    ViewBag.data = "Something wrong";
                    return View();
                }
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        [HttpPost]
        public ActionResult Login(RegistrationDTO userRegister)
        {
           var Userdata= _IAccount.Login(userRegister);

            if(Userdata!=null)
            {
                HttpContext.Session.SetInt32("Userid",Convert.ToInt32(Userdata.UserId));

                return RedirectToAction("newsfeed", "UserDashboard");
            }
            else{
                return RedirectToAction("Register", "Account");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Register", "Account");
        }


    }
}