using System;
using System.Text;
using CategoryService.DTOs.Requests;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace CategoryService.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        public QueueRepository()
        {
        }

        public bool Add(CreateCategoryRequest request)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: "CategoryQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var message = JsonConvert.SerializeObject(request);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "CategoryQueue",
                basicProperties: null,
                body: body
                );

            return true;
        }

        public bool Read()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: "CategoryQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, mq) =>
            {
                var body = mq.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var request = JsonConvert.DeserializeObject<CreateCategoryRequest>(message);
                Log.Information(request.name + " category read from queue...");
            };

            channel.BasicConsume(
                queue: "CategoryQueue",
                autoAck: false,
                consumer: consumer
                );

            return true;
        }
    }
}
