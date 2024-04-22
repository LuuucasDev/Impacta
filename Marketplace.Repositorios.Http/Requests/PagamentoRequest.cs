using System;

namespace Marketplace.Repositorios.Http.Requests
{
    public class PagamentoRequest
    {
        public string NumeroCartao { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
    }
}
