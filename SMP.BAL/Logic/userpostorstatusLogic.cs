using SMP.Repository.Repository;
using SSRFACE.BAL.DTO;
using SSRFACE.BAL.ILogic;
using SSRFACE.DATA.ModelForTables;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SMP.DATA.Models;
using System.Linq;

namespace SSRFACE.BAL.Logic
{
    public class userpostorstatusLogic : Iuserpostorstatus
    {
        private readonly IMapper _mapper;
        private readonly IRepository<UserPost> _UserPost;
        private readonly IRepository<Users> _Users;
        public userpostorstatusLogic(

            IRepository<UserPost> UserPost,
            IMapper mapper,
            IRepository<Users> Users
            )
        {

            _UserPost = UserPost;
            _mapper = mapper;
            _Users = Users;
        }

        public PostDto AddStatusorpost(PostDto data,long userid)
        {
            if (data != null)
            {
                try
                {
                    UserPost obj = new UserPost();

                    obj.CreatedBy = userid;
                    obj.CreatedDate = DateTime.Now;
                    obj.PostContent = data.PostContent;
                    obj.PhotoUrl = "";
                    obj.VideoUrl = "";
                    obj.PostedByUserId = userid;
                    _UserPost.InsertAndGetId(obj);
                    if(obj.UserPostId>0)
                    {
                        var UserQuery = (from s in _Users where (s.UserId == userid) select s).FirstOrDefault();

                        var userPostDTO = _mapper.Map<PostDto>(obj);
                        userPostDTO.UserName = UserQuery.UserName;
                        return userPostDTO;
                    }
                    return null;
                }
                catch(Exception ex)
                {
                    return null;
                }

            }

            else
            {
                return null;
            }


        }
    }
}
