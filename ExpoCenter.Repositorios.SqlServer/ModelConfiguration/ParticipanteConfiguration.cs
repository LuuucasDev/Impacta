using ExpoCenter.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpoCenter.Repositorios.SqlServer.ModelConfiguration
{
    internal class ParticipanteConfiguration : IEntityTypeConfiguration<Participante>
    {
        public void Configure(EntityTypeBuilder<Participante> builder)
        {
            builder.ToTable("Participante");

            builder.HasIndex(p => p.Cpf).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();

            builder
                .Property(p => p.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}