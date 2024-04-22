using ExpoCenter.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExpoCenter.Repositorios.SqlServer
{
    public class ExpoCenterDbContext : DbContext
    {
        public ExpoCenterDbContext(DbContextOptions<ExpoCenterDbContext> opcoes) : base(opcoes)
        {
            Database.EnsureCreated();
        }
        //tabela
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //configuração da tabela

            //modelBuilder.ApplyConfiguration(new EventoConfiguration());
            //modelBuilder.ApplyConfiguration(new ParticipanteConfiguration());
            //modelBuilder.ApplyConfiguration(new PagamentoConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}