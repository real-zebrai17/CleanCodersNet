using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders
{
    public class User
    {
        private readonly string _userName;
        public string UserName => _userName;

        public string Id { get; set; }

        public User(string userName)
        {
            _userName = userName;
        }

        public bool IsSame(User user)
        {
            return Object.Equals(Id, user.Id);
        }
    }
}
