using Aplicacao.Binds;
using Aplicacao.Contratos;
using Crosscuting.Funcoes;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.RabbitMq
{
    public class Rabbit : IRabbit
    {
        private IModel _channel;
        public Rabbit(IOptions<ConnectionStrings> configuration)
        {
            _channel = CreateConnection(configuration?.Value?.RabbitMq);
        }

        public IModel CreateConnection(string connectionString)
        {
            var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
        }
        public ResponseQueue CreateQueue(string fila, IModel channel)
         => new ResponseQueue(channel.QueueDeclare(queue: fila,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null));
        public Task Producer(object request, string fila, IModel channel = null)
        {
            var canalEscolhido = channel ?? _channel;
            CreateQueue(fila, canalEscolhido);
            canalEscolhido.BasicPublish(string.Empty, fila, null, Encoding.UTF8.GetBytes(JsonFunc.SerializeObject(request)));
            return Task.FromResult(0);
        }
    }
}