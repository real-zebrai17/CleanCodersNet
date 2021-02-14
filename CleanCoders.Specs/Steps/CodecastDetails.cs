using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace CleanCoders.Specs.Steps
{
    [Binding]
    public class CodecastDetails
    {
        private CodeCastDetailsUseCase _useCase = new CodeCastDetailsUseCase();
        private PresentableCodeCastDetails _details;

        [When(@"the user request details for (.*)")]
        public void WhenTheUserRequestDetailsFor(string permalink)
        {
            _details = _useCase.RequestCodecastDetails(Context.GateKeeper.LoggerInUser, permalink);
        }

        [Then(@"the presented title is (.*), published (.*)")]
        public void ThenThePresentedTitleIsPublished(string title, string publishedDate)
        {
            _details.Title.Should().Be(title);
            _details.PublicationDate.Should().Be(publishedDate);
        }

        [Then(@"with option to purchase (.*) license")]
        public void ThenWithOptionToPurchaseLicense(string licenseType)
        {
            ((string.Equals(licenseType, "viewing", StringComparison.InvariantCultureIgnoreCase) && !_details.IsViewable) ||
             (string.Equals(licenseType, "downloading", StringComparison.InvariantCultureIgnoreCase) && !_details.IsDownloadable))
             .Should().BeTrue();
        }
    }
}
