using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSRFACE.BAL.ILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSRFACE.ViewComponents
{
    public class LeftsideViewComponent : ViewComponent
    {
        protected IAccount _IAccount { get; private set; }
        public LeftsideViewComponent(IAccount IAccount)
        {
            _IAccount = IAccount;
        }
        public async Task<IViewComponentResult> InvokeAsync(int noOfEmployee)
        {
            long userid = 0;
            userid = Convert.ToInt64(HttpContext.Session.GetInt32("Userid"));
          var Modelreturn=  _IAccount.GetUserDetailsByUserId(userid);
            return await Task.FromResult((IViewComponentResult)View("Default", Modelreturn));
        }
    }
}
