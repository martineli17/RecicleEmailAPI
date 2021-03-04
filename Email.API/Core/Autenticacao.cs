
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace Email.API.Core
{
    public class Autenticacao : IAutenticacao
    {
        private readonly string _key;
        public Autenticacao(IConfiguration configuration)
        {
            _key = configuration.GetSection("ApiKey").Value;
        }

        public bool Autenticar(string value)
        {
            var keyRecebida = Encoding.UTF8.GetString(Convert.FromBase64String(value));
            var keyOriginal = Encoding.UTF8.GetString(Convert.FromBase64String(_key));
            return keyRecebida == keyOriginal;
        }
    }
}
