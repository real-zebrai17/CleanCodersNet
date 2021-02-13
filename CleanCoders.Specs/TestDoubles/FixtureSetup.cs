using System;
using static CleanCoders.License.LicenseType;

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

        public static void SetupSampleData()
        {
            SetupContext();

            var bob = new User("Bob");
            var micah = new User("Micah");
            Context.UserGateway.Save(bob);
            Context.UserGateway.Save(micah);


            Codecast e1 = new Codecast { Title = "Episode 1 - The Beginning", PublicationDate = DateTime.Now };
            Codecast e2 = new Codecast { Title = "Episode 2 - The Continuation", PublicationDate =  e1.PublicationDate.AddMilliseconds(1) };
            Context.CodecastGateway.Save(e1);
            Context.CodecastGateway.Save(e2);

            License bobE1 = new License(VIEWING, bob, e1);
            License bobE2 = new License(VIEWING, bob, e2);
            Context.LicenseGateway.Save(bobE1);
            Context.LicenseGateway.Save(bobE2);


        }
    }
}
