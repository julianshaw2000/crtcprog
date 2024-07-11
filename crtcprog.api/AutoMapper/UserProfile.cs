using AutoMapper;
using crtcprog.api.DTO;
using ctrcprog.api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace crtcprog.api.AutoMapper
{


    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>(); 
        }
    }
}