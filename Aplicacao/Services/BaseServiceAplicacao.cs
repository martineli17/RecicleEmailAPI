namespace Aplicacao.Services
{
    public class BaseServiceAplicacao
    {
        protected readonly InjectorAplicacao Injector;
        public BaseServiceAplicacao(InjectorAplicacao injector)
        {
            Injector = injector;
        }
    }
}