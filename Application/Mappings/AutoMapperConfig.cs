using Application.DTO;
using ApplicationDTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AutoMapperConfig:Profile   
    {
        public AutoMapperConfig() {

            CreateMap<AddUserDto, User>().ForMember(n=>n.FirstName,opt =>opt.MapFrom(x=>x.Name)).ReverseMap();

            CreateMap<UpdateUserDto, User>().ReverseMap();

        }
    }
}
