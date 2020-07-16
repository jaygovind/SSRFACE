using SSRFACE.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.ILogic
{
   public interface Icomment
    {
        long? AddSubcommentOrReply(SubcommentOrReplyDTO subcommentdata, int ComId, int userId);
        long? Addcomment(CommentsDTO data, int Postid, int userId);

        List<CommentsDTO> GetCommentsBypostid(int Postid);

        List<SubcommentOrReplyDTO> GetSubcommentOrReplyByCommentId(int Commentid);
    }
}
