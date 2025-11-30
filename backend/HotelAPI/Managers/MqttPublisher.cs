using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Formatter;
using System.Text;

namespace HotelAPI.Managers
{
    public class MqttPublisher
    {
        private readonly string _host = "localhost";
        private readonly int _port = 1883;
        private readonly string _user = "admin";
        private readonly string _pass = "admin";
        private readonly IMqttClient _client;
        private readonly MqttClientOptions _options;

        public MqttPublisher()
        {
            var MqttFactory = new MqttClientFactory();
            _client = MqttFactory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithClientId("hotel-api")
                .WithTcpServer(_host, _port)
                .WithCredentials(_user, _pass)
                .WithCleanSession(false)
                .WithProtocolVersion(MqttProtocolVersion.V311)
                .Build();
        }

        public async Task ConnectAsync()
        {
            if (!_client.IsConnected)
            {
                await _client.ConnectAsync(_options, CancellationToken.None);
            }
        }

        public async Task PublishMessageAsync(string topic, string message)
        {
            try
            {
                await ConnectAsync();

                var MqttMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(Encoding.UTF8.GetBytes(message))
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build();

                await _client.PublishAsync(MqttMessage, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            if (_client.IsConnected)
            {
                await _client.DisconnectAsync();
            }
        }
    }
}
