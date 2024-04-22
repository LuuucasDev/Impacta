using GatewayPagamento.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GatewayPagamento.WebApi.Models
{
    public class PagamentoPostViewModel
    {
        [Required]
        public string NumeroCartao { get; set; }
        [Required]
        public string NumeroPedido { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public decimal Valor { get; set; }

        internal static Pagamento Mapear(PagamentoPostViewModel viewModel)
        {
            var pagamento = new Pagamento();

            pagamento.Data = viewModel.Data;
            pagamento.Valor = viewModel.Valor;
            pagamento.NumeroPedido = viewModel.NumeroPedido;
            pagamento.Cartao = new Cartao {Numero = viewModel.NumeroCartao};

            return pagamento;
        }
    }
}