using SMP.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.ILogic
{
   public  interface IAdminControl
    {
        List<UsersDTO> DeleteUser(string loginid, string userid);
        bool ApprovedUserbyadmin(string LogedinId, string userid, bool ischecked);
    }
}
