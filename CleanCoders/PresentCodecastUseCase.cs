using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanCoders
{
    public class PresentCodecastUseCase
    {
        public List<PresentableCodeCast> GetPresentedCodecasts(User loggedInUser)
        {
            var presentableCodecasts = Context.Gateway.FindAllCodecasts()
                .Select(pcc => new PresentableCodeCast
                {
                    IsViewable      = IsLicensedToViewCodeCast(loggedInUser, pcc),
                    Title           = pcc.Title,
                    PublicationDate = pcc.PublicationDate
                });
            return presentableCodecasts.ToList();
        }

        public bool IsLicensedToViewCodeCast(User user, Codecast codeCast)
        {
            var licenses = Context.Gateway.FindLicensesForUserAndCodecasts(user, codeCast);
            return licenses.Any();
        }
    }
}
