﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class ItemRequisicaoDTO
    {
        // public int Id { get; set; }
        public String ReqNumero { get; set; }
        public int NumItem { get; set; }
        public String Estoque { get; set; }
        public String Item { get; set; }
        public String Descricao { get; set; }
        public String GrupoCTA { get; set; }
        public Decimal QtdPedida { get; set; }
        public String Unidade { get; set; }
        public DateTime Entrega { get; set; }
        public Decimal Valor { get; set; }
        public String VBUrgente { get; set; }
        public String LocalEntrega { get; set; }
        public String Mapa { get; set; }
        public Decimal QTDAtendida { get; set; }
        public DateTime DTAtendida { get; set; }
        public String SqAprovacao { get; set; }
        public String SqItemAprovacao { get; set; }
    }
}
