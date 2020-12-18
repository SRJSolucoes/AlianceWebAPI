using AutoMapper;
using Domain.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ITRequisicaoDTO>> GetItemdaRequisicao(int Requisicao)
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<ITRequisicaoDTO>>(ListEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AnexoRequisicaoDTO>> GetAnexodaRequisicao(int Requisicao)
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<AnexoRequisicaoDTO>>(ListEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<RequisicaoDTO>> GetRequisicaoporCodigo(int Requisicao)
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

        public void Dispose()
        {
            _repository.Dispose();
        }
    }

}
