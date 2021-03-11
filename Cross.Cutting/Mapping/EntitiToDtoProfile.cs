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
            CreateMap<ItemRequisicao, ItemRequisicaoDTO>().ReverseMap();
            CreateMap<AnexoRequisicao, AnexoRequisicaoDTO>().ReverseMap();
            CreateMap<DetReqPagamento, DetReqPagamentoDTO>()
                .ForMember(d => d.CDGrupo, o=> o.MapFrom(s => s.GRPAG))
                .ForMember(d => d.DescGrupo, o=> o.MapFrom(s => s.DSGRPAG))
                .ForMember(d => d.CCusto, o=> o.MapFrom(s => s.CCUSTO))
                .ForMember(d => d.DescCCusto, o=> o.MapFrom(s => s.DSCCUSTO))
                .ForMember(d => d.CTAFluxo, o=> o.MapFrom(s => s.CTAFLUXO))
                .ForMember(d => d.DescCTAFluxo, o=> o.MapFrom(s => s.DSCTAFLUXO))
                .ForMember(d => d.ValorItem, o => o.MapFrom(s => s.VLRITEM))
            .ReverseMap()
                .ForMember(d => d.GRPAG, o => o.MapFrom(s => s.CDGrupo))
                .ForMember(d => d.DSGRPAG, o => o.MapFrom(s => s.DescGrupo))
                .ForMember(d => d.CCUSTO, o => o.MapFrom(s => s.CCusto))
                .ForMember(d => d.DSCCUSTO, o => o.MapFrom(s => s.DescCCusto))
                .ForMember(d => d.CTAFLUXO, o => o.MapFrom(s => s.CTAFluxo))
                .ForMember(d => d.DSCTAFLUXO, o => o.MapFrom(s => s.DescCTAFluxo))
                .ForMember(d => d.VLRITEM, o => o.MapFrom(s => s.ValorItem));
      
    }
    }      
} 
