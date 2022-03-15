using GerenciadorCursos.DataAcessRepo.Context;
using GerenciadorCursos.DataAcessRepo.Repository;
using GerenciadorCursos.DataAcessRepo.RepositoryInterfaces;
using GerenciadorCursos.DomainCore.Models;
using System.Threading.Tasks;

namespace GerenciadorCursos.DataAcessRepo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepositoryBase<CursoModel> Cursos { get; private set; }

        public IGenericRepositoryBase<UserModel> Users { get; private set; }

        private readonly RepositoryContext _context;

        public UnitOfWork(RepositoryContext context)
        {
            _context = context;
            Users = new GenericRepositoryBase<UserModel>(_context);
            Cursos = new GenericRepositoryBase<CursoModel>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void RollBack()
        {
        }
    }
}