using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dominio.Contratos.Repositorios
{
    public interface IBaseRepositorio<TEntity> where TEntity : Base
    {
        Task AddAsync(TEntity entidade);
        Task AddAsync(IEnumerable<TEntity> entidades);
        Task UpdateAsync(TEntity entidade);
        Task UpdateAsync(IEnumerable<TEntity> entidades);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(params Guid[] ids);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filtro);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
