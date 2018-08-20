
namespace Utilities.Implementation
{
    using API;
    using DAL;

    public static class LoggerFactory
    {
        public static ILogger GetInstance => _logger ?? (_logger = new Logger(new FileWriter(new StreamWriterWrapperFactory()), new DateTimeWrapper()));

        private static ILogger _logger;
    }
}
