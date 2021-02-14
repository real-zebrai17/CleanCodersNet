using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static CleanCoders.License.LicenseType;

namespace CleanCoders
{
    public class CodecastSummaryUseCase
    {
        public List<PresentableCodeCastSummary> PresentCodeCasts(User loggedInUser)
        {
            var presentableCodecasts = Context.CodecastGateway.FindAllCodecastsSortedChronologically()
                .Select(pcc => FormatCodecasts(loggedInUser, pcc));
            return presentableCodecasts.ToList();
        }

        private PresentableCodeCastSummary FormatCodecasts(User loggedInUser, Codecast pcc)
        {
            return DoFormatSummaryFields(loggedInUser, pcc, new PresentableCodeCastSummary());
        }

        public static PresentableCodeCastSummary DoFormatSummaryFields(User loggedInUser, Codecast codeCast, PresentableCodeCastSummary pcc)
        {
            pcc.IsDownloadable = IsLicensedFor(DOWNLOADING, loggedInUser, codeCast);
            pcc.IsViewable = IsLicensedFor(VIEWING, loggedInUser, codeCast);
            pcc.Title = codeCast.Title;
            pcc.PublicationDate = codeCast.PublicationDate.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            return pcc;
        }

        public static bool IsLicensedFor(License.LicenseType licenseType, User user, Codecast codeCast)
        {
            var licenses = Context.LicenseGateway.FindLicensesForUserAndCodecasts(user, codeCast);
            return licenses.Any(l => l.Type == licenseType);
        }
    }
}
