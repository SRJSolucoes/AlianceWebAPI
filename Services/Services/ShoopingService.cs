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

        public async Task<IEnumerable<RequisicaoDTO>> GetAll()
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
