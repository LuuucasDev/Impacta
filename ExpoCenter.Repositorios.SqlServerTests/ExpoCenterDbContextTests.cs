using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpoCenter.Repositorios.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ExpoCenter.Dominio.Entidades;

namespace ExpoCenter.Repositorios.SqlServer.Tests
{
    [TestClass()]
    public class ExpoCenterDbContextTests
    {
        private readonly ExpoCenterDbContext dbContext;
        private readonly DbContextOptions<ExpoCenterDbContext> dbContextOptions;

        public ExpoCenterDbContextTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<ExpoCenterDbContext>()
                .UseSqlServer(new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExpoCenter;Integrated Security=True"))
                .LogTo(ExibirQuery, LogLevel.Information)
                .Options;

            dbContext = new ExpoCenterDbContext(dbContextOptions);
        }

        private void ExibirQuery(string query)
        {
            Console.WriteLine(query);
        }

        [TestMethod()]
        public void InserirEventoTeste()
        {
            var evento = new Evento();
            evento.Local = "São Paulo";
            evento.Data = new DateTime(2022, 7, 15);
            evento.Preco = 22.35m;
            evento.Descricao = "Blocos de Carnaval em São Paulo";


            dbContext.Eventos.Add(evento);
            dbContext.SaveChanges();
        }
        [TestMethod]
        public void InserirParticipanteTeste()
        {
            Participante participante = new ();
            participante.Cpf = "12345678910";
            participante.Email = "luuucasdev@gmail.com";
            participante.DataNascimento = Convert.ToDateTime("03/08/1992");
            participante.Nome = "Lucas";

            participante.Eventos = new List<Evento>
            {
              //dbContext.Eventos.Firts()
                dbContext.Eventos.Single(e => e.Descricao == "Blocos de Carnaval em São Paulo")
            };
            dbContext.Participantes.Add(participante);
            dbContext.SaveChanges ();

            foreach (var evento in participante.Eventos)
            {
                Console.WriteLine(evento.Descricao);
            }
        }
        [TestMethod]
        public void SelecionarParticipantesTeste()
        {
            foreach (var participante in dbContext.Participantes)
            {
                Console.WriteLine(participante.Nome);
            }
        }
        [TestMethod]
        public void InserirPagamentoTeste()
        {
            var pagamento = new Pagamento();

            pagamento.IdCartao = Guid.NewGuid();
            pagamento.IdProduto = Guid.NewGuid();
            pagamento.Valor = 21.03m;

            dbContext.Add(pagamento);
            dbContext.SaveChanges();
        }
    }
}