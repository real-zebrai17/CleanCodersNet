using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanCoders
{
    public class PresentCodecastUseCase
    {
        public List<PresentableCodeCast> getPresentedCodecasts(User loggedInUser)
        {
            return new List<PresentableCodeCast>();
        }

        public bool IsLicensedToViewCodeCast(User user, Codecast codeCast)
        {
            var licenses = Context.Gateway.FindLicensesForAndCodecasts(user, codeCast);
            return licenses.Any();
        }
    }
}
