using AutoMapper;
using Domain.Entidades;
using Domain.Models;

namespace Cross.Cutting.Mapping 
{
    public class ModelToEntityProfile : Profile 
    { 
        public ModelToEntityProfile() 
        {
            CreateMap<Requisicao, RequisicaoModel>().ReverseMap();
        } 
    }      
} 
