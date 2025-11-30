using HotelMiddleware.Models;
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

        public static RabbitMqManager GetInstanceFromConfig(Config config)
        {
            throw new NotImplementedException();
        }

        public RabbitMqManager(string host, string port, string username, string password, string prefix)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _prefix = prefix;
        }

        public string Read(string topic)
        {
            throw new NotImplementedException();
        }

        public string Read(string[] topics)
        {
            throw new NotImplementedException();
        }

        public bool Write(string topic, string message)
        {
            throw new NotImplementedException();
        }
    }
}
