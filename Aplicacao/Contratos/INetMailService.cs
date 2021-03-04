using Aplicacao.DTO;
using System.Threading.Tasks;
namespace Aplicacao.Contratos
{
    public interface INetMailService
    {
        Task<bool> Enviar(EmailDTO email);
    }
}