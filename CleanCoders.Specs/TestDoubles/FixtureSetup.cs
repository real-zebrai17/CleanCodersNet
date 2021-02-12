using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCoders.Specs.TestDoubles
{
    public class FixtureSetup
    {
        public static void SetupContext()
        {
            Context.LicenseGateway = new InMemoryLicenseGateway();
            Context.UserGateway = new InMemoryUserGateway();
            Context.CodecastGateway = new InMemoryCodecastGateway();
            Context.GateKeeper = new GateKeeper();
        }
    }
}
