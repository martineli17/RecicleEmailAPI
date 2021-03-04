using RabbitMQ.Client;
using System.Threading.Tasks;

public interface IRabbitFactory
{
    IModel CreateConnection();
    ResponseQueue CreateQueue(string fila, IModel channel = null);
    Task Producer(object request, string fila, IModel channel = null);
}