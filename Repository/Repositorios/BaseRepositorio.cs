using Dominio.Contratos.Repositorios;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repository.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositorios
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : Base
    {
        protected ContextEmail Context;
        public BaseRepositorio(ContextEmail context)
        {
            Context = context;
        }

        public async Task AddAsync(TEntity entidade) => await Context.Set<TEntity>().AddAsync(entidade);
        public async Task AddAsync(IEnumerable<TEntity> entidades) => await Context.Set<TEntity>().AddRangeAsync(entidades);
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filtro)
        {
            await Task.Yield();
            return Context.Set<TEntity>().Where(filtro);
        }
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entidade = await Context.Set<TEntity>().FindAsync(id);
            Context.Entry(entidade).State = EntityState.Detached;
            return entidade;
        }
        public async Task RemoveAsync(Guid id)
        {
            await Task.Yield();
            var entidade = GetByIdAsync(id).Result;
            Context.Set<TEntity>().Remove(entidade);
        }
        public async Task RemoveAsync(params Guid[] ids)
        {
            await Task.Yield();
            Context.Set<TEntity>().RemoveRange(Context.Set<TEntity>().Where(x => ids.Contains(x.Id)));
        }
        public async Task UpdateAsync(TEntity entidade)
        {
            await Task.Yield();
            Context.Set<TEntity>().Update(entidade);
        }
        public async Task UpdateAsync(IEnumerable<TEntity> entidades)
        {
            await Task.Yield();
            Context.Set<TEntity>().UpdateRange(entidades);
        }
    }
}