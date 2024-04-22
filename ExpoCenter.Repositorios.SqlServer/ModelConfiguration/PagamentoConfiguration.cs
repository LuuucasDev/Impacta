using ExpoCenter.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpoCenter.Repositorios.SqlServer.ModelConfiguration
{
    internal class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamento");

            builder
                .Property(p => p.Valor)
                .HasPrecision(11, 2);
        }
    }
}