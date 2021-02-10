using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders
{
    public class GateKeeper
    {
        public User LoggerInUser { get; private set; }
        public void SetCurrentUser(User loggedInUser)
        {
            LoggerInUser = loggedInUser;
        }
    }
}
