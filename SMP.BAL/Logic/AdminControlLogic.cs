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
    public class AdminControlLogic : IAdminControl
    {
        private readonly IRepository<ApprovedUserDTO> _userApprovedDtoRepository;
        private readonly IRepository<UsersDTO> _userDtoRepository;
        public AdminControlLogic(IRepository<ApprovedUserDTO> userApprovedDtoRepository, IRepository<UsersDTO> userDtoRepository)
        {
            _userApprovedDtoRepository = userApprovedDtoRepository;
            _userDtoRepository = userDtoRepository;
        }
        public bool ApprovedUserbyadmin(string LogedinId, string userid,bool ischecked)
        {

            try
            {


                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public List<UsersDTO> DeleteUser(string loginid,string userid)
        {
            try
            {


            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }
    }
}
