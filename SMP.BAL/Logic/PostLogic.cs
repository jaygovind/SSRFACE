using SMP.COMMON.Enums;
using SMP.Repository.Repository;
using SSRFACE.BAL.DTO;
using SSRFACE.BAL.ILogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SSRFACE.BAL.Logic
{
    public class PostLogic : Ipost
    {
        private readonly IRepository<PostDto> _PostDto;

        public PostLogic(

            IRepository<PostDto> PostDto
            )
        {

            _PostDto = PostDto;
        }


        public List<PostDto> Getposts()
        {
            try
            {
                string procName = SPROC_Names.Sp_GetPost.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@Opcode", Value = 1, DbType = System.Data.DbType.String };

                var Postlist = _PostDto.ExecuteWithJsonResult(procName, "Getpost", ParamsArray);
                return Postlist;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
