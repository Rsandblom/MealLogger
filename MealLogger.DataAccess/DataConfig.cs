using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MealLogger.DataAccess
{
    public static class DataConfig
    {
        public static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string strConnection = builder.Build().GetConnectionString("DbConnection");

            return strConnection;
        }
        
    }
}

