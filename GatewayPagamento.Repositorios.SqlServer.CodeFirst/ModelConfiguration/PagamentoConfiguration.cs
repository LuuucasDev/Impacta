using GatewayPagamento.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace GatewayPagamento.Repositorios.SqlServer.CodeFirst.ModelConfiguration
{
    internal class PagamentoConfiguration : EntityTypeConfiguration<Pagamento>
    {
        public PagamentoConfiguration()
        {
            Property(p => p.NumeroPedido)
                .IsRequired()
                .HasMaxLength(20);

            Property(p => p.Valor)
                .IsRequired()
                .HasPrecision(12, 2);

            HasRequired(p => p.Cartao);
        }
    }
}