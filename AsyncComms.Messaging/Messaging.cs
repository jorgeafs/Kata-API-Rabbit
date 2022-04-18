using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AsyncComms.Messaging
{
    public interface IMessaging
    {
        void SendMessage(string message);
        void SendSingleMessage(string message, string queueId);
        void StartReceiver();
        string ReceiveOne(string queueId);
        void ConfirmMessage(ulong id);
    }

    public class Messaging : IMessaging
    {
        private readonly IModel _channel;
        public ConcurrentQueue<string> Messages = new ConcurrentQueue<string>();
        public ConcurrentDictionary<ulong, string> MessagesWithIds = new ConcurrentDictionary<ulong, string>();

        public Messaging()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        public void SendMessage(string message)
        {

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);

        }

        public void SendSingleMessage(string message, string queueId)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
             var channel = connection.CreateModel();
             channel.QueueDeclare(queue: queueId,
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);

        }

        public void StartReceiver()
        {
            _channel.BasicQos(0,10,false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                MessagesWithIds.TryAdd(ea.DeliveryTag,message);
            };
            _channel.BasicConsume(queue: "hello",
                autoAck: false,
                consumer: consumer);
        }

        public string ReceiveOne(string queueId)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueId,
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(_channel);
            var needToWait = true;
            var response = string.Empty;
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                response = Encoding.UTF8.GetString(body);
                needToWait = false;
            };
            
            channel.BasicConsume(queue: queueId,
                autoAck: true,
                consumer: consumer);
            
            while(needToWait)
            {Thread.Sleep(30);}

            return response;
        }

        public void ConfirmMessage(ulong id)
        {
            try
            {
                _channel.BasicAck(id, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            MessagesWithIds.Remove(id,out var message);
        }
    }
}
