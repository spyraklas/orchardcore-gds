using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchardCore.DSI.Config
{
    public interface IDfESignInPublicApiConfig
    {
        public string ClientId { get; set; }
        public string ServiceSecret { get; set; }
        public string ServiceUrl { get; set; }
        public string Cryptography { get; set; }
        public string ServiceAudience { get; set; }
    }
}
