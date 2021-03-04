using Aplicacao.Binds;
using Aplicacao.Contratos;
using Aplicacao.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Aplicacao.NetMail
{
    public class NetMailService : INetMailService
    {
        SmtpClient _client;
        NetMailSettings _configuration;
        public NetMailService(IOptions<NetMailSettings> configuration)
        {
            if (configuration is null) throw new System.Exception("Configurações de e-mail estão nulas.");
            _configuration = configuration.Value;
            _client = new SmtpClient(_configuration.Host, _configuration.Port);
            _client.Credentials = new NetworkCredential(_configuration.Origem, _configuration.Senha);
            _client.EnableSsl = true;
        }

        public Task<bool> Enviar(EmailDTO email)
        {
            try
            {
                using MailMessage mensagemEmail = new MailMessage(_configuration.Origem, email.Destinatario, email.Assunto, email.Mensagem);
                _client.Send(mensagemEmail);
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }
    }
}