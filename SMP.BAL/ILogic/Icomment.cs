using SSRFACE.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.ILogic
{
   public interface Icomment
    {
        long? AddSubcommentOrReply(SubcommentOrReplyDTO subcommentdata, long ComId, long userId);
        long? Addcomment(CommentsDTO data, long Postid, long userId);

        List<CommentsDTO> GetCommentsBypostid(long Postid);

        List<SubcommentOrReplyDTO> GetSubcommentOrReplyByCommentId(long Commentid);
    }
}
