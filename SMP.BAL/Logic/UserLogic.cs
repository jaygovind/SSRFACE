using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMP.COMMON;
using SMP.COMMON.Enums;
using SMP.DATA.Models;
using SMP.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMP.BAL.Logic
{
    public class UserLogic : IUserLogic
    {
        #region Private properties
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<UsersDTO> _userDtoRepository;
        private readonly IRepository<UserRoleDTO> _UserRoleDTO;
        private RoleManager<IdentityRole> _roleManager;
        #endregion

        #region CTOR's
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLogic"/> class.
        /// </summary>
        /// <param name="userRepository"></param>
        //public UserLogic(IRepository<Users> userRepository, )
        //{
        //    _userRepository = userRepository;
        //    _roleManager = roleMgr;
        //}

        public UserLogic(
            IRepository<Users> userRepository,
            IRepository<UsersDTO> userDTORepository,
            IRepository<UserRoleDTO> UserRoleDTO,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userRepository = userRepository;
            _userDtoRepository = userDTORepository;
            _UserRoleDTO = UserRoleDTO;
            _roleManager = roleManager;
        }
        #endregion

        #region Interface IuserService Methods

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ///


        //public async Task CreateRoleNameAsync(string name)
        //{

        //        IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
        //    if (result.Succeeded)
        //    {

        //    }



        //}

        public async Task<string> AddUser(UsersDTO user)
        {
            try
            {
                Users UserObj = this.ConvertUserDtoToUser(user);
                // Checks for duplicate user ------------------------------------
                var userList = await _userRepository.Where(x => x.UserName == UserObj.UserName).ToListAsync();
                if (userList.Count > 0) return "userAlreadyExist";

                UserObj.Password = EncryptDecrypt.Encrypt(user.Password);
                await _userRepository.InsertAsync(UserObj);
                await _userRepository.SaveChangesAsync();
                user.UserId = UserObj.UserId;

                return UserObj.UserId.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Authenticates the user.
        /// Method is used in the Token generation
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<UsersDTO> AuthenticateUser(string email, string password)
        {
            try
            {
                var enPwd = EncryptDecrypt.Encrypt(password);
                UsersDTO vm = new UsersDTO();

                //string procName = SPROC_Names.UspAuthenticateUser.ToString();
                //var ParamsArray = new SqlParameter[2];
                //ParamsArray[0] = new SqlParameter() { ParameterName = "@Email", Value = email, DbType = System.Data.DbType.String };
                //ParamsArray[1] = new SqlParameter() { ParameterName = "@Password", Value = enPwd, DbType = System.Data.DbType.String };
                //var resultData = _userDtoRepository.ExecuteWithJsonResult(procName, "UserDTO", ParamsArray);

                //var res = resultData != null ? resultData.FirstOrDefault() : new UsersDTO();

                var m = await _userRepository.Where(u => (u.Email.Equals(email))
                 && u.Password.Equals(enPwd)).FirstOrDefaultAsync();
                if (m != null)
                {
                    vm = ConvertUserToUserDto(m);
                    return vm;
                }
                return null;
                //return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public List<UsersDTO> GetAll()
        {
            try
            {
                List<UsersDTO> listtoReturn = new List<UsersDTO>();
                var fetchallUsers = _userRepository.GetAll().ToList();
                var listOfUsers = ConvertListofUsers(fetchallUsers);
                return listOfUsers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Reset Password Successfully
        /// </summary>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<string> ResetPassword(string oldpassword, string newpassword, long userid)
        {
            var userObj = await _userRepository.Where(u => u.UserId == userid).FirstOrDefaultAsync();
            if (userObj != null)
            {
                if (EncryptDecrypt.Decrypt(userObj.Password) != oldpassword)
                    return "InvalidOldPassword";

                userObj.Password = EncryptDecrypt.Encrypt(newpassword);
                await _userRepository.UpdateAsync(userObj);
                await _userRepository.SaveChangesAsync();
                return "PasswordResetSuccessfully";
            }
            return "UserNotFound";
        }

        /// <summary>
        /// Save Refresh Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> SaveRefreshToken(RefreshTokenDTO model)
        {
            var userObj = await _userRepository.Where(u => u.UserId == model.UserId).FirstOrDefaultAsync();
            if (userObj != null)
            {
                userObj.RefreshToken = model.Refreshtoken;
                await _userRepository.UpdateAsync(userObj);
                await _userRepository.SaveChangesAsync();
                return "refreshTokenSaved";
            }
            return "UserNotFound";
        }

        /// <summary>
        /// Authenticates the user.
        /// Method is used in the Token generation
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<UsersDTO> AuthenticateToken(string refreshToken)
        {
            try
            {
                UsersDTO UserModel = new UsersDTO();
                var userObj = await _userRepository.Where(x => x.RefreshToken == refreshToken).FirstOrDefaultAsync();
                if (userObj != null)
                {
                    UserModel = ConvertUserToUserDto(userObj);
                    return UserModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UsersDTO> GetById(long userId)
        {
            try
            {
                UsersDTO obj = new UsersDTO();
                var userModel = await _userRepository.Where(u => u.UserId == userId).FirstOrDefaultAsync();
                if (userModel != null)
                {
                    obj = ConvertUserToUserDto(userModel);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Map Model To DTO
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private UsersDTO ConvertUserToUserDto(Users u,int sno=0)
        {
            var userDto = new UsersDTO()
            {
                serialno= sno,
                Email = u.Email,
                CreatedDatestring=u.CreatedDate.ToString(),
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = u.Password,
                UserId = u.UserId,
                UserName = u.UserName,
                RefreshToken = u.RefreshToken
            };
            return userDto;
        }

        /// <summary>
        /// Map DTO to Model
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private Users ConvertUserDtoToUser(UsersDTO u)
        {
            var user = new Users()
            {
                Email = u.Email,
                FirstName = u.Email,
                LastName = u.LastName,
                Password = u.Password,
                UserId = u.UserId,
                UserName = u.Email,
                RefreshToken = u.RefreshToken,
                CreatedDate=DateTime.Now,
                CreatedBy=0,
                IsActive=true
            };
            return user;
        }

        /// <summary>
        /// Map Users List
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private List<UsersDTO> ConvertListofUsers(List<Users> users)
        {
            var userDtoList = new List<UsersDTO>();
            int i = 0;
            foreach (var item in users)
            {
                i = i + 1;

                userDtoList.Add(ConvertUserToUserDto(item,i));
            }
            return userDtoList;
        }



        public List<UsersDTO> GetIsadminApproveddata(string userid)
        {

            return null;
        }

        public List<UserRoleDTO> CreateRole(string  rolename)
        {
            if (rolename == null)
            {
                throw new ArgumentNullException("role");
            }
            IdentityRole roleobj = new IdentityRole();

            return null;


            // roleobj.Name = rolename;
            //var id=  _roleRepository.Insert(roleobj);
        }

        public List<UserRoleDTO> GetUserRoles()
        {
            try
            {
                return null;

            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public UsersDTO GetUserRoleByRoleId(string roleid)
        {
            try
            {


                    return null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

            public int AssignRole(string UserId, string Roleid)
        {
            try
            {


                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UsersDTO> GetAllAssignRolesUser()
        {
            try
            {


                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<UsersDTO> GetAllAssignRolesUserByid(string userid)
        {
            try
            {


                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion
    }
}
