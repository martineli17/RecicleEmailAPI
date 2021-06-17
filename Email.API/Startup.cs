using Aplicacao.Binds;
using Aplicacao.Contratos;
using Aplicacao.NetMail;
using Aplicacao.RabbitMq;
using Aplicacao.Services;
using Email.API.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
            services.AddScoped<IAutenticacao, Autenticacao>();
            services.AddHostedService<EmailBackgroundService>();
            services.AddTransient<INetMailService, NetMailService>(x => 
                new NetMailService(x.GetRequiredService<IOptions<NetMailSettings>>().Value));
            services.AddTransient<IEmailServiceAplicacao, EmailServiceAplicacao>();
            services.AddSingleton<IEmailRabbitObservable, EmailRabbitObservable>();
            services.AddTransient<IRabbit, Rabbit>(x => new Rabbit
                (x.GetRequiredService<IOptions<ConnectionStrings>>().Value.RabbitMq));
            services.AddTransient<InjectorAplicacao>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
