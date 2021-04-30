using Aplicacao.DTO;
using System;
using System.Threading.Tasks;
namespace Aplicacao.Contratos
{
    public interface IEmailServiceAplicacao : IObserver<EmailDTO>
    {
        Task<bool> Enviar(EmailDTO email);
    }
}