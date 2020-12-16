using AcessoWebApi.Infrastructure.Security;
using Domain.Entidades;
using Domain.Enum;
using Domain.Enum.Core;
using Domain.Interfaces;
using Domain.Models;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RepositoryBaseCrud<T> : IRepositoryCrud<T> where T : EntidadeBase
    {
        private readonly Func<string, ISession> _sessionInput;
        private readonly Func<string, ISession> _sessionOutput;

        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RepositoryBaseCrud(Func<string, ISession> _sessionInput,
                                  Func<string, ISession> _sessionOutput,
                                  ICurrentUserAccessor _currentUserAccessor)
        {
            if (!_sessionInput("Input").IsOpen)
            {
                _sessionInput("Input").SessionFactory.OpenSession();
            }

            if (!_sessionOutput("OutPut").IsOpen)
            {
                _sessionOutput("OutPut").SessionFactory.OpenSession();
            }

            this._sessionInput = _sessionInput;
            this._sessionOutput = _sessionOutput;

            this._currentUserAccessor = _currentUserAccessor;
        }

        public async Task<T> InsertAsync(T item)
        {
            using (var transaction = _sessionInput("Input").BeginTransaction())
            {
                try
                {
                    item.Datadealteracao = null;
                    item.Datadeinativacao = null;
                    item.Usuariodealteracao = null;
                    item.Usuariodeinativacao = null;
                    if (item.Ativo == '\0')
                    {
                        item.Ativo = 'A';
                    }

                    item.Datadeinclusao = DateTime.UtcNow;
                    item.Usuariodeinclusao = _currentUserAccessor.GetCurrentUser().Idusuario;
                    if (item.Parceiro == Guid.Empty)
                    {
                        item.Parceiro = _currentUserAccessor.GetCurrentUser().Idparceiro;
                    }

                    await _sessionInput("Input").SaveAsync(item);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();
                    throw ex;
                }
                return item;
            }
        }

        public async Task<T> UpdateAsync(T item, Guid id)
        {
            using (var transaction = _sessionInput("Input").BeginTransaction())
            {
                try
                {
                    var _itemCadastrado = await _sessionInput("Input").GetAsync<T>(id);
                    if (_itemCadastrado == null)
                        return null;
                    else
                    {

                        item.Datadeinclusao = _itemCadastrado.Datadeinclusao;
                        item.Usuariodeinclusao = _itemCadastrado.Usuariodeinclusao;
                        item.Parceiro = _itemCadastrado.Parceiro;
                        item.Ativo = _itemCadastrado.Ativo;

                        item.Datadealteracao = DateTime.UtcNow;
                        if (item.Usuariodealteracao == null)
                            item.Usuariodealteracao = _currentUserAccessor.GetCurrentUser().Idusuario;

                        await _sessionInput("Input").MergeAsync(item);
                        await _sessionInput("Input").FlushAsync();
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();
                    throw ex;
                }
                return item;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var transaction = _sessionInput("Input").BeginTransaction())
            {
                try
                {
                    var result = await _sessionInput("Input").GetAsync<T>(id);
                    if (result == null)
                        return false;
                    else
                    {

                        result.Ativo = 'I';
                        result.Datadeinativacao = DateTime.UtcNow;
                        result.Usuariodeinativacao = _currentUserAccessor.GetCurrentUser().Idusuario;

                        await _sessionInput("Input").UpdateAsync(result);

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> DeleteAdminAsync(Guid id)
        {
            using (var transaction = _sessionInput("Input").BeginTransaction())
            {
                try
                {
                    var result = await _sessionInput("Input").GetAsync<T>(id);
                    if (result == null)
                        return false;
                    else
                    {

                        await _sessionInput("Input").DeleteAsync(result);

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {

            var result = await _sessionOutput("OutPut").GetAsync<T>(id);
            if (result == null)
                return false;
            else
                return result.Ativo != 'A';
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                var result = await _sessionOutput("OutPut").GetAsync<T>(id);
                if (result != null)
                    return result.Ativo.Equals('A') ? result : null;
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                var _idparceiro = _currentUserAccessor.GetCurrentUser().Idparceiro;
                return await (from e in _sessionOutput("OutPut").Query<T>() select e).Where(e => e.Parceiro.Equals(_idparceiro) && e.Ativo.Equals('A')).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetInatives()
        {
            var Idparceiro = _currentUserAccessor.GetCurrentParcID();
            return await _sessionOutput("OutPut").Query<T>().Where(u => u.Parceiro.Equals(Idparceiro) && u.Ativo.Equals('I')).ToListAsync();
        }

        public void Dispose()
        {
            if (_sessionInput("Input").Transaction.WasCommitted) _sessionInput("Input").Transaction.Dispose();
            if (_sessionOutput("OutPut").Transaction.WasCommitted) _sessionOutput("OutPut").Transaction.Dispose();
        }
    }
}
