using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCoders.Specs.TestDoubles
{
    public class InMemoryLicenseGateway : GatewayUtilities<License>, ILicenseGateway
    {
        public IEnumerable<License> FindLicensesForUserAndCodecasts(User user, Codecast codeCast)
        {
            return Entities.Where(c => c.User.IsSame(user) && c.CodeCast.IsSame(codeCast));
        }
    }
}
