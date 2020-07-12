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

                string procName = SPROC_Names.sp_IsAdminControl.ToString();
                var ParamsArray = new SqlParameter[4];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@opcode", Value = 1, DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@LogedinId", Value = LogedinId, DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@UserId", Value = userid, DbType = System.Data.DbType.String };
                ParamsArray[3] = new SqlParameter() { ParameterName = "@Ischecked", Value = ischecked, DbType = System.Data.DbType.String };
                var UserDtoList = _userDtoRepository.ExecStoreProcedureWithReturnType(procName, "IsadminApprovedData", ParamsArray);
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
                string procName = SPROC_Names.sp_IsAdminControl.ToString();
                var ParamsArray = new SqlParameter[4];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@opcode", Value = 3, DbType = System.Data.DbType.Int32 };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@LogedinId", Value = loginid, DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@UserId", Value = userid, DbType = System.Data.DbType.String };
                ParamsArray[3] = new SqlParameter() { ParameterName = "@Ischecked", Value = "", DbType = System.Data.DbType.String };
                var UserRoleList = _userDtoRepository.ExecuteWithJsonResult(procName, "AssignRoleToUser", ParamsArray);

                return UserRoleList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
