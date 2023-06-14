using Microsoft.Extensions.Configuration;

namespace Music.Store.Infrastructure
{
    public static class Global
    {
        public static string ApiUrl { get; set; }
        public static string Secret { get; set; }
        public static string PortalUrl { get; set; }    


        public static void Initialize(IConfiguration configuration)
        {
            ApiUrl = configuration["ApiUrl"];
            Secret = configuration["Secret"];
            PortalUrl = configuration["PortalUrl"];
        }
    }
}
