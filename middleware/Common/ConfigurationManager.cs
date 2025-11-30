using HotelMiddleware.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMiddleware.Common
{
    public static class ConfigurationManager
    {
        public static Config GetConfig()
        {
            var configFile = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            Config config = new Config()
            {
                HardwareType = HardwareTypes.ConvertToHardwareType(configFile["AppSettings:HardwareType"]),
                Id = configFile["AppSettings:Id"]
            };

            return config;
        }
    }
}
