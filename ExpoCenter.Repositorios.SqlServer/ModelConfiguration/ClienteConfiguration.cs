using ExpoCenter.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpoCenter.Repositorios.SqlServer.ModelConfiguration
{
    internal class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}