using GerenciadorCursos.DataAcessRepo.Context;
using GerenciadorCursos.DataAcessRepo.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GerenciadorCursos.DataAcessRepo.Repository
{
    public class GenericRepositoryBase<T> : IGenericRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;

        public GenericRepositoryBase(RepositoryContext context) => _context = context;

        public async Task<IQueryable<T>> FindAllAsync(bool trackChanges)
        {
            return !trackChanges ? await Task.Run(() => _context.Set<T>().AsNoTracking()) : await Task.Run(() => _context.Set<T>());
        }

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? await Task.Run(() => _context.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => _context.Set<T>().Where(expression));
        }

        public async Task CreateAsync(T entity)
        {
            await Task.Run(() => _context.Set<T>().Add(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }
    }
}