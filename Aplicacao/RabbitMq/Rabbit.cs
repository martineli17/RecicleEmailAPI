using Aplicacao.Contratos;
using Crosscuting.Funcoes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.RabbitMq
{
    public class Rabbit : IRabbit
    {
        private IConnection _connection;
        public Rabbit(string connection)
        {
            _connection = CreateConnection(connection);
        }
      
        public Task<ResponseQueue> CreateQueue(string fila)
        {
            var canalEscolhido = _connection.CreateModel();
            var queueCriada = new ResponseQueue(canalEscolhido.QueueDeclare(queue: fila,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null));
            canalEscolhido.Dispose();
            return Task.FromResult(queueCriada);
        }

        public Task Producer(object request, string fila)
        {
            var canalEscolhido = _connection.CreateModel();
            canalEscolhido.BasicPublish(string.Empty, fila, null, Encoding.UTF8.GetBytes(JsonFunc.SerializeObject(request)));
            canalEscolhido.Dispose();
            return Task.CompletedTask;
        }

        public Task Consumer(Action<ResponseRabbitMQ> action, string fila, IModel channel = null)
        {
            var canalEscolhido = channel ?? _connection.CreateModel();
            var consumer = new EventingBasicConsumer(canalEscolhido);
            consumer.Received += (sender, events) => HandlerRecebimento(action, canalEscolhido, sender, events);
            canalEscolhido.BasicConsume(fila, false, consumer);
            return Task.CompletedTask;
        }

        private void HandlerRecebimento(Action<ResponseRabbitMQ> action, IModel canal, 
                                        object value, BasicDeliverEventArgs events)
        {
            try
            {
                action.Invoke(new ResponseRabbitMQ(events));
                canal.BasicAck(events.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                canal.BasicNack(events.DeliveryTag, true, true);
            }
        }

        private IConnection CreateConnection(string connectionString)
        {
            var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
            return factory.CreateConnection();
        }
    }
}