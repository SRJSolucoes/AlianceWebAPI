﻿using Domain.VO.Compras;
using Domain.VO.GestaoProcessos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.VO
{
    public class AprovacaoPendenteVO : ICloneable
    {
        public AprovacaoPendenteVO() { }

        public string CodigoFilial { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CodigoTipoRequisicao { get; set; }
        public string DescricaoTipoRequisicao { get; set; }
        public DateTime? Competencia { get; set; }
        public DateTime? ProgramacaoPagamento { get; set; }
        public string CodigoTipoCobranca { get; set; }
        public string DescricaoTipoCobranca { get; set; }
        public string DescricaoEstruturaFuncional { get; set; }
        public string DescricaoCategoriaFuncional { get; set; }
        public string Observacao { get; set; }
        public bool Aprovado { get; set; }
        public bool Reprovado { get; set; }
        public bool Cancelado { get; set; }
        public List<AprovacaoPendenteDetalheRequisicaoFinanceiraVO> ListaAprovacaoPendenteDetalheRequisicaoFinanceiraVO { get; set; }
        public List<AprovacaoPendenteDetalheRequisicaoComprasVO> ListaAprovacaoPendenteDetalheRequisicaoComprasVO { get; set; }
        public List<DetalhePedidoCompraVO> ListaDetalhePedidoCompraVO { get; set; }
        public List<AprovacaoPendenteDetalheContratoComprasVO> ListaAprovacaoPendenteDetalheContratoComprasVO { get; set; }
        public List<AprovacaoPendenteDetalheBorderoPagamentoVO> ListaAprovacaoPendenteDetalheBorderoPagamentoVO { get; set; }
        public List<AprovacaoPendenteDetalheViagemVO> ListaAprovacaoPendenteDetalheViagemVO { get; set; }
        public List<AprovacaoPendenteDetalheRelacaoPagamentoVO> ListaAprovacaoPendenteDetalheRelacaoPagamentoVO { get; set; }
        public string ListaRequisicaoFinanceiraJSON { get; set; }
        public string ListaRequisicaoComprasJSON { get; set; }
        public string ListaContratoComprasJSON { get; set; }
        public string ListaBorderoPagamentoJSON { get; set; }
        public string ListaViagemJSON { get; set; }
        public string ListaPedidoCompraJSON { get; set; }
        public string ListaApropriacaoJSON { get; set; }
        public string ListaRelacaoPagamentoJSON { get; set; }
        public string AprovadoresSerelizados { get; set; }
        public string CodigoFornecedor { get; set; }
        public string DescricaoFilial { get; set; }
        public string Moeda { get; set; }
        public string ProximoAprovador { get; set; }
        public string Identificador { get; set; }
        public long? SequenciaAprovacao { get; set; }
        public long? SequenciaItemAprovacao { get; set; }
        public long? SequenciaItemLinhaAprovacao { get; set; }
        public long? Sequencia { get; set; }
        public long? Status { get; set; }
        public string DescricaoStatus { get; set; }
        public string StatusEspecifico { get; set; }
        public string CodigoEmpresa { get; set; }
        public string EstruturaFuncional { get; set; }
        public string UsuarioEstruturaFuncional { get; set; }
        public string CategoriaFuncional { get; set; }
        public long? TipoAprovacao { get; set; }
        public string Aprovador { get; set; }
        public bool PrevisaoOrcamentaria { get; set; }
        public string NomeAprovador { get; set; }
        public string HoraAprovacao { get; }
        public string Justificativa { get; set; }
        public string NumeroRequisicao { get; set; }
        public string Requisitante { get; set; }
        public string NomeRequisitante { get; set; }
        public string CodigoMoeda { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }
        public string DescricaoProcesso { get; set; }
        public long? CodigoProcesso { get; set; }
        public decimal? NumeroItem { get; set; }
        public string DescricaoRequisicao { get; set; }
        public string NomeEmpresa { get; set; }
        public string UltimoAprovador { get; set; }
        public DateTime? DataAprovacao { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }

    }
}
