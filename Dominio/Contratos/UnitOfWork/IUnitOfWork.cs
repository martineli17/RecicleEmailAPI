using System.Threading.Tasks;


namespace Dominio.Contratos.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}