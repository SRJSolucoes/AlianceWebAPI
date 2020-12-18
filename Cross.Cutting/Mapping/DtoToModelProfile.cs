using Domain.Models;
using AutoMapper;
using Domain.DTOs;

namespace Cross.Cutting.Mapping 
{
    public class DtoToModelProfile : Profile 
    { 
        public DtoToModelProfile() 
        {
            CreateMap<RequisicaoDTO, RequisicaoModel>().ReverseMap();
            CreateMap<ITRequisicaoDTO, ITRequisicaoModel>().ReverseMap();
            CreateMap<AnexoRequisicaoDTO, AnexoRequisicaoModel>().ReverseMap();
        }
    }      
} 
