using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMiddleware.Models
{
    public enum HardwareType
    {
        ARCADE,
        EXPENDEDORA
    }

    public class HardwareTypes
    {
        public static HardwareType ConvertToHardwareType(string config)
        {
            config = config.ToLower();

            if (config.Equals("arcade"))
            {
                return HardwareType.ARCADE;
            }
            else if (config.Equals("expendedora"))
            {
                return HardwareType.EXPENDEDORA;
            }
            else
            {
                throw new Exception("No se encontró un HardwareType válido");
            }
        }
    }
}
