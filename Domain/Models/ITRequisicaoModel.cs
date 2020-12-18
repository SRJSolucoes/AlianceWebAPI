using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ITRequisicaoModel
    {
        public String ReqNumero { get; set; }
        public int NumItem { get; set; }
        public String Estoque { get; set; }
        public String Item { get; set; }
        public String Descricao { get; set; }
        public String GrupoCTA { get; set; }
        public Decimal QtdPedida { get; set; }
        public String Unidade { get; set; }
        public String Entrega { get; set; }
        public String Valor { get; set; }
        public String VBUrgente { get; set; }
        public String LocalEntrega { get; set; }
        public String Mapa { get; set; }
        public Decimal QTDAtendida { get; set; }
        public Decimal DTAtendida { get; set; }
    }
}
