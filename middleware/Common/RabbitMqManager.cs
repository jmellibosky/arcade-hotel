using HotelMiddleware.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMiddleware.Common
{
    public class RabbitMqManager
    {
        private string _host;
        private string _port;
        private string _username;
        private string _password;
        private string _prefix;

        public RabbitMqManager GetInstanceFromConfig(Config config)
        {
            return new RabbitMqManager(
                config.Host,
                config.Port,
                config.Username,
                config.Password,
                config.Id.ToLower()
                );
        }

        public RabbitMqManager() { }

        public RabbitMqManager(string host, string port, string username, string password, string prefix)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _prefix = prefix;
        }

        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _host,
                Port = int.Parse(_port),
                UserName = _username,
                Password = _password
            };

            return factory.CreateConnection();
        }

        public bool Write(string topic, string message)
        {
            try
            {
                using var connection = GetConnection();
                using var channel = connection.CreateModel();

                var fullTopic = $"{_prefix}.{topic}";

                channel.QueueDeclare(
                    queue: fullTopic,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: fullTopic,
                    basicProperties: null,
                    body: body
                );

                return true;
            }
            catch
            {
                return false;
            }
        }

        // ------------------------------------
        // LEER UN MENSAJE DE UN TOPIC
        // ------------------------------------
        public string Read(string topic)
        {
            try
            {
                var fullTopic = $"{_prefix}.{topic}";

                using var connection = GetConnection();
                using var channel = connection.CreateModel();

                // 👇 EVITA ERROR: si no existe, la crea.
                channel.QueueDeclare(
                    queue: fullTopic,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var result = channel.BasicGet(fullTopic, autoAck: true);

                if (result == null)
                    return null;

                return Encoding.UTF8.GetString(result.Body.ToArray());
            }
            catch
            {
                return null;
            }
        }


        // ------------------------------------
        // LEER VARIOS TOPICS
        // ------------------------------------
        public string Read(string[] topics)
        {
            foreach (var t in topics)
            {
                var msg = Read(t);
                if (msg != null)
                    return msg;
            }
            return null;
        }
    }
}
