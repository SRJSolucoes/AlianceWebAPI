using AutoMapper;
using Domain.DTOs;
using Domain.Entidades;

namespace Cross.Cutting.Mapping 
{
    public class EntitiToDtoProfile : Profile 
    { 
        public EntitiToDtoProfile() 
        {
            CreateMap<Requisicao, RequisicaoDTO>().ReverseMap();
            CreateMap<ITRequisicao, ITRequisicaoDTO>().ReverseMap();
            CreateMap<AnexoRequisicao, AnexoRequisicaoDTO>().ReverseMap();
        }
    }      
} 
