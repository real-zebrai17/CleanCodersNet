using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders
{
    public class User : Entity
    {
        public string UserName { get; }

        public User(string userName)
        {
            UserName = userName;
        }
    }
}
