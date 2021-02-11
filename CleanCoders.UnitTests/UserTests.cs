using CleanCoders.Specs.TestDoubles;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCoders.UnitTests
{
    [TestFixture]
    public class UserTests
    {
        [SetUp]
        public void SetUp()
        {
            Context.Gateway = new MockGateway();
        }

        [Test]
        public void TwoDifferentUsersAreNotTheSame()
        {
            User u1 = new User("u1");
            User u2 = new User("u2");
            Context.Gateway.Save(u1);
            Context.Gateway.Save(u2);


            Assert.IsFalse(u1.IsSame(u2));
        }

        [Test]
        public void OneUserIsSameAsItself()
        {
            User u1 = new User("u1");
            Context.Gateway.Save(u1);

            Assert.IsTrue(u1.IsSame(u1));
        }

        [Test]
        public void UsersWithTheSameIDAreTheSame()
        {
            User u1 = new User("u1");
            Context.Gateway.Save(u1);

            User u2 = new User("u2");
            u2.Id = u1.Id;

            Assert.IsTrue(u1.IsSame(u2));

        }
    }
}
