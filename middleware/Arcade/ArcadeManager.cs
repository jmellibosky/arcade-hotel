using HotelMiddleware.Common;
using HotelMiddleware.Models;
using HotelMiddleware.Models.Interfaces;
using HotelMiddleware.Models.Requests;
using HotelMiddleware.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelMiddleware.Arcade
{
    public class ArcadeManager
    {
        private RabbitMqManager _rabbitmq;
        private Config _config;
        private string[] _topics;
        private const string _coinTopic = "coin";
        private const string _defaultTopic = "default";
        private const string _statusTopic = "status";
        private int maxRetries = 10;

        public ArcadeManager(Config config)
        {
            _config = config;
            _rabbitmq = new RabbitMqManager().GetInstanceFromConfig(config);

            _topics = [ _coinTopic, _defaultTopic, _statusTopic ];
        }

        public void Run()
        {
            while (true)
            {
                string messageReq = "";
                while (string.IsNullOrEmpty(messageReq))
                {
                    messageReq = ReadRabbitMq();
                }
                
                IRabbitMqResponse response = null;
                string topic = "";
                if (messageReq.StartsWith(_coinTopic))
                {
                    CoinRequest? coinRequest = JsonConvert.DeserializeObject<CoinRequest>(messageReq.Replace(_coinTopic, ""));
                    if (coinRequest == null)
                    {
                        continue;
                    }

                    topic = _coinTopic;
                    response = ProcessCoinRequest(coinRequest);
                }
                else if (messageReq.StartsWith(_defaultTopic))
                {
                    DefaultRequest? defaultRequest = JsonConvert.DeserializeObject<DefaultRequest>(messageReq.Replace(_defaultTopic, ""));
                    if (defaultRequest == null)
                    {
                        continue;
                    }

                    topic = _defaultTopic;
                    response = ProcessDefaultRequest(defaultRequest);
                }
                else if (messageReq.StartsWith(_statusTopic))
                {
                    StatusRequest? statusRequest = JsonConvert.DeserializeObject<StatusRequest>(messageReq.Replace(_statusTopic, ""));
                    if (statusRequest == null)
                    {
                        continue;
                    }

                    topic = _statusTopic;
                    response = ProcessStatusRequest(statusRequest);
                }
                else
                {
                    continue;
                }

                if (response != null)
                {
                    string messageRes = ParseResponseMessage(response);
                    WriteRabbitMq(topic, messageRes);
                }
            }
        }

        #region Request Processing
        private CoinResponse ProcessCoinRequest(CoinRequest coinRequest)
        {
            CoinResponse response = null;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using (var stream = new FileStream(_config.LuaFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        writer.WriteLine(coinRequest.key);
                    }

                    response = new CoinResponse
                    {
                        id = coinRequest.id,
                        result = true,
                        message = $"Default request processed successfully after {i+1} tries."
                    };
                }
                catch (Exception)
                {
                    Thread.Sleep(20);
                }
            }

            if (response == null)
            {
                response = new CoinResponse
                {
                    id = coinRequest.id,
                    result = false,
                    message = "Failed to process coin request after maximum retries."
                };
            }

            return response;
        }

        private DefaultResponse ProcessDefaultRequest(DefaultRequest defaultRequest)
        {
            var response = new DefaultResponse
            {
                id = defaultRequest.id,
                result = true,
                message = "Default request processed successfully."
            };

            return response;
        }

        private StatusResponse ProcessStatusRequest(StatusRequest statusRequest)
        {
            var response = new StatusResponse
            {
                id = statusRequest.id,
                version = $"{_config.Version}_{_config.Id}_{DateTime.Now.ToString()}"
            };

            return response;
        }
        #endregion

        #region Parsing
        private string ParseResponseMessage(IRabbitMqResponse response)
        {
            return JsonConvert.SerializeObject(response);
        }
        #endregion

        #region RabbitMQ
        private string ReadRabbitMq()
        {
            return _rabbitmq.Read(_topics);
        }

        private void WriteRabbitMq(string topic, string message)
        {
            bool result = _rabbitmq.Write(topic, message);

            if (!result)
            {
                Console.WriteLine("Failed to write message to RabbitMQ");
            }
        }
        #endregion
    }
}
