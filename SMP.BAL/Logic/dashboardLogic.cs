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


            return retutndata;
        }
    }
}
