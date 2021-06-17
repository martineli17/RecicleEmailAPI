using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Contratos
{
    public interface IRabbit
    {
        Task<ResponseQueue> CreateQueue(string fila);
        Task Producer(object request, string fila);
        Task Consumer(Action<ResponseRabbitMQ> action, string fila, IModel channel = null);
    }
}