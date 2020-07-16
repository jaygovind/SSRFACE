using Microsoft.AspNetCore.Http;
using SMP.COMMON.Enums;
using SMP.Repository.Repository;
using SSRFACE.BAL.DTO;
using SSRFACE.BAL.ILogic;
using SSRFACE.DATA.ModelForTables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SSRFACE.BAL.Logic
{
    public class CommentLogic : Icomment
    {
        private readonly IRepository<CommentsDTO> _CommentsDTO;
        private readonly IRepository<SubcommentOrReplyDTO> _SubcommentOrReplyDTO;

        private readonly IRepository<Comment> _Comment;
        private readonly IRepository<SubComment> _SubComment;

        public CommentLogic(

            IRepository<CommentsDTO> CommentsDTO,
             IRepository<Comment> Comment,
             IRepository<SubcommentOrReplyDTO> SubcommentOrReplyDTO,
             IRepository<SubComment> SubComment
            )
        {

            _CommentsDTO = CommentsDTO;
            _Comment = Comment;
            _SubcommentOrReplyDTO = SubcommentOrReplyDTO;
            _SubComment = SubComment;
        }
        public long? Addcomment(CommentsDTO data,int Postid,int userId)
        {

            try
            {
                Comment Commentobj = new Comment();
                Commentobj.CommentedDate = DateTime.Now;
                Commentobj.CommentMsg = data.CommentMsg;
                Commentobj.PostID = Postid;

                Commentobj.CommentedUserID = userId;

                var CommentreturnId = _Comment.InsertAndGetId(Commentobj);
                return CommentreturnId;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }


        public long? AddSubcommentOrReply(SubcommentOrReplyDTO subcommentdata, int ComId, int userId)
        {

            try
            {
                SubComment SubCommentobj = new SubComment();
                SubCommentobj.CommentedDate = DateTime.Now;
                SubCommentobj.CommentMsg = subcommentdata.CommentMsg;
                SubCommentobj.ComentID = ComId;

                SubCommentobj.SubComUserID = userId;

                var SubCommentreturnId = _SubComment.InsertAndGetId(SubCommentobj);
                return SubCommentreturnId;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public List<CommentsDTO> GetCommentsBypostid(int Postid)
        {
            var commentlist = new List<CommentsDTO>();
            try
            {
                string procName = SPROC_Names.Get_Comment.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@Opcode", Value = 1, DbType = System.Data.DbType.Int32 };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@Userid", Value = 0, DbType = System.Data.DbType.Int32 };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@Postid", Value = Postid, DbType = System.Data.DbType.Int32 };
                commentlist = _CommentsDTO.ExecuteWithJsonResult(procName, "GetComment", ParamsArray);
                return commentlist;
            }
            catch (Exception ex)
            {

            }
            return commentlist;
        }

        public List<SubcommentOrReplyDTO> GetSubcommentOrReplyByCommentId(int Commentid)
        {
            var commentlist = new List<SubcommentOrReplyDTO>();
            try
            {
                string procName = SPROC_Names.Get_SubCommentOrReplyOfComment.ToString();
                var ParamsArray = new SqlParameter[4];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@Opcode", Value = 1, DbType = System.Data.DbType.Int32 };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@Userid", Value = 0, DbType = System.Data.DbType.Int32 };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@Postid", Value = 0, DbType = System.Data.DbType.Int32 };
                ParamsArray[3] = new SqlParameter() { ParameterName = "@CommentId", Value = Commentid, DbType = System.Data.DbType.Int32 };
                commentlist = _SubcommentOrReplyDTO.ExecuteWithJsonResult(procName, "GetSubCommentOrReply", ParamsArray);
                return commentlist;
            }
            catch (Exception ex)
            {

            }
            return commentlist;
        }
    }
}
