using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApp.Models
{
    class AppConfig
    {
        public static string getAppSettingKey(string key)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();
            var connectionString = Configuration[key];
            return connectionString;
        }
    }

}