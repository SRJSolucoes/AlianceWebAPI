﻿using Domain.VO.Compras;
using Domain.VO.GestaoProcessos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.VO
{
    public class AprovacaoPendenteVO
    {

        //public string Identificador { get; set; }

        [Required(ErrorMessage = "SequenciaAprovacao é um campo obrigatório")]
        public long? SequenciaAprovacao { get; set; }

        [Required(ErrorMessage = "SequenciaItemAprovacao é um campo obrigatório")]
        public long? SequenciaItemAprovacao { get; set; }

        [Required(ErrorMessage = "SequenciaItemLinhaAprovacao é um campo obrigatório")]
        public long? SequenciaItemLinhaAprovacao { get; set; }

        [Required(ErrorMessage = "Sequencia é um campo obrigatório")]
        public long? Sequencia { get; set; }

        [Required(ErrorMessage = "Status é um campo obrigatório")]
        public long? Status { get; set; }
        //public string DescricaoStatus { get; set; }

        public string StatusEspecifico { get; set; }
        //public string CodigoEmpresa { get; set; }
        //public string EstruturaFuncional { get; set; }
        //public string CategoriaFuncional { get; set; }

        public long? TipoAprovacao { get; set; }
        [Required(ErrorMessage = "Aprovador é um campo obrigatório")]
        public string Aprovador { get; set; }
        [Required(ErrorMessage = "DataAprovacao é um campo obrigatório")]
        public DateTime? DataAprovacao { get; set; }
        public string Justificativa { get; set; }
        //public string NumeroRequisicao { get; set; }
        //public string Requisitante { get; set; }
        //public string Moeda { get; set; }
        //public DateTime? Data { get; set; }
        //public string DescricaoProcesso { get; set; }
        [Required(ErrorMessage = "CodigoProcesso é um campo obrigatório")]
        public long? CodigoProcesso { get; set; }
        //public decimal? NumeroItem { get; set; }
        //public string DescricaoRequisicao { get; set; }
        //public string ProximoAprovador { get; set; }


        //public string CodigoTipoCobranca { get; set; }
        //public string CodigoFilial { get; set; }
        //public string DescricaoFornecedor { get; set; }
        //public string CodigoTipoRequisicao { get; set; }
        //public string DescricaoTipoRequisicao { get; set; }
        //public DateTime? Competencia { get; set; }
        //public DateTime? ProgramacaoPagamento { get; set; }
        //public string DescricaoTipoCobranca { get; set; }
        //public string DescricaoEstruturaFuncional { get; set; }
        //public string DescricaoCategoriaFuncional { get; set; }
        //public string Observacao { get; set; }
        //public bool Aprovado { get; set; }
        //public bool Reprovado { get; set; }
        //public bool Cancelado { get; set; }
        //public string AprovadoresSerelizados { get; set; }
        //public string CodigoFornecedor { get; set; }
        //public string DescricaoFilial { get; set; }
        //public string UsuarioEstruturaFuncional { get; set; }
        //public bool PrevisaoOrcamentaria { get; set; }
        //public string NomeAprovador { get; set; }
        //public string HoraAprovacao { get; }
        //public string NomeRequisitante { get; set; }
        //public string CodigoMoeda { get; set; }
        //public decimal? Valor { get; set; }
        //public string NomeEmpresa { get; set; }
        //public string UltimoAprovador { get; set; }

    }
}
