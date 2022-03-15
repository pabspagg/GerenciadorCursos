using GerenciadorCursos.DataAcessRepo.RepositoryInterfaces;
using GerenciadorCursos.DomainCore.Models;
using System;
using System.Threading.Tasks;

namespace GerenciadorCursos.DataAcessRepo.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositoryBase<CursoModel> Cursos { get; }
        IGenericRepositoryBase<UserModel> Users { get; }

        Task Commit();

        void RollBack();
    }
}