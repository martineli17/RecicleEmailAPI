using Dominio.Contratos.UnitOfWork;

namespace Service
{
    public class Injector
    {
        public readonly IUnitOfWork UnitOfWork;
        public Injector(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}