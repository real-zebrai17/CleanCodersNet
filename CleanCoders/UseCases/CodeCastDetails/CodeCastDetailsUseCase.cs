using CleanCoders.Entities;
using CleanCoders.UseCases.CodeCastSummaries;

namespace CleanCoders.UseCases.CodeCastDetails
{
    public class CodeCastDetailsUseCase
    {
        public PresentableCodeCastDetails RequestCodecastDetails(User loggerInUser, string permalink)
        {
            var codecast = Context.CodecastGateway.FindCodecastByPermalink(permalink);
            if (codecast == null)
                return new PresentableCodeCastDetails { WasFound = false };

            var details = new PresentableCodeCastDetails();
            CodecastSummariesUseCase.DoFormatSummaryFields(loggerInUser, codecast, details);
            details.WasFound = true;

            return details;
        }
    }
}
