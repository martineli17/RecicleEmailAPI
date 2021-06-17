using Aplicacao.Contratos;
using Aplicacao.DTO;
using Aplicacao.Filas;
using System;
using System.Threading.Tasks;
namespace Aplicacao.Services
{
    public class EmailServiceAplicacao : BaseServiceAplicacao, IEmailServiceAplicacao
    {
        public EmailServiceAplicacao(InjectorAplicacao injector) : base(injector)
        {
        }
        public Task<bool> Enviar(EmailDTO email)
       {
            base.Injector.Rabbit.Producer(email, FilasRabbit.EMAIL_ENVIADOS_COLETA);
            base.Injector.NetMailService.Enviar(email);
            return Task.FromResult(true);
        }

        public void OnCompleted() => throw new NotImplementedException();

        public void OnError(Exception error) => throw new NotImplementedException();

        public void OnNext(EmailDTO value) => Enviar(value).Wait();
    }
}