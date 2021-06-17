using Aplicacao.Contratos;
using AutoMapper;

namespace Aplicacao.Services
{
    public class InjectorAplicacao
    {
        public readonly INetMailService NetMailService;
        public readonly IRabbit Rabbit;
        public InjectorAplicacao(IRabbit rabbit, INetMailService netMailService)
        {
            Rabbit = rabbit;
            NetMailService = netMailService;
        }
    }
}