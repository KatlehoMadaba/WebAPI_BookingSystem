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
            //First Name comes from User(Dest) class Name comes from DTO(Source)
            CreateMap<AddUserDto, User>().ForMember(n=>n.FirstName,opt =>opt.MapFrom(x=>x.Name)).ReverseMap();

            //Name comes from DTO(Source class) FirstName comes from User(dest class)
           // CreateMap<AddUserDto, User>().ReverseMap().ForMember(n => n.Name, opt => opt.MapFrom(x => x.FirstName));

            //updating a user
            CreateMap<UpdateUserDto, User>().ReverseMap();

        }
    }
}
