using AutoMapper;
using ParkyAPI.Modelos;
using ParkyAPI.Modelos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.ParkyMapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<ParqueNacional, ParqueNacionalDto>().ReverseMap();
            CreateMap<Sendero, SenderoDto>().ReverseMap();
            CreateMap<Sendero, SenderoCreateDto>().ReverseMap();
            CreateMap<Sendero, SenderoUpdateDto>().ReverseMap();
        }
    }
}
