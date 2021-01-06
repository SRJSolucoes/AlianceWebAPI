using AutoMapper;
using Domain.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ShoopingService : IShoopingService
    {
        private IRepository<Requisicao> _repository;
        private readonly IMapper _mapper;

        public ShoopingService(IRepository<Requisicao> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequisicaoDTO>> GetAllRequisicao()
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<RequisicaoDTO>>(ListEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ITRequisicaoDTO>> GetItemdaRequisicao(string Requisicao)
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<ITRequisicaoDTO>>(ListEntity.Where(x => x.ReqNumero == Requisicao));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AnexoRequisicaoDTO>> GetAnexodaRequisicao(string Requisicao)
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<AnexoRequisicaoDTO>>(ListEntity.Where(x => x.ReqNumero == Requisicao));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<RequisicaoDTO>> GetRequisicaoporCodigo(string Requisicao)
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                //var requisicoes = ListEntity.Where(x => x.ReqNumero == Requisicao);
                var requisicoes = _repository.QuerySelect().Where(x => x.ReqNumero == Requisicao);
                return _mapper.Map<IEnumerable<RequisicaoDTO>>(requisicoes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }

}
