using HotelMiddleware.Arcade;
using HotelMiddleware.Common;
using HotelMiddleware.Models;

Config config = ConfigurationManager.GetConfig();

if (config.HardwareType == HardwareType.ARCADE)
{
    ArcadeManager.Run(config);
}
else if (config.HardwareType == HardwareType.EXPENDEDORA)
{
    throw new NotImplementedException();
}