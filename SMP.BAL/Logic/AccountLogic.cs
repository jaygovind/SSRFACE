using SMP.DATA.Models;
using SMP.Repository.Repository;
using SSRFACE.BAL.DTO;
using SSRFACE.BAL.HelperClass;
using SSRFACE.BAL.ILogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SSRFACE.BAL.Logic
{
    public class AccountLogic : IAccount
    {
        private readonly IRepository<Users> _Users;

        public AccountLogic(IRepository<Users> Users)
        {
            _Users = Users;
        }

            public int Register(RegistrationDTO userRegister)
           {

            try
            {
                if ((userRegister != null && userRegister.emailRegsiter != null) && (userRegister.regpassword != null && userRegister.regconpasswrd != null) && userRegister.FirstName!=null)
                {

                    if (userRegister.regpassword.Equals(userRegister.regconpasswrd))
                    {
                        var Isexit = (from s in _Users where (s.Email == userRegister.emailRegsiter) select s).FirstOrDefault();

                        if (Isexit == null)
                        {
                            Users Objuser = new Users();
                            var keyNew = Helper.GeneratePassword(10);
                            var password = Helper.EncodePassword(userRegister.regpassword, keyNew);
                            Objuser.Email = userRegister.emailRegsiter;
                            Objuser.UserName = userRegister.FirstName;
                            Objuser.FirstName = userRegister.FirstName;
                            Objuser.Gender = userRegister.Gender;
                            Objuser.Password = password;
                            Objuser.CreatedDate = DateTime.Now;
                            Objuser.UpdatedDate = DateTime.Now;
                            Objuser.VCode = keyNew;
                            _Users.InsertAndGetId(Objuser);

                            if (Objuser.UserId > 0)
                            {
                                return 1;
                            }
                        }
                        else
                        {
                            return 2; //user already exist
                        }
                    }
                    else
                    {
                        return 3; //Password mis matched
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
            return 0;
        }


        public UserSSODTO Login(RegistrationDTO Login)
        {
            UserSSODTO objuserssr = new UserSSODTO();
            try
            {
                var UserQuery = (from s in _Users where (s.Email == Login.emailLogin)select s).FirstOrDefault();

                if (UserQuery != null)
                {
                    var hashCode = UserQuery.VCode;
                    //Password Hasing Process Call Helper Class Method
                    var encodingPasswordString = Helper.EncodePassword(Login.LoginPaaswrd, hashCode);
                    //Check Login Detail User Name Or Password
                    var query = (from s in _Users where (s.Email == Login.emailLogin) && s.Password.Equals(encodingPasswordString) && (s.IsActive==true) select s).FirstOrDefault();
                    if (query != null)
                    {
                        objuserssr.UserId = query.UserId;
                        objuserssr.Email = query.Email;

                        return objuserssr;
                    }
                }
                return objuserssr;
            }
            catch(Exception ex)
            {
                return objuserssr;
            }

        }


        public UserSSODTO GetUserDetailsByUserId(long Userid)
        {
            UserSSODTO objuserssr = new UserSSODTO();
            try
            {
                var UserQuery = (from s in _Users where (s.UserId == Userid) select s).FirstOrDefault();

                objuserssr.UserName = UserQuery.UserName;

                return objuserssr;
            }
            catch(Exception ex)
            {
                return objuserssr;
            }

        }
   }
}
