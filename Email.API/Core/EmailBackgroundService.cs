using Aplicacao.Contratos;
using Aplicacao.DTO;
using Aplicacao.Filas;
using Crosscuting.Funcoes;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Core
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IEmailRabbitObservable _observaleEmail;
        private readonly IRabbit _rabbit;
        private readonly IEmailServiceAplicacao _emailService;
        public EmailBackgroundService(IEmailServiceAplicacao emailService,
                                    IEmailRabbitObservable observaleEmail,
                                    IRabbit rabbit)
        {
            _emailService = emailService;
            _observaleEmail = observaleEmail;
            _rabbit = rabbit;
            _observaleEmail.Subscribe(_emailService);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbit.CreateQueue(FilasRabbit.EMAIL_ENVIAR);
            await _rabbit.CreateQueue(FilasRabbit.EMAIL_ENVIADOS_COLETA);
            await _rabbit.Consumer(GetMensageriaEmail, FilasRabbit.EMAIL_ENVIAR);
        }

        private void GetMensageriaEmail(ResponseRabbitMQ dados)
        {
            if (dados is null || dados.Body.IsEmpty) return;
            var email = JsonFunc.DeserializeObject<EmailDTO>(Encoding.UTF8.GetString(dados.Body.ToArray()));
            if (email != null) _observaleEmail.Send(email).GetAwaiter();
        }
    }
}
