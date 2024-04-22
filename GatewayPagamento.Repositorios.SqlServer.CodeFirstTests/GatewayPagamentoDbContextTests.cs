using Microsoft.VisualStudio.TestTools.UnitTesting;
using GatewayPagamento.Repositorios.SqlServer.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GatewayPagamento.Dominio.Entidades;

namespace GatewayPagamento.Repositorios.SqlServer.CodeFirst.Tests
{
    [TestClass()]
    public class GatewayPagamentoDbContextTests
    {
        [TestMethod()]
        public void InserirCartaoTest()
        {
            var contexto = new GatewayPagamentoDbContext();

            contexto.Cartoes.Add(new Cartao {Numero = "1234123412341234", Limite = 1000});
            contexto.SaveChanges();
        }
    }
}