using HotelMiddleware.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMiddleware.Models.Requests
{
    internal class DefaultRequest : IRabbitMqRequest
    {
        public int id { get; set; }
        public string file { get; set; }
        public bool restart { get; set; }
    }
}
