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
        private readonly List<License> _licenses;

        public MockGateway()
        {
            _codecasts = new List<Codecast>();
            _users = new List<User>();
            _licenses = new List<License>();
        }

        public void Delete(Codecast codecast)
        {
            _codecasts.Remove(codecast);
        }

        public List<Codecast> FindAllCodecasts()
        {
            return _codecasts;
        }

        public Codecast FindCodecastByTitle(string codeCastTitle)
        {
            return _codecasts.SingleOrDefault(c => c.Title == codeCastTitle);
        }

        public IEnumerable<License> FindLicensesForAndCodecasts(User user, Codecast codeCast)
        {
            return _licenses.Where(c => c.User.IsSame(user) && c.CodeCast.IsSame(codeCast));
        }

        public User FindUserByUserName(string userName)
        {
            return _users.SingleOrDefault(c => c.UserName == userName);
        }

        public void Save(Codecast codecast)
        {
            _codecasts.Add(codecast);
        }

        public void Save(User user)
        {
            _users.Add(EstablishId(user));
        }

        private User EstablishId(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Id))
                user.Id = Guid.NewGuid().ToString();
            return user;
        }

        public void Save(License license)
        {
            _licenses.Add(license);
        }
    }
}
