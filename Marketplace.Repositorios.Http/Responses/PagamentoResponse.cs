using System;

namespace Marketplace.Repositorios.Http.Responses
{
    public class PagamentoResponse
    {
        public int Id { get; set; }
        public string MascaraCartao { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int Status { get; set; }
        public string MensagemStatus { get; set; }
    }
}
