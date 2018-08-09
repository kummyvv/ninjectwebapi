using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.Configuration
{
    public class Configuration
    {
        public int CONFIGURATION_ID { get; set; }
        public int PACKAGE_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; } = string.Empty;
        public string CUSTOMER_NAME { get; set; }
        public string REMARKS { get; set; }
        public string COMMENTS { get; set; }
        public int UPDT_USER_ID { get; set; }
    }
}
