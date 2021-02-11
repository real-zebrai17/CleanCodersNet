using System.Collections.Generic;

namespace CleanCoders
{
    public interface IGateway
    {
        List<Codecast> FindAllCodecasts();
        void Delete(Codecast cc);
        void Save(Codecast codecast);
        void Save(User user);
        User FindUserByUserName(string userName);
        Codecast FindCodecastByTitle(string codeCastTitle);
        void Save(License license);
        IEnumerable<License> FindLicensesForAndCodecasts(User user, Codecast codeCast);
    }
}