using CleanCoders.UseCases.CodeCastSummaries;

namespace CleanCoders.UseCases.CodeCastDetails
{
    public class PresentableCodeCastDetails
        : PresentableCodeCastSummary
    {
        public bool WasFound { get; set; }
    }
}
