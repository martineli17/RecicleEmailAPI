using Aplicacao.Binds;
using Aplicacao.Contratos;
using Aplicacao.Mappers;
using Aplicacao.NetMail;
using Aplicacao.RabbitMq;
using Aplicacao.Services;
using Dominio.Contratos.Repositorios;
using Dominio.Contratos.Services;
using Dominio.Contratos.UnitOfWork;
using Email.API.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Repositorios;
using Service;

namespace Email.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<NetMailSettings>(Configuration.GetSection("MailSetting"));
            services.AddDbContext<ContextEmail>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseLoggerFactory(LoggerFactory.Create(p => p.AddConsole()))
                    .EnableSensitiveDataLogging().EnableDetailedErrors());
            services.AddScoped<INetMailService, NetMailService>();
            services.AddScoped<IEmailRepositorio, EmailRepositorio>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailServiceAplicacao, EmailServiceAplicacao>();
            services.AddScoped<IRabbit, Rabbit>();
            services.AddScoped<Injector>();
            services.AddScoped<InjectorAplicacao>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(EmailMapper).Assembly);
            services.AddScoped<IAutenticacao, Autenticacao>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
