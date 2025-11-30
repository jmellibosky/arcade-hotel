using RabbitMQ.Client;

namespace HotelAPI.Managers
{
    public static class RabbitMqManager
    {
        private static IModel GetChannel(IConnection connection, string queueName)
        {
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            return channel;
        }

        private static void Publish(IModel channel, string message, string queueName)
        {
            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: System.Text.Encoding.UTF8.GetBytes(message));
        }

        public static void Publish(IConnection connection, string queueName, string message)
        {
            using var channel = GetChannel(connection, queueName);
            Publish(channel, message, queueName);
        }
    }
}
