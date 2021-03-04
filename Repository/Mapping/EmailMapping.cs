using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    public class EmailMapping : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("EMAIL");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.DataCriacao).HasColumnName("DATACRIACAO").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Assunto).HasColumnName("ASSUNTO").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Destinatario).HasColumnName("DESTINATARIO").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Mensagem).HasColumnName("MENSAGEM").HasColumnType("text").IsRequired();
        }
    }
}
