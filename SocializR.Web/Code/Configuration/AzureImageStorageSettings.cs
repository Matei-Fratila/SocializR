using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocializR.Web.Code.Configuration
{
    public class AzureImageStorageSettings
    {
        public string BaseUri { get; set; }
        public string AccountName { get; set; }
        public string KeyValue { get; set; }
        public string ContainerName { get; set; }
    }
}
