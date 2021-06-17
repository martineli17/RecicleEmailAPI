using Aplicacao.DTO;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Contratos
{
    public interface IEmailRabbitObservable : IObservable<EmailDTO>
    {
        Task Send(EmailDTO dados);
    }
}
