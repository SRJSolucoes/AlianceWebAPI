using Domain.Enum;
using Domain.Enum.Core;
using Domain.Models;
using Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class EntidadeBase
    {
        private Guid _fkparceiro;
        private DateTime _datadeinclusao;
        private Char _ativo;

        public virtual DateTime? Datadeinativacao { get; set; }
        public virtual DateTime? Datadealteracao { get; set; }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual Guid Usuariodeinclusao { get; set; }
        public virtual Guid? Usuariodealteracao { get; set; }
        public virtual Guid? Usuariodeinativacao { get; set; }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual Guid Parceiro
        {
            get { return _fkparceiro; }
            set { _fkparceiro = value; }
        }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual DateTime Datadeinclusao
        {
            get { return _datadeinclusao; }
            set { _datadeinclusao = (value==null ? DateTime.UtcNow: value); }
        }
        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        [StringLength(1, ErrorMessageResourceName = "Campo_com_tamanho_excedido", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual Char Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
    }
}
