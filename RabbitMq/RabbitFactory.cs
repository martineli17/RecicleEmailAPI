using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

public class RabbitFactory : IRabbitFactory
{
    private IModel _channel;
    private string _fila;
    public RabbitFactory()
    {
        _channel = CreateConnection();
    }

    public IModel CreateConnection()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        var connection = factory.CreateConnection();
        return connection.CreateModel();
    }
    public ResponseQueue CreateQueue(string fila, IModel channel = null)
     => channel is null ? new ResponseQueue(_channel.QueueDeclare(queue: _fila,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null))
        : new ResponseQueue(channel.QueueDeclare(queue: _fila,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null));
    public Task Producer(object request, string fila = null, IModel channel = null)
    {
        var canalEscolhido = channel ?? _channel;
        if(fila is not null) CreateQueue(fila, channel is null ? null : channel);
        var body = Encoding.UTF8.GetBytes(JsonFunc.SerializeObject(request));
        canalEscolhido.BasicPublish(string.Empty, fila, null, body);
        return Task.FromResult(0);
    }
}