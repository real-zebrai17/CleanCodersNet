using System.Collections.Generic;

namespace CleanCoders
{
    public interface IUserGateway
    {
        User FindUserByUserName(string userName);
        User Save(User user);
    }
}