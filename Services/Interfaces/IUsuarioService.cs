using Domain.DTOs.UsuarioDto;
using Domain.Entidades;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUsuarioService 
    {
        Task<UsuarioDTO> Get(Guid id);

        Task<IEnumerable<UsuarioDTO>> GetAll();

        Task<IEnumerable<UsuarioDTO>> GetAllInactives();

        Task<UsuarioDTOCreateReturn> Post(UsuarioDTOCreate usuario);
        
        Task<UsuarioDTOCreateReturn> PostKeyUser(UsuarioChaveDTOCreate usuario);

        Task<UsuarioDTOUpdateReturn> Put(UsuarioDTOUpdate usuario);

        Task<bool> Delete(Guid id);

        Task<bool> DeleteAdmin(Guid id);
    }
}
