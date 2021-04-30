using Aplicacao.Binds;
using Aplicacao.Contratos;
using Aplicacao.DTO;
using Aplicacao.Filas;
using Aplicacao.Mappers;
using Aplicacao.NetMail;
using Aplicacao.RabbitMq;
using Aplicacao.Services;
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

namespace Aplicacao.Binds
{
    public class ServiceProviderEmailFactory
    {
        public static IServiceProvider Create(IOptions<ConnectionStrings> optionsConnection, 
                                            IOptions<NetMailSettings> optionsEmail)
        {
            var services = new ServiceCollection();
            services.AddDbContext<ContextEmail>(opt => opt.UseSqlServer(optionsConnection.Value.SqlServer)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseLoggerFactory(LoggerFactory.Create(p => p.AddFilter("Entity", LogLevel.Debug)))
                    .EnableSensitiveDataLogging().EnableDetailedErrors());
            services.AddTransient<INetMailService, NetMailService>(x => new NetMailService(optionsEmail));
            services.AddTransient<IEmailRepositorio, EmailRepositorio>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailServiceAplicacao, EmailServiceAplicacao>();
            services.AddSingleton<IEmailRabbitObservable, EmailRabbitObservable>();
            services.AddTransient<IRabbit, Rabbit>(x => new Rabbit(optionsConnection.Value.RabbitMq));
            services.AddTransient<Injector>();
            services.AddTransient<InjectorAplicacao>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(EmailMapper).Assembly);
            return services.BuildServiceProvider();
        }
    }
}
