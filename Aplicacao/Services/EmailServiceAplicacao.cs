using Aplicacao.Contratos;
using Aplicacao.DTO;
using Aplicacao.Filas;
using Dominio.Contratos.Services;
using Dominio.Entidades;
using System;
using System.Threading.Tasks;
namespace Aplicacao.Services
{
    public class EmailServiceAplicacao : BaseServiceAplicacao, IEmailServiceAplicacao
    {
        private readonly IEmailService _emailService;
        public EmailServiceAplicacao(InjectorAplicacao injector, IEmailService emailService) : base(injector)
        {
            _emailService = emailService;
        }
        public Task<bool> Enviar(EmailDTO email)
       {
            var entity = base.Injector.Mapper.Map<Email>(email);
            base.Injector.Rabbit.Producer(email, FilasRabbit.EMAIL_ENVIADOS_COLETA);
            _emailService.AddAsync(entity);
            base.Injector.NetMailService.Enviar(email);
            return Task.FromResult(true);
        }

        public void OnCompleted() => throw new NotImplementedException();

        public void OnError(Exception error) => throw new NotImplementedException();

        public void OnNext(EmailDTO value) => Enviar(value).Wait();
    }
}