using CleanCoders.Entities;

namespace CleanCoders.Gateways
{
    public interface IUserGateway
    {
        User FindUserByUserName(string userName);
        User Save(User user);
    }
}