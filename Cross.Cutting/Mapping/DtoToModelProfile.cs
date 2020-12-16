using Domain.Models;
using AutoMapper;
using Domain.DTOs;

namespace Cross.Cutting.Mapping 
{
    public class DtoToModelProfile : Profile 
    { 
        public DtoToModelProfile() 
        {
            CreateMap<RequisicaoModel, RequisicaoDTO>().ReverseMap();
        } 
    }      
} 
