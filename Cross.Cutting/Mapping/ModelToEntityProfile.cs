using AutoMapper;
using Domain.Entidades;
using Domain.Models;

namespace Cross.Cutting.Mapping 
{
    public class ModelToEntityProfile : Profile 
    { 
        public ModelToEntityProfile() 
        {
            CreateMap<RequisicaoModel, Requisicao>().ReverseMap();
            CreateMap<ITRequisicaoModel, ITRequisicao>().ReverseMap();
            CreateMap<AnexoRequisicaoModel, AnexoRequisicao>().ReverseMap();
        }
    }      
} 
