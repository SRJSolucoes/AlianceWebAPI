using Domain.Entidades;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
        Task<Usuario> FindByLogin(Guid Idparceiro, string email, string senha);
        Task<Usuario> FindtoChangePassword(Guid Idusuario);
    }
}
