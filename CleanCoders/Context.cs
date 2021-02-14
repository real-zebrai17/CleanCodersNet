using CleanCoders.Gateways;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders
{
    public static class Context
    {
        public static ILicenseGateway LicenseGateway { get; set; }
        public static IUserGateway UserGateway { get; set; }
        public static ICodecastGateway CodecastGateway { get; set; }
        public static GateKeeper GateKeeper { get; set; }
    }
}
