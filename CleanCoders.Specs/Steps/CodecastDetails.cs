using FluentAssertions;
using TechTalk.SpecFlow;

namespace CleanCoders.Specs.Steps
{
    [Binding]
    public class CodecastDetails
    {
        private CodecastDetailsUseCase _useCase = new CodecastDetailsUseCase();
        private PresentableCodeCastDetails _details;

        [When(@"the user request details for (.*)")]
        public void WhenTheUserRequestDetailsFor(string permalink)
        {
            _details = _useCase.RequestCodecastDetails(Context.GateKeeper.LoggerInUser, permalink);
        }

        [Then(@"the presented title is (.*), published (.*)")]
        public void ThenThePresentedTitleIsPublished(string title, string publishedDate)
        {
            _details.title.Should().Be(title);
            _details.publicationDate.Should().Be(publishedDate);
        }

        [Then(@"with option to purchase (.*) license")]
        public void ThenWithOptionToPurchaseLicense(string licenseType)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
