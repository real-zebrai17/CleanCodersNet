using CleanCoders.Entities;
using System.Collections.Generic;

namespace CleanCoders.Gateways
{
    public interface ILicenseGateway
    {
        License Save(License license);
        IEnumerable<License> FindLicensesForUserAndCodecasts(User user, Codecast codeCast);
    }
}