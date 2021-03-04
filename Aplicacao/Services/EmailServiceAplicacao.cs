using Aplicacao.Contratos;
using Aplicacao.DTO;
using Aplicacao.Filas;
using Dominio.Contratos.Services;
using Dominio.Entidades;
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
        public async Task<bool> Enviar(EmailDTO email)
       {
            var result = true;
            var entity = base.Injector.Mapper.Map<Email>(email);
            if (result && !await _emailService.AddAsync(entity)) result = false;
            if (result && !await base.Injector.NetMailService.Enviar(email)) result = false;
            if(result) await base.Injector.Rabbit.Producer(email, FilasRabbmit.Email.ToString());
            if (!result) await _emailService.RemoveAsync(entity.Id);
            return result;
        }
    }
}