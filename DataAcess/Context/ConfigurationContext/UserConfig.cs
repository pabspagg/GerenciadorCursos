using GerenciadorCursos.DomainCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCursos.DataAcessRepo.Context.ConfigurationContext
{
    public class UserConfig : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Username).HasColumnType("varchar(15)").IsRequired();
            builder.Property(p => p.Password).HasColumnType("varchar(15)").IsRequired();
            builder.Property(p => p.Role).HasConversion<string>().HasColumnType("varchar(15)").IsRequired();

            builder.HasData(
                  new { Id = 1, Username = "pablo", Password = "123456", Role = Role.Aluno },
                  new { Id = 2, Username = "joao", Password = "123456", Role = Role.Aluno },
                  new { Id = 3, Username = "lucas", Password = "123456", Role = Role.Aluno },
                  new { Id = 4, Username = "diego", Password = "123456", Role = Role.Aluno },
                  new { Id = 5, Username = "victor", Password = "123456", Role = Role.Aluno },
                  new { Id = 6, Username = "joao", Password = "123456", Role = Role.Aluno },
                  new { Id = 7, Username = "maria", Password = "123456", Role = Role.Secretaria },
                  new { Id = 8, Username = "jose", Password = "123456", Role = Role.Gerencia }

      );
        }
    }
}
