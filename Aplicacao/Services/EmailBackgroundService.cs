using Aplicacao.Binds;
using Aplicacao.Contratos;
using Aplicacao.DTO;
using Aplicacao.Filas;
using Aplicacao.Mappers;
using Aplicacao.NetMail;
using Aplicacao.RabbitMq;
using Crosscuting.Funcoes;
using Dominio.Contratos.Repositorios;
using Dominio.Contratos.Services;
using Dominio.Contratos.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository;
using Repository.Repositorios;
using Service;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacao.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IEmailRabbitObservable _observaleEmail;
        private readonly IRabbit _rabbit;
        private readonly IEmailServiceAplicacao _emailService;
        public EmailBackgroundService(IOptions<ConnectionStrings> optionsConnection, IOptions<NetMailSettings> optionsEmail)
        {
            
            IServiceProvider provider = ServiceProviderEmailFactory.Create(optionsConnection, optionsEmail);
            _rabbit = (IRabbit)provider.GetService(typeof(IRabbit));
            _emailService = (IEmailServiceAplicacao)provider.GetService(typeof(IEmailServiceAplicacao));
            _observaleEmail = (IEmailRabbitObservable)provider.GetService(typeof(IEmailRabbitObservable));
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
