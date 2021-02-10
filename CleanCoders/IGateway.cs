using System.Collections.Generic;

namespace CleanCoders
{
    public interface IGateway
    {
        List<Codecast> FindAllCodecasts();
        void Delete(Codecast cc);
        void Save(Codecast codecast);
        void Save(User user);
        User FindUser(string userName);
    }
}