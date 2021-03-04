using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Aplicacao.Contratos
{
    public interface IRabbit
    {
        IModel CreateConnection(string connectionString);
        ResponseQueue CreateQueue(string fila, IModel channel);
        Task Producer(object request, string fila, IModel channel = null);
    }
}