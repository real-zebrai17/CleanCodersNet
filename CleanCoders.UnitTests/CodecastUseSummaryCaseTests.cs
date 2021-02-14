using CleanCoders.Specs.TestDoubles;
using NUnit.Framework;
using System;
using System.Linq;
using static CleanCoders.License.LicenseType;


namespace CleanCoders.UnitTests
{
    [TestFixture]
    public class CodecastUseSummaryCaseTests
    {
        private CodecastSummaryUseCase _useCase;
        private User _user;
        private Codecast _codeCast;

        [SetUp]
        public void SetUp()
        {
            FixtureSetup.SetupContext();

            _useCase = new CodecastSummaryUseCase();
            _user = Context.UserGateway.Save(new User("User"));
            _codeCast = Context.CodecastGateway.Save(new Codecast());

        }


        [Test]
        public void UserWithoutViewLicense_CannotViewCodecast()
        {
            Assert.IsFalse(CodecastSummaryUseCase.IsLicensedFor(VIEWING, _user, _codeCast));
        }

        [Test]
        public void UserWithViewLicense_CanViewCodecast()
        {
            var licence = new License(VIEWING, _user, _codeCast);
            Context.LicenseGateway.Save(licence);

            Assert.IsTrue(CodecastSummaryUseCase.IsLicensedFor(VIEWING, _user, _codeCast));
        }

        [Test]
        public void UserWithViewLicense_CanViewOtherUsersCodecast()
        {
            var otherUser = Context.UserGateway.Save(new User("otherUser"));
            var licence = new License(VIEWING, _user, _codeCast);
            Context.LicenseGateway.Save(licence);

            Assert.IsFalse(CodecastSummaryUseCase.IsLicensedFor(VIEWING, otherUser, _codeCast));
        }

        [Test]
        public void PresentingOneCodecast()
        {
            var now = new DateTime(2014, 5, 19);
            _codeCast.Title = "Some Title";
            _codeCast.PublicationDate = now;

            var presentedCodecasts = _useCase.PresentCodeCasts(_user);

            Assert.AreEqual(1, presentedCodecasts.Count());
            Assert.AreEqual("Some Title", presentedCodecasts.Single().Title);
            Assert.AreEqual("5/19/2014", presentedCodecasts.Single().PublicationDate);
        }

        [Test]
        public void PresentedCodeCastIsNotViewableIfNoLicense()
        {
            Assert.IsFalse(_useCase.PresentCodeCasts(_user).Single().IsViewable);
        }

        [Test]
        public void PresentedCodeCastIsViewableIfViewableLicenseExists()
        {
            Context.LicenseGateway.Save(new License(VIEWING,  _user, _codeCast));
            Assert.IsTrue(_useCase.PresentCodeCasts(_user).Single().IsViewable);
        }

        [Test]
        public void PresentedCodeCastIsViewableIfDownloadLicenseExists()
        {
            Context.LicenseGateway.Save(new License(DOWNLOADING, _user, _codeCast));
            var presentableCodecast = _useCase.PresentCodeCasts(_user).Single();

            Assert.IsTrue(presentableCodecast.IsDownloadable);
            Assert.IsFalse(presentableCodecast.IsViewable);
        }
    }
}
