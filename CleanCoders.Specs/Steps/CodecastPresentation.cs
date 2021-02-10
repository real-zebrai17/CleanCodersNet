using CleanCoders.Specs.TestDoubles;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
            var user = Context.Gateway.FindUser(userName);
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
            List<Codecast> codecast = Context.Gateway.FindAllCodecasts();
            new List<Codecast>(codecast).ForEach(cc => Context.Gateway.Delete(cc));

            if (Context.Gateway.FindAllCodecasts().Count() != 0)
                throw new CodePresentationRunnerException(nameof(GivenNoCodecasts), "Records not cleared from gateway");
        }

        [Given(@"Codecasts")]
        public void GivenCodecasts(Table table)
        {
            var codecasts = table.CreateSet<CodeCastData>();
            foreach (var codecast in codecasts)
                Context.Gateway.Save(
                    new Codecast { Title = codecast.title, PublicationDate = codecast.published }
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
            _useCase.getPresentedCodecasts(_gateKeeper.LoggerInUser)
                .Any()
                .Should().BeFalse();
        }


    }
}
