using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ResponseQueue
{
    public string Nome { get; set; }
    public uint QuantidadeConsumidores { get; set; }
    public uint QuantidadeMensagens { get; set; }
    public ResponseQueue(QueueDeclareOk queueDeclare)
    {
        Nome = queueDeclare.QueueName;
        QuantidadeConsumidores = queueDeclare.ConsumerCount;
        QuantidadeMensagens = queueDeclare.MessageCount;
    }
    public ResponseQueue()
    {

    }
}