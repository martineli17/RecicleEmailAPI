namespace Service
{
    public class BaseService
    {
        protected Injector Injector;
        public BaseService(Injector injector)
        {
            Injector = injector;
        }
    }
}