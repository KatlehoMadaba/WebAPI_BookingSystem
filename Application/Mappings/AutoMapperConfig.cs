﻿using Application.DTO;
using ApplicationDTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AutoMapperConfig:Profile   
    {
        public AutoMapperConfig() {

            CreateMap<AddUserDto, User>()
                .ForMember(n => n.FirstName, opt => opt.MapFrom(x => x.Name))
                .ForMember(n => n.HashedPassword, opt => opt.MapFrom(x => x.Password))
                .ReverseMap();

            CreateMap<UpdateUserDto, User>().ForMember(n => n.FirstName, opt => opt.MapFrom(x => x.Name)).ReverseMap();

        }
    }
}
