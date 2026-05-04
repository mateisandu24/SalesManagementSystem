using System.Configuration;

namespace SalesManagementSystem.Utils
{
    public static class ConfigHelper
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["SalesDb"].ConnectionString;
    }
}