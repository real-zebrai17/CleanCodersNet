using CleanCoders.Specs.TestDoubles;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static CleanCoders.License.LicenseType;

namespace CleanCoders.Specs.Steps
{
    [Binding]
    public class CodecastPresentation
    {
        PresentCodecastUseCase _useCase = new PresentCodecastUseCase();
        GateKeeper _gateKeeper = new GateKeeper();

        #region Internal Classes 
        private class CodeCastData
        {
            public string title;
            public string published;
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
            Context.Gateway = new MockGateway();
        }

        [Given(@"that user (.*) is logged in")]
        public void GivenUserLoggedIn(string userName)
        {
            var user = Context.Gateway.FindUserByUserName(userName);
            if (user != null)
                _gateKeeper.SetCurrentUser(user);
            else
                throw new CodePresentationRunnerException(nameof(GivenUserLoggedIn), $"user {userName} not found");
        }

        [Given(@"user (.*)")]
        public void GivenUser(string userName)
        {
            Context.Gateway.Save(new User(userName));
        }


        [Given(@"no codecasts")]
        public void GivenNoCodecasts()
        {
            List<Codecast> codecast = Context.Gateway.FindAllCodecastsSortedChronologically();
            new List<Codecast>(codecast).ForEach(cc => Context.Gateway.Delete(cc));

            if (Context.Gateway.FindAllCodecastsSortedChronologically().Count() != 0)
                throw new CodePresentationRunnerException(nameof(GivenNoCodecasts), "Records not cleared from gateway");
        }

        [Given(@"Codecasts")]
        public void GivenCodecasts(Table table)
        {
            var codecasts = table.CreateSet<CodeCastData>();
            foreach (var codecast in codecasts)
                Context.Gateway.Save(
                    new Codecast { Title = codecast.title, PublicationDate = DateTime.ParseExact(codecast.published, "M/d/yyyy", CultureInfo.InvariantCulture) }
                    );
        }

        [Then(@"then following codecats will be presented for (.*)")]
        public void ThenThenFollowingCodecatsWillBePresentedFor(string userName)
        {
            _gateKeeper.LoggerInUser
                       .UserName
                       .Should().Be(userName);
        }


        [Then(@"there will be no codecasts presented")]
        public void ThenThereWillBeNoCodecastsPresented()
        {
            _useCase.PresentCodeCasts(_gateKeeper.LoggerInUser)
                .Any()
                .Should().BeFalse();
        }

        [Given(@"with license for (.*) able to view (.*)")]
        public void GivenWithLicenseForAbleToView(string userName, string codeCastTitle)
        {
            var user = Context.Gateway.FindUserByUserName(userName);
            var codeCast = Context.Gateway.FindCodecastByTitle(codeCastTitle);

            var license = new License(VIEWING, user, codeCast);
            Context.Gateway.Save(license);

            if (!_useCase.IsLicensedFor(VIEWING, user, codeCast))
                throw new CodePresentationRunnerException(nameof(GivenWithLicenseForAbleToView), "codeCast license not setup.");
        }

        [Given(@"with license for (.*) able to download (.*)")]
        public void GivenWithLicenseForAbleToDownloadA(string userName, string codeCastTitle)
        {
            var user = Context.Gateway.FindUserByUserName(userName);
            var codeCast = Context.Gateway.FindCodecastByTitle(codeCastTitle);

            var license = new License(DOWNLOADING, user, codeCast);
            Context.Gateway.Save(license);

            if (!_useCase.IsLicensedFor(DOWNLOADING, user, codeCast))
                throw new CodePresentationRunnerException(nameof(GivenWithLicenseForAbleToView), "codeCast license not setup.");
        }


        [Then(@"Ordered query:of Codecasts")]
        public void ThenOrderedQueryOfCodecasts(Table table)
        {
            var expectedPresentedCodecasts = table.CreateSet<PresentableCodeCastData>().ToArray();
            var presentedCodecastes = _useCase.PresentCodeCasts(_gateKeeper.LoggerInUser);

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
