using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders.Entities
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
