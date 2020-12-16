using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class UsuarioModel
    {
        private Guid _idusuario;
        private Char _ativo;
        private String _nome;
        private String _email;
        private String _telefone;
        private String _role;
        private DateTime _datadeinclusao;
        private DateTime _datadealteracao;
        private DateTime _datadeinativacao;
        private Guid _usuariodeinclusao;
        private Guid _usuariodealteracao;
        private Guid _usuariodeinativacao;

        public virtual Guid Idusuario {
            get{return  _idusuario == Guid.Empty ? Guid.NewGuid() : _idusuario;}
            set{_idusuario = value;}
        }

        public virtual Char Ativo
        {
            get { return _ativo == '\0' ? 'A': _ativo; }
            set { _ativo = value; }
        }

        public virtual String Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public virtual String Role
        {
            get {return _role == null ? "User" : _role;}
            set { _role = value; }
        }

        public virtual String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual String Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        public virtual DateTime Datadeinclusao
        {
            get { return _datadeinclusao; }
            set { _datadeinclusao = value; }
        }

        public virtual DateTime DatadeAlteracao
        {
            get { return _datadealteracao; }
            set { _datadealteracao = value; }
        }

        public virtual DateTime DatadeInativacao
        {
            get { return _datadeinativacao; }
            set { _datadeinativacao = value; }
        }

        public virtual Guid Usuariodeinclusao
        {
            get { return _usuariodeinclusao; }
            set { _usuariodeinclusao = value; }
        }

        public virtual Guid Usuariodealteracao
        {
            get { return _usuariodealteracao; }
            set { _usuariodealteracao = value; }
        }
        public virtual Guid Usuariodeinativacao
        {
            get { return _usuariodeinativacao; }
            set { _usuariodeinativacao = value; }
        }
    }
}
