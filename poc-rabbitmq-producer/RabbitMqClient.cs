using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace poc_rabbitmq_producer
{
    public class RabbitMqClient
    {
        private readonly string _rabbitMqUrl = "amqp://guest:guest@localhost:5672";
        private readonly string _queueName = "RabbitMqQueue-Guilherme-one";
        private readonly IConnection _connection;
        private readonly IModel _model;

        public RabbitMqClient()
        {
            _connection = CreateConnection();
            _model = _connection.CreateModel();
        }

        private IConnection CreateConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(this._rabbitMqUrl)
            };

            return connectionFactory.CreateConnection();
        }

        public void Publish(string message)
        {
            ConfigureQueue(_queueName, _model);
            _model.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: Encoding.UTF8.GetBytes(message));
        }

        public void Listen()
        {
            ConfigureQueue(_queueName, _model);
            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += (sender, @event) =>
            {
                var body = @event.Body;
                var message = Encoding.UTF8.GetString(body);

                if (!string.IsNullOrWhiteSpace(message))
                {
                    _model.BasicAck(@event.DeliveryTag, true);
                }

                Console.WriteLine($"Received message: {message}");
            };

            _model.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        private void ConfigureQueue(string name, IModel model)
        {
            model.QueueDeclare(queue: name, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }


    }

}
