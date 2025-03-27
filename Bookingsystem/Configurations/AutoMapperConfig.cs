using AutoMapper;
using Bookingsystem.Models.DTO;
using Bookingsystem.Models.Entities;

namespace Bookingsystem.Configurations
{
    public class AutoMapperConfig:Profile   
    {
        public AutoMapperConfig() {
            //<Source class, Destination Class>
            //We are copying student data to add user dto
            //We are copying student data to add user dto
            //CreateMap<User,AddUserDto>();
            //CreateMap<AddUserDto, User>();

            CreateMap<AddUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();

        }
    }
}
