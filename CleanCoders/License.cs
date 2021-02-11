using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders
{
    public class License 
    {
        private readonly User _user;
        private readonly Codecast _codeCast;

        public License(User user, Codecast codeCast)
        {
            _user = user;
            _codeCast = codeCast;
        }

        public User User => _user;

        public Codecast CodeCast => _codeCast;
    }
}
