using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static CleanCoders.License.LicenseType;

namespace CleanCoders
{
    public class PresentCodecastUseCase
    {
        public List<PresentableCodeCast> PresentCodeCasts(User loggedInUser)
        {
            var presentableCodecasts = Context.CodecastGateway.FindAllCodecastsSortedChronologically()
                .Select(pcc => FormatCodecasts(loggedInUser, pcc));
            return presentableCodecasts.ToList();
        }

        private PresentableCodeCast FormatCodecasts(User loggedInUser, Codecast pcc)
        {
            return new PresentableCodeCast
            {
                IsDownloadable = IsLicensedFor(DOWNLOADING, loggedInUser, pcc),
                IsViewable = IsLicensedFor(VIEWING, loggedInUser, pcc),
                Title = pcc.Title,
                PublicationDate = pcc.PublicationDate.ToString("M/d/yyyy", CultureInfo.InvariantCulture)
            };
        }

        public bool IsLicensedFor(License.LicenseType licenseType, User user, Codecast codeCast)
        {
            var licenses = Context.LicenseGateway.FindLicensesForUserAndCodecasts(user, codeCast);
            return licenses.Any(l => l.Type == licenseType);
        }
    }
}
