using CleanCoders.Specs.TestDoubles;
using NUnit.Framework;


namespace CleanCoders.UnitTests
{
    [TestFixture]
    public class EntityTests
    {
        [SetUp]
        public void SetUp()
        {
            Context.LicenseGateway = new InMemoryLicenseGateway();
        }

        [Test]
        public void TwoDifferentEntitysAreNotTheSame()
        {
            Entity e1 = new Entity();
            Entity e2 = new Entity();
            e1.Id = "e1Id";
            e2.Id = "e2Id";

            Assert.IsFalse(e1.IsSame(e2));
        }

        [Test]
        public void OneEntityIsSameAsItself()
        {
            Entity e1 = new Entity();
            e1.Id = "e1Id";

            Assert.IsTrue(e1.IsSame(e1));
        }

        [Test]
        public void EntitysWithTheSameIDAreTheSame()
        {
            Entity e1 = new Entity();
            e1.Id = "e1Id";

            Entity e2 = new Entity();
            e2.Id = e1.Id;

            Assert.IsTrue(e1.IsSame(e2));
        }

        [Test]
        public void EntityWithNullIdsAreNeverTheSame()
        {
            Entity u1 = new Entity();
            Entity u2 = new Entity();
            Assert.IsFalse(u1.IsSame(u2));
        }
    }
}
