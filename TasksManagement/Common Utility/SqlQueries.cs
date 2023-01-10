using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksManagement.Common_Utility
{
    public class SqlQueries
    {
        public static IConfiguration _configuration = new ConfigurationBuilder()
                                                          .AddXmlFile("SqlQueries.xml", true, true).Build();

        public static string CreateRecord { get { return _configuration["CreateRecord"]; } }
        public static string GetRecord { get { return _configuration["GetRecord"]; } }
        public static string GetRecordById { get { return _configuration["GetRecordById"]; } }
        public static string UpdateRecord { get { return _configuration["UpdateRecord"]; } }
        public static string DeleteRecord { get { return _configuration["DeleteRecord"]; } }


    }
}