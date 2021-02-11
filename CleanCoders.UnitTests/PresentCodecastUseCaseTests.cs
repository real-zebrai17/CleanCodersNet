using CleanCoders.Specs.TestDoubles;
using NUnit.Framework;
using System.Linq;

namespace CleanCoders.UnitTests
{
    [TestFixture]
    public class PresentCodecastUseCaseTests
    {
        private PresentCodecastUseCase _useCase;
        private User _user;
        private Codecast _codeCast;

        [SetUp]
        public void SetUp()
        {
            Context.Gateway = new MockGateway();

            _useCase = new PresentCodecastUseCase();
            _user = Context.Gateway.Save(new User("User"));
            _codeCast = Context.Gateway.Save(new Codecast());

        }


        [Test]
        public void UserWithoutViewLicense_CannotViewCodecast()
        {
            Assert.IsFalse(_useCase.IsLicensedToViewCodeCast(_user, _codeCast));
        }

        [Test]
        public void UserWithViewLicense_CanViewCodecast()
        {
            var licence = new License(_user, _codeCast);
            Context.Gateway.Save(licence);

            Assert.IsTrue(_useCase.IsLicensedToViewCodeCast(_user, _codeCast));
        }

        [Test]
        public void UserWithViewLicense_CanViewOtherUsersCodecast()
        {
            var otherUser = Context.Gateway.Save(new User("otherUser"));
            var licence = new License(_user, _codeCast);
            Context.Gateway.Save(licence);

            Assert.IsFalse(_useCase.IsLicensedToViewCodeCast(otherUser, _codeCast));
        }

        [Test]
        public void PresentingOneCodecast()
        {
            _codeCast.Title = "Some Title";
            _codeCast.PublicationDate = "Tomorrow";

            var presentedCodecasts = _useCase.GetPresentedCodecasts(_user);

            Assert.AreEqual(1, presentedCodecasts.Count());
            Assert.AreEqual("Some Title", presentedCodecasts.Single().Title);
            Assert.AreEqual("Tomorrow", presentedCodecasts.Single().PublicationDate);
        }

        [Test]
        public void PresentedCodeCastIsNotViewableIfNoLicense()
        {
            Assert.IsFalse(_useCase.GetPresentedCodecasts(_user).Single().IsViewable);
        }

        [Test]
        public void PresentedCodeCastIsViewableIfLicenseExists()
        {
            Context.Gateway.Save(new License(_user, _codeCast));
            Assert.IsTrue(_useCase.GetPresentedCodecasts(_user).Single().IsViewable);
        }


    }
}
