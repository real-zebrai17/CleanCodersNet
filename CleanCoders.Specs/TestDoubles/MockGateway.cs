using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCoders.Specs.TestDoubles
{
    public class MockGateway : IGateway
    {
        private readonly List<Codecast> _codecasts;
        private readonly List<User> _users;

        public MockGateway()
        {
            _codecasts = new List<Codecast>();
            _users = new List<User>();
        }

        public void Delete(Codecast codecast)
        {
            _codecasts.Remove(codecast);
        }

        public List<Codecast> FindAllCodecasts()
        {
            return _codecasts;
        }

        public User FindUser(string userName)
        {
            return _users.SingleOrDefault(c => c.UserName == userName);
        }

        public void Save(Codecast codecast)
        {
            _codecasts.Add(codecast);
        }

        public void Save(User user)
        {
            _users.Add(user);
        }
    }
}
