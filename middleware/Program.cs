using HotelMiddleware.Arcade;
using HotelMiddleware.Common;
using HotelMiddleware.Models;

Config config = ConfigurationManager.GetConfig();

if (config.HardwareType == HardwareType.ARCADE)
{
    new ArcadeManager(config).Run();
}
else if (config.HardwareType == HardwareType.EXPENDEDORA)
{
    throw new NotImplementedException();
}