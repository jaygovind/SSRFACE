using Newtonsoft.Json;
using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMP.COMMON.Enums;
using SMP.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SMP.BAL.Logic
{
    public class dashboardLogic : Idashboard
    {
        private readonly IRepository<DashboardDto> _DashboardDto;

        public dashboardLogic(IRepository<DashboardDto> DashboardDto)
        {
            _DashboardDto = DashboardDto;
        }
        public DashboardDto Getdashboarddata()
        {
            DashboardDto retutndata = new DashboardDto();

            string procName = SPROC_Names.Sp_dashboard.ToString();
            var ParamsArray = new SqlParameter[1];
            ParamsArray[0] = new SqlParameter() { ParameterName = "@opcode", Value = 1, DbType = System.Data.DbType.String };

            var UserRoleList = _DashboardDto.ExecuteWithJsonResult(procName, "Counts", ParamsArray);

            if (UserRoleList != null )
            {
                retutndata.Usercount = UserRoleList[0].Usercount;
                retutndata.RolesCount = UserRoleList[0].RolesCount;
                retutndata.RolesAssigntousercount = UserRoleList[0].RolesAssigntousercount;
            }

            return retutndata;
        }
    }
}
