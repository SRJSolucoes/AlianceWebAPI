using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class RequisicaoModel
    {
        public String ReqNumero { get; set; }
        public DateTime ReqData { get; set; }
        public String Empresa { get; set; }
        public String Empresa_Nome { get; set; }
        public String Moeda { get; set; }
        public String DS_Moeda { get; set; }
        public String TipoReq { get; set; }
        public String DS_TipoReq { get; set; }
        public String Setor { get; set; }
        public String Requisitante { get; set; }
        public String Posicao_Funcional { get; set; }
        public String DS_Posicao_Funcional { get; set; }
        public String Observacao { get; set; }
        public DateTime DataMov { get; set; }
        public String Etape { get; set; }
        public String Tpoper { get; set; }
        public String VBReqcompra { get; set; }
        public String Usuario { get; set; }
        public String Ordem { get; set; }
        public String Justificativa { get; set; }
        public String Devolucao { get; set; }
        public String Protocolo { get; set; }
        public String ForaPrazo { get; set; }
        public String StatusAprv { get; set; }
        public String Impresso { get; set; }
        public String Encerram { get; set; }
        public String Destinacao { get; set; }
        public String CCusto { get; set; }

        //private Guid _idparceiro;
        //private Guid _fkparceiro;
        //private Char _ativo;
        //private String _nome;
        //private DateTime _datadeinclusao;
        //private DateTime _datadealteracao;
        //private DateTime _datadeinativacao;
        //private Guid _usuariodeinclusao;
        //private Guid _usuariodealteracao;
        //private Guid _usuariodeinativacao;

        //public virtual Guid Idparceiro
        //{
        //    get { return _idparceiro == Guid.Empty ? Guid.NewGuid() : _idparceiro; }
        //    set { _idparceiro = value; }
        //}
        //public virtual Guid Fkparceiro
        //{
        //    get { return _fkparceiro; }
        //    set { _fkparceiro = value; }
        //}
        //public virtual String Nome
        //{
        //    get { return _nome; }
        //    set { _nome = value; }
        //}
        //public virtual Char Ativo
        //{
        //    get { return _ativo == '\0' ? 'A' : _ativo; }
        //    set { _ativo = value; }
        //}
        //public virtual DateTime Datadeinclusao
        //{
        //    get { return _datadeinclusao; }
        //    set { _datadeinclusao = value; }
        //}
        //public virtual DateTime DatadeAlteracao
        //{
        //    get { return _datadealteracao; }
        //    set { _datadealteracao = value; }
        //}
        //public virtual DateTime DatadeInativacao
        //{
        //    get { return _datadeinativacao; }
        //    set { _datadeinativacao = value; }
        //}
        //public virtual Guid Usuariodeinclusao
        //{
        //    get { return _usuariodeinclusao; }
        //    set { _usuariodeinclusao = value; }
        //}
        //public virtual Guid Usuariodealteracao
        //{
        //    get { return _usuariodealteracao; }
        //    set { _usuariodealteracao = value; }
        //}
        //public virtual Guid Usuariodeinativacao
        //{
        //    get { return _usuariodeinativacao; }
        //    set { _usuariodeinativacao = value; }
        //}
    }
}
