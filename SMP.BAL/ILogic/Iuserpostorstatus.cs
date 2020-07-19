using SSRFACE.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSRFACE.BAL.ILogic
{
   public interface Iuserpostorstatus
    {
        PostDto AddStatusorpost(PostDto data, long userid);
    }
}
