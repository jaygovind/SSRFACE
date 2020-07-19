using SSRFACE.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.ILogic
{
  public interface IAccount
    {

        int Register(RegistrationDTO userRegister);
        UserSSODTO Login(RegistrationDTO Login);

        UserSSODTO GetUserDetailsByUserId(long Userid);
    }
}
