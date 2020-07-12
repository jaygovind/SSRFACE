using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SMP.BAL.DTO;

namespace SMP.BAL.ILogic
{
    public interface IUserLogic
    {
        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        ///
        //Task CreateRoleNameAsync(string name);

        UsersDTO GetUserRoleByRoleId(string roleid);
        List<UsersDTO> GetAllAssignRolesUserByid(string userid);
        List<UsersDTO> GetIsadminApproveddata(string userid);
        List<UsersDTO> GetAllAssignRolesUser();
        int AssignRole(string UserId, string Roleid);

        List<UserRoleDTO> CreateRole(string rolename);

        List<UserRoleDTO> GetUserRoles();

        Task<string> AddUser(UsersDTO user);

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UsersDTO> AuthenticateUser(string username, string password);

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        List<UsersDTO> GetAll();

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> ResetPassword(string oldpassword, string newpassword, long userId);

        /// <summary>
        /// Save Refresh Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> SaveRefreshToken(RefreshTokenDTO model);

        /// <summary>
        /// Authenticate Refresh Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<UsersDTO> AuthenticateToken(string refreshToken);

        /// <summary>
        /// Get User Detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UsersDTO> GetById(long userId);
    }
}
