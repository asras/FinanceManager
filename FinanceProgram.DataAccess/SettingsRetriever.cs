using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FinanceProgram.DataAccess
{
    public static class SettingsRetriever
    {
        private static NameValueCollection Settings = (NameValueCollection)ConfigurationManager.GetSection("SaveSettings");

        public static string SaveLocation => Settings["SaveLocation"];
    }
}
