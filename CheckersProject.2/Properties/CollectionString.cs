using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CheckersProject._2
{
    class CollectionString
    {
        public static string ConnStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CheckersProject._2.Properties.Settings.ConnStr"].ConnectionString;
            }
        }


    }
}
