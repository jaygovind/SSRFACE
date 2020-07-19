using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SSRFACE.BAL.DTO;
using SSRFACE.DATA.ModelForTables;

namespace SSRFACE.BAL.AutoMappingcls
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserPost, PostDto>(); // means you want to map from User to UserDTO
        }
    }
}
