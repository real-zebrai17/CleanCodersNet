using System.Collections.Generic;

namespace CleanCoders
{
    public interface IGateway
    {
        List<Codecast> FindAllCodecastsSortedChronologically();
        void Delete(Codecast cc);
        Codecast Save(Codecast codecast);
        User Save(User user);
        User FindUserByUserName(string userName);
        Codecast FindCodecastByTitle(string codeCastTitle);
        void Save(License license);
        IEnumerable<License> FindLicensesForUserAndCodecasts(User user, Codecast codeCast);
    }
}