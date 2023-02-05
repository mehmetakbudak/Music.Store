using Microsoft.Extensions.Configuration;

namespace Music.Store.Infrastructure
{
    public static class Global
    {        
        public static string ApiUrl { get; set; }


        public static void Initialize(IConfiguration configuration)
        {
            ApiUrl = configuration["ApiUrl"];
        }
    }
}
