using GerenciadorCursos.DomainCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCursos.DataAcessRepo.Context.ConfigurationContext
{
    public class CursoConfig : IEntityTypeConfiguration<CursoModel>
    {
        public void Configure(EntityTypeBuilder<CursoModel> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Titulo).HasColumnType("varchar(30)").IsRequired();
            builder.Property(c => c.Duracao).HasColumnType("int").IsRequired();
            builder.Property(c => c.Status).HasColumnType("varchar(30)").HasConversion<string>().IsRequired();

            builder.HasData(
                 new { Id = 1, Titulo = "Curso programação backend", Duracao = 2, Status = Status.Previsto },
                 new { Id = 2, Titulo = "Curso programação frontend", Duracao = 2, Status = Status.EmAndamento },
                 new { Id = 3, Titulo = "Curso devops", Duracao = 1, Status = Status.Concluido },
                 new { Id = 4, Titulo = "Curso mobile", Duracao = 5, Status = Status.Previsto },
                 new { Id = 5, Titulo = "Curso inovação", Duracao = 10, Status = Status.Previsto }
           );

        }
    }
}
