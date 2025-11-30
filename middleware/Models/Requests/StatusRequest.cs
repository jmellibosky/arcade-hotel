using HotelMiddleware.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMiddleware.Models.Requests
{
    public class StatusRequest : IRabbitMqRequest
    {
        public int id { get; set; }
    }
}
