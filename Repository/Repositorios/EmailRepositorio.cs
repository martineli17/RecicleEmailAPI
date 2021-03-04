using Dominio.Contratos.Repositorios;
using Dominio.Entidades;

namespace Repository.Repositorios
{
    public class EmailRepositorio : BaseRepositorio<Email>, IEmailRepositorio
    {
        public EmailRepositorio(ContextEmail context) : base(context)
        {
        }
    }
}