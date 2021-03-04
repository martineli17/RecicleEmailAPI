using Aplicacao.DTO;
using System.Threading.Tasks;
namespace Aplicacao.Contratos
{
    public interface IEmailServiceAplicacao
    {
        Task<bool> Enviar(EmailDTO email);
    }
}