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

        public List<Codecast> FindAllCodecastsSortedChronologically()
        {
            return _codecasts.OrderBy(c=>c.PublicationDate)
                             .ToList();
        }

        public Codecast FindCodecastByTitle(string codeCastTitle)
        {
            return _codecasts.SingleOrDefault(c => c.Title == codeCastTitle);
        }

        public IEnumerable<License> FindLicensesForUserAndCodecasts(User user, Codecast codeCast)
        {
            return _licenses.Where(c => c.User.IsSame(user) && c.CodeCast.IsSame(codeCast));
        }

        public User FindUserByUserName(string userName)
        {
            return _users.SingleOrDefault(c => c.UserName == userName);
        }

        public Codecast Save(Codecast codecast)
        {
            _codecasts.Add((Codecast)EstablishId(codecast));
            return codecast;
        }

        public User Save(User user)
        {
            _users.Add((User)EstablishId(user));
            return user;
        }

        private Entity EstablishId(Entity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
                entity.Id = Guid.NewGuid().ToString();
            return entity;
        }

        public void Save(License license)
        {
            _licenses.Add(license);
        }
    }
}
