using CleanCoders.Specs.TestDoubles;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCoders.UnitTests
{
    [TestFixture]
    public class CodecastDetailsUseCaseTests
    {
        private User _user;

        [SetUp]
        public void SetUp()
        {
            FixtureSetup.SetupContext();
            _user = Context.UserGateway.Save(new User("User"));
        }

        [Test]
        public void CreatesCodecastDetailsPresentaion()
        {
            var codecast = Context.CodecastGateway.Save(new Codecast
            {
                Title = "Codecast",
                Permalink = "permalink-a",
                PublicationDate = DateTime.ParseExact("1/2/2015", "M/d/yyyy", CultureInfo.InvariantCulture)

            }); ;

            var details = new CodeCastDetailsUseCase()
                                        .RequestCodecastDetails(_user, "permalink-a");

            Assert.True(details.WasFound);
            Assert.AreEqual("Codecast", details.Title);
            Assert.AreEqual("1/2/2015", details.PublicationDate);
        }


        [Test]
        public void CreatesCodecastDetailsPresentaionDoesntCrashOnMissingCodecast()
        {

            var details = new CodeCastDetailsUseCase()
                                        .RequestCodecastDetails(_user, "missing");
            
            Assert.False(details.WasFound);
        }

    }
}
