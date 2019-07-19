using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp
{
    public class AppSettings
    {
        public string Company { get; set; }

        public string Location { get; set; }

        public string Username { get; set; }

        public string SqlConnection { get; set; }

        public UserInfo UserInfo { get; set; }

        public AppData AppData { get; set; }
    }

    public class AppData
    {
        public double Version { get; set; }

        public string Type { get; set; }


    }
}
