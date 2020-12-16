using AutoMapper;
using Domain.DTOs;
using Domain.Entidades;

namespace Cross.Cutting.Mapping 
{
    public class EntitiToDtoProfile : Profile 
    { 
        public EntitiToDtoProfile() 
        {
            CreateMap<RequisicaoDTO, Requisicao>().ReverseMap();
        }
    }      
} 
