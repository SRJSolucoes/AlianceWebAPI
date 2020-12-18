using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class ITRequisicaoDTO
    {
        public string ReqNumero { get; set; }
        public int NumItem { get; set; }
        public string? Estoque { get; set; }
        public string? Item { get; set; }
        public string? Descricao { get; set; }
        public string? GrupoCTA { get; set; }
        public Decimal? QtdPedida { get; set; }
        public string? Unidade { get; set; }
        public string? Entrega { get; set; }
        public string? Valor { get; set; }
        public string? VBUrgente { get; set; }
        public string? LocalEntrega { get; set; }
        public string? Mapa { get; set; }
        public Decimal? QTDAtendida { get; set; }
        public Decimal? DTAtendida { get; set; }
    }
}
