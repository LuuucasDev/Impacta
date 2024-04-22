using GatewayPagamento.Dominio.Entidades;
using GatewayPagamento.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayPagamento.Repositorios.SqlServer.CodeFirst
{
    public class CartaoRepositorio : ICartaoRepositorio
    {
        public Cartao Selecionar(string numeroCartao)
        {
            using (var contexto = new GatewayPagamentoDbContext())
            {
                return contexto.Cartoes.SingleOrDefault(c => c.Numero == numeroCartao);
            }
        }
    }
}
