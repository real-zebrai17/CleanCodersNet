using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCoders.Specs.TestDoubles
{
    public class InMemoryUserGateway : GatewayUtilities<User>, IUserGateway
    {
        public User FindUserByUserName(string userName)
        {
            return Entities.SingleOrDefault(c => c.UserName == userName);
        }
    }
}