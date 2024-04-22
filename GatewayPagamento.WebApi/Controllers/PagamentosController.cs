using GatewayPagamento.Dominio.Entidades;
using GatewayPagamento.Dominio.Servicos;
using GatewayPagamento.Repositorios.SqlServer.CodeFirst;
using GatewayPagamento.WebApi.Helpers;
using GatewayPagamento.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GatewayPagamento.WebApi.Controllers
{
    public class PagamentosController : ApiController
    {
        private readonly PagamentoRepositorio pagamentoRepositorio = new PagamentoRepositorio();
        private readonly CartaoRepositorio cartaoRepositorio = new CartaoRepositorio();
        private readonly PagamentoServico pagamentoServico;

        public PagamentosController()
        {
            pagamentoServico = new PagamentoServico(pagamentoRepositorio, cartaoRepositorio);
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/pagamentos/cartao/{guidCartao}")]
        public List<PagamentoGetViewModel> Get(Guid guidCartao)
        {
            return PagamentoGetViewModel.Mapear(pagamentoRepositorio.Selecionar(p => p.Cartao.Guid == guidCartao));
        }

        // POST api/<controller>
        public IHttpActionResult Post(PagamentoPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var statusPagamento = pagamentoServico.Inserir(PagamentoPostViewModel.Mapear(viewModel));
            var pagamento = PagamentoPostViewModel.Mapear(viewModel);
            pagamentoServico.Inserir(pagamento);

            var responseViewModel = PagamentoGetViewModel.Mapear(pagamento);

            switch (pagamento.Status)
            {
                case StatusPagamento.SaldoIndisponivel:
                case StatusPagamento.PedidoJaPago:
                case StatusPagamento.CartaoInexistente:
                    //return BadRequest(statusPagamento.ObterDescricao());
                    return Content(HttpStatusCode.BadRequest, responseViewModel);
                case StatusPagamento.PagamentoOK:
                    return Ok(responseViewModel);
            }
            return InternalServerError(new ArgumentOutOfRangeException(nameof(pagamento.Status)));
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}