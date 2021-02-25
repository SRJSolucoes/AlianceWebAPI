using System;
using System.Collections.Generic;

namespace Domain.VO.Compras
{
    public class DetalhePedidoCompraVO
    {
        public Int64? SequenciaAprovacao { get; set; }
        public long Sequencia { get; set; }
        public List<PedidoCompraVO> PedidosAprovacoes { get; set; }
    }
}
