using HotelMiddleware.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMiddleware.Models.Responses
{
    internal class StatusResponse : IRabbitMqResponse
    {
        public int id { get; set; }
        public string version { get; set; }
    }
}
