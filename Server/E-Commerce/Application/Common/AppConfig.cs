using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class AppConfig
    {
        
        public AppConfig() { }

        public string ApiKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string FilePath { get; set; }


    }
}
