using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders
{
    public class User : Entity
    {
        private readonly string _userName;
        public string UserName => _userName;

        public User(string userName)
        {
            _userName = userName;
        }
    }
}
