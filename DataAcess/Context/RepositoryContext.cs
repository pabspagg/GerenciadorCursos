using GerenciadorCursos.DomainCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCursos.DataAcessRepo.Context
{
    public class RepositoryContext : DbContext
    {


        public DbSet<UserModel> Users { get; set; }
        public DbSet<CursoModel> Cursos { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}