using Aplicacao.DTO;
using System.Threading.Tasks;
namespace Aplicacao.Contratos
{
    public interface INetMailService
    {
        bool Enviar(EmailDTO email);
    }
}