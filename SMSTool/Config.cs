using System.Configuration;
namespace SMSTool
{
    public class Config
    {
        internal static readonly string COM_PORT        = ConfigurationManager.AppSettings["COM_PORT"];
        internal static readonly bool USE_SIM           = bool.Parse(ConfigurationManager.AppSettings["USE_SIM"]);
        internal static readonly int SERVICE_FREQUENCY  = int.Parse(ConfigurationManager.AppSettings["SERVICE_FREQUENCY"]);
        internal static readonly int SERVICE_DELAY_START= int.Parse(ConfigurationManager.AppSettings["SERVICE_DELAY_START"]);
        internal static readonly string API_URL         = ConfigurationManager.AppSettings["API_URL"];
    }
}
