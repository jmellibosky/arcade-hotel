using Microsoft.Extensions.Options;
using MQTTnet;
using System.Text;

namespace HotelAPI.Managers
{
    public class MqttPublisher
    {
        private readonly string _host = "127.0.0.1";
        private readonly int _port = 1883;
        private readonly string _user = "lyf";
        private readonly string _pass = "lyf";
        private readonly IMqttClient _client;
        private readonly MqttClientOptions _options;

        public MqttPublisher()
        {
            var MqttFactory = new MqttClientFactory();
            _client = MqttFactory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(_host, _port)
                .WithCredentials(_user, _pass)
                .WithCleanSession(false)
                .Build();
        }

        public async Task ConnectAsync()
        {
            if (!_client.IsConnected)
            {
                try
                {
                    await _client.ConnectAsync(_options, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task PublishMessageAsync(string topic, string message)
        {
            await ConnectAsync();

            var MqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(message))
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            await _client.PublishAsync(MqttMessage, CancellationToken.None);
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
