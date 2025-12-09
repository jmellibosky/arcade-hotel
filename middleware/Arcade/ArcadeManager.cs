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

        public ArcadeManager(Config config)
        {
            _config = config;
            _rabbitmq = new RabbitMqManager().GetInstanceFromConfig(config);
            _topics = [ "coin", "default", "status" ];
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
                
                Console.WriteLine("\n\nMensaje recibido: " + messageReq);
                IRabbitMqRequest request = ParseRequestMessage(messageReq);
                IRabbitMqResponse response = ProcessRequest(request);

                string messageRes = ParseResponseMessage(response);
                WriteRabbitMq(messageRes);
            }
        }

        #region Request Processing
        private IRabbitMqResponse ProcessRequest(IRabbitMqRequest request)
        {
            if (request == null)
            {
                //throw new Exception("El request fue nulo.");
                Console.WriteLine("El request fue nulo.");
                return null;
            }
            if (request.GetType() == typeof(CoinRequest))
            {
                return ProcessCoinRequest(request as CoinRequest);
            }
            else if (request.GetType() == typeof(DefaultRequest))
            {
                return ProcessDefaultRequest(request as DefaultRequest);
            }
            else if (request.GetType() == typeof(StatusRequest))
            {
                return ProcessStatusRequest(request as StatusRequest);
            }
            else
            {
                //throw new Exception("No se encontró el tipo de request.");
                Console.WriteLine("No se encontró el tipo de request.");
                return null;
            }
        }

        private StatusResponse ProcessStatusRequest(StatusRequest statusRequest)
        {
            throw new NotImplementedException();
        }

        private DefaultResponse ProcessDefaultRequest(DefaultRequest defaultRequest)
        {
            throw new NotImplementedException();
        }

        private CoinResponse ProcessCoinRequest(CoinRequest coinRequest)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Parsing
        private IRabbitMqRequest ParseRequestMessage(string message)
        {
            if (message.StartsWith("coin"))
            {
                return JsonConvert.DeserializeObject<CoinRequest>(message.Replace("coin", ""));
            }
            else if (message.StartsWith("default"))
            {
                return JsonConvert.DeserializeObject<DefaultRequest>(message.Replace("default", ""));
            }
            else if (message.StartsWith("status"))
            {
                return JsonConvert.DeserializeObject<StatusRequest>(message.Replace("status", ""));
            }
            else {
                //throw new Exception("No se encontró el tipo de request.");
                Console.WriteLine("No se encontró el tipo de request.");
                return null;
            }
        }

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

        private void WriteRabbitMq(string message)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
