using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCoders
{
    public class CodeCastDetailsUseCase
    {
        public PresentableCodeCastDetails RequestCodecastDetails(User loggerInUser, string permalink)
        {
            var codecast = Context.CodecastGateway.FindCodecastByPermalink(permalink);
            if (codecast == null)
                return new PresentableCodeCastDetails { WasFound = false };

            var details = new PresentableCodeCastDetails();
            CodecastSummaryUseCase.DoFormatSummaryFields(loggerInUser, codecast, details);
            details.WasFound = true;

            return details;
        }
    }
}
