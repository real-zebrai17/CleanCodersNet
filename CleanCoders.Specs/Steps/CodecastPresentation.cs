using CleanCoders.Entities;
using CleanCoders.Specs.TestDoubles;
using CleanCoders.UseCases.CodeCastSummaries;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static CleanCoders.Entities.License.LicenseType;

namespace CleanCoders.Specs.Steps
{
    [Binding]
    public class CodecastPresentation
    {
        CodecastSummariesUseCase _useCase = new CodecastSummariesUseCase();
        

        #region Internal Classes 
        private class CodeCastData
        {
            public string title;
            public string published;
            public string permalink;
        }

        private class PresentableCodeCastData
        {
            public string title;
            public string picture;
            public string description;
            public string viewable;
            public string downloadable;
        }

        public class CodePresentationRunnerException : Exception
        {
            private readonly string _method;


            public CodePresentationRunnerException(string method, string message)
                : base($"{method}: {message}")
            {
                _method = method;
            }
        }

        #endregion 


        public CodecastPresentation()
        {
            FixtureSetup.SetupContext();
        }

        [Given(@"that user (.*) is logged in")]
        public void GivenUserLoggedIn(string userName)
        {
            var user = Context.UserGateway.FindUserByUserName(userName);
            if (user != null)
                Context.GateKeeper.SetCurrentUser(user);
            else
                throw new CodePresentationRunnerException(nameof(GivenUserLoggedIn), $"user {userName} not found");
        }

        [Given(@"user (.*)")]
        public void GivenUser(string userName)
        {
            Context.UserGateway.Save(new User(userName));
        }


        [Given(@"no codecasts")]
        public void GivenNoCodecasts()
        {
            List<Codecast> codecast = Context.CodecastGateway.FindAllCodecastsSortedChronologically();
            new List<Codecast>(codecast).ForEach(cc => Context.CodecastGateway.Delete(cc));

            if (Context.CodecastGateway.FindAllCodecastsSortedChronologically().Count() != 0)
                throw new CodePresentationRunnerException(nameof(GivenNoCodecasts), "Records not cleared from gateway");
        }

        [Given(@"Codecasts")]
        public void GivenCodecasts(Table table)
        {
            var codecasts = table.CreateSet<CodeCastData>();
            foreach (var codecast in codecasts)
                Context.CodecastGateway.Save(
                    new Codecast
                    {
                        Title           = codecast.title,
                        PublicationDate = DateTime.ParseExact(codecast.published, "M/d/yyyy", CultureInfo.InvariantCulture),
                        Permalink       = codecast.permalink
                    });
        }

        [Then(@"then following codecats will be presented for (.*)")]
        public void ThenThenFollowingCodecatsWillBePresentedFor(string userName)
        {
            Context.GateKeeper.LoggerInUser
                              .UserName
                              .Should().Be(userName);
        }


        [Then(@"there will be no codecasts presented")]
        public void ThenThereWillBeNoCodecastsPresented()
        {
            _useCase.PresentCodeCasts(Context.GateKeeper.LoggerInUser)
                .Any()
                .Should().BeFalse();
        }

        [Given(@"with license for (.*) able to view (.*)")]
        public void GivenWithLicenseForAbleToView(string userName, string codeCastTitle)
        {
            var user = Context.UserGateway.FindUserByUserName(userName);
            var codeCast = Context.CodecastGateway.FindCodecastByTitle(codeCastTitle);

            var license = new License(VIEWING, user, codeCast);
            Context.LicenseGateway.Save(license);

            if (!CodecastSummariesUseCase.IsLicensedFor(VIEWING, user, codeCast))
                throw new CodePresentationRunnerException(nameof(GivenWithLicenseForAbleToView), "codeCast license not setup.");
        }

        [Given(@"with license for (.*) able to download (.*)")]
        public void GivenWithLicenseForAbleToDownloadA(string userName, string codeCastTitle)
        {
            var user = Context.UserGateway.FindUserByUserName(userName);
            var codeCast = Context.CodecastGateway.FindCodecastByTitle(codeCastTitle);

            var license = new License(DOWNLOADING, user, codeCast);
            Context.LicenseGateway.Save(license);

            if (!CodecastSummariesUseCase.IsLicensedFor(DOWNLOADING, user, codeCast))
                throw new CodePresentationRunnerException(nameof(GivenWithLicenseForAbleToView), "codeCast license not setup.");
        }


        [Then(@"Ordered query:of Codecasts")]
        public void ThenOrderedQueryOfCodecasts(Table table)
        {
            var expectedPresentedCodecasts = table.CreateSet<PresentableCodeCastData>().ToArray();
            var presentedCodecastes = _useCase.PresentCodeCasts(Context.GateKeeper.LoggerInUser);

            expectedPresentedCodecasts.Count().Should().Be(presentedCodecastes.Count());
            for (int i = 0; i < presentedCodecastes.Count; i++)
            {
                presentedCodecastes[i].IsViewable
                    .Should().Be(expectedPresentedCodecasts[i].viewable == "+");
                presentedCodecastes[i].IsDownloadable
                    .Should().Be(expectedPresentedCodecasts[i].downloadable == "+");
                presentedCodecastes[i].Title
                    .Should().Be(expectedPresentedCodecasts[i].title);
                presentedCodecastes[i].Title
                    .Should().Be(expectedPresentedCodecasts[i].picture);
                presentedCodecastes[i].Title
                    .Should().Be(expectedPresentedCodecasts[i].description);
            }
        }
    }
}
