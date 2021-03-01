using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entidades
{
    public class ItemRequisicao : EntidadeBase
    {
        public virtual int Id { get; set; }
        public virtual String ReqNumero { get; set; }
        public virtual int NumItem { get; set; }
        public virtual String Estoque { get; set; }
        public virtual String Item { get; set; }
        public virtual String Descricao { get; set; }
        public virtual String GrupoCTA { get; set; }
        public virtual Decimal QtdPedida { get; set; }
        public virtual String Unidade { get; set; }
        public virtual DateTime Entrega { get; set; }
        public virtual Decimal Valor { get; set; }
        public virtual String VBUrgente { get; set; }
        public virtual String LocalEntrega { get; set; }
        public virtual String Mapa { get; set; }
        public virtual Decimal QTDAtendida { get; set; }
        public virtual DateTime DTAtendida { get; set; }
    }
}
