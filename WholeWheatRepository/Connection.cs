using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeWheatRepository
{
    public static class Connection
    {
        public static string LiveConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["live_dbName"].ToString();
        }
    }
}
