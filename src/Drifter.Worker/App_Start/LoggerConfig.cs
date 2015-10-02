namespace Drifter.Worker
{
    public static class LoggerConfig
    {
        internal static void ConfigureLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
