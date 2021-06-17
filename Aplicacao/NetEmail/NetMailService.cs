using Aplicacao.Binds;
using Aplicacao.Contratos;
using Aplicacao.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace Aplicacao.NetMail
{
    public class NetMailService : INetMailService
    {
        NetMailSettings _configuration;
        private readonly string _origem;
        public NetMailService(NetMailSettings configuration)
        {
            if (configuration is null) throw new System.Exception("Configurações de e-mail estão nulas.");
            _configuration = configuration;
            _origem = _configuration.Origem;
        }

        public bool Enviar(EmailDTO email)
        {
            try
            {
                using SmtpClient client = new SmtpClient(_configuration.Host, _configuration.Port);
                client.Credentials = new NetworkCredential(_configuration.Origem, _configuration.Senha);
                client.EnableSsl = true;
                using MailMessage mensagemEmail = new MailMessage(_origem, email.Destinatario, email.Assunto, email.Mensagem);
                client.Send(mensagemEmail);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}