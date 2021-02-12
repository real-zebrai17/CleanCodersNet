using System.Collections.Generic;

namespace CleanCoders
{
    public interface ILicenseGateway
    {
        License Save(License license);
        IEnumerable<License> FindLicensesForUserAndCodecasts(User user, Codecast codeCast);
    }
}