using GatewayPagamento.Dominio.Entidades;
using GatewayPagamento.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GatewayPagamento.WebApi.Models
{
    public class PagamentoGetViewModel
    {
        public int Id { get; set; }
        public string MascaraCartao { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int Status { get; set; }
        public string MensagemStatus { get; set; }

        internal static List<PagamentoGetViewModel> Mapear(List<Pagamento> pagamentos)
        {
            var viewModels = new List<PagamentoGetViewModel>();

            foreach (var pagamento in pagamentos)
            {
                viewModels.Add(Mapear(pagamento));
            }

            return viewModels;
        }

        internal static PagamentoGetViewModel Mapear(Pagamento pagamento)
        {
            var viewModel = new PagamentoGetViewModel();

            viewModel.NumeroPedido = pagamento.NumeroPedido;

            viewModel.Data = pagamento.Data;

            viewModel.Id = pagamento.Id;

            viewModel.Valor = pagamento.Valor;

            var numeroCartao = pagamento.Cartao.Numero;

            viewModel.MascaraCartao = $"{numeroCartao.Substring(0, 6)}...{numeroCartao.Substring(numeroCartao.Length - 4)}";

            viewModel.Status = (int)pagamento.Status;

            viewModel.MensagemStatus = pagamento.Status.ObterDescricao();

            return viewModel;
        }
    }
}