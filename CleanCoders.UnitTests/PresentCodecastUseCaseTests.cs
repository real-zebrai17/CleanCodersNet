using CleanCoders.Specs.TestDoubles;
using NUnit.Framework;


namespace CleanCoders.UnitTests
{
    [TestFixture]
    public class PresentCodecastUseCaseTests
    {
        private PresentCodecastUseCase _userCase;
        private User _user;
        private Codecast _codeCast;

        [SetUp]
        public void SetUp()
        {
            _userCase = new PresentCodecastUseCase();

            _user = new User("User");
            _codeCast = new Codecast();

            Context.Gateway = new MockGateway();
        }


        [Test]
        public void UserWithoutViewLicense_CannotViewCodecast()
        {
            Assert.IsFalse(_userCase.IsLicensedToViewCodeCast(_user, _codeCast));
        }

        [Test]
        public void UserWithViewLicense_CanViewCodecast()
        {
            var licence = new License(_user, _codeCast);
            Context.Gateway.Save(licence);

            Assert.IsTrue(_userCase.IsLicensedToViewCodeCast(_user, _codeCast));
        }

        [Test]
        public void UserWithViewLicense_CanViewOtherUsersCodecast()
        {
            var otherUser = new User("otherUser");
            var licence = new License(_user, _codeCast);
            Context.Gateway.Save(licence);

            Assert.IsFalse(_userCase.IsLicensedToViewCodeCast(otherUser, _codeCast));
        }
    }
}
