using AutoMapper;
using Domain.DTOs.UsuarioDto;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repository;
using Domain.Security;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IRepository<Usuario> _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private IUsuarioRepository _usuarioRepository;

        public UsuarioService(IRepository<Usuario> repository, IMapper mapper, IPasswordHasher passwordHasher, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAdmin(Guid id)
        {
            try
            {
                return await _repository.DeleteAdminAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAll()
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<UsuarioDTO>>(ListEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllInactives()
        {
            try
            {
                var ListEntity = await _usuarioRepository.GetInatives();
                return _mapper.Map<IEnumerable<UsuarioDTO>>(ListEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTOCreateReturn> Post(UsuarioDTOCreate usuario)
        {
            try
            {
                var model = _mapper.Map<UsuarioModel>(usuario);
                var entity = _mapper.Map<Usuario>(model);

                entity.Salt = Guid.NewGuid().ToByteArray();
                var _senha = SenhaAleatoria.GetSenhaAleatoria();
                entity.Hash = _passwordHasher.Hash(_senha, entity.Salt);

                entity.Ativo = 'I';

                var result = await _repository.InsertAsync(entity); ;
                var _usuarioDTOCreateReturn = _mapper.Map<UsuarioDTOCreateReturn>(result);
                _usuarioDTOCreateReturn.Senha = _senha;

                return _usuarioDTOCreateReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTOCreateReturn> PostKeyUser(UsuarioChaveDTOCreate usuario)
        {
            try
            {
                var model = _mapper.Map<UsuarioModel>(usuario);
                var entity = _mapper.Map<Usuario>(model);

                entity.Salt = Guid.NewGuid().ToByteArray();
                var _senha = SenhaAleatoria.GetSenhaAleatoria();
                entity.Hash = _passwordHasher.Hash(_senha, entity.Salt);

                var result = await _repository.InsertAsync(entity); ;

                var _usuarioDTOCreateReturn = _mapper.Map<UsuarioDTOCreateReturn>(result);
                _usuarioDTOCreateReturn.Senha = _senha;

                return _usuarioDTOCreateReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTOUpdateReturn> Put(UsuarioDTOUpdate usuario)
        {
            try
            {
                var model = _mapper.Map<UsuarioModel>(usuario);
                var entity = _mapper.Map<Usuario>(model);
                var result = await _repository.UpdateAsync(entity, entity.Idusuario);

                return _mapper.Map<UsuarioDTOUpdateReturn>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> Get(Guid id)
        {
            try
            {
                var entity = await _repository.SelectAsync(id);
                return _mapper.Map<UsuarioDTO>(entity) ?? new UsuarioDTO();
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
