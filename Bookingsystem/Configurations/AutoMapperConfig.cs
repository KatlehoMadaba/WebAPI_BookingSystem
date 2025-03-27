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
            
            //Congfigure when the prop names are not the same between desination class and dto/source class.
            //adding a user
            CreateMap<AddUserDto, User>().ForMember(n=>n.FirstName,opt =>opt.MapFrom(x=>x.Name)).ReverseMap();
            //updating a user
            CreateMap<UpdateUserDto, User>().ReverseMap();

        }
    }
}
