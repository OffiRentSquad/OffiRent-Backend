using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OffiRent.AcceptanteTests.Features
{
    [Binding]
    public sealed class CreatePostStepDefinition
    {
        [Given(@"the user specifies an office created by a user other than himself")]
        public void GivenTheUserSpecifiesAnOfficeCreatedByAUserOtherThanHimself()
        {
            ScenarioContext.StepIsPending();
        }

        [When(@"the intent to create post was submitted,")]
        public void WhenTheIntentToCreatePostWasSubmitted()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the system will return a message saying that you cannot create publications about offices that are not yours\.")]
        public void ThenTheSystemWillReturnAMessageSayingThatYouCannotCreatePublicationsAboutOfficesThatAreNotYours()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"the user is creating a publication")]
        public void GivenTheUserIsCreatingAPublication()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"a publication duration greater than (.*) month is specified")]
        public void GivenAPublicationDurationGreaterThanMonthIsSpecified(int p0)
        {
            ScenarioContext.StepIsPending();
        }

        [When(@"the intention to create the post is confirmed")]
        public void WhenTheIntentionToCreateThePostIsConfirmed()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the system will return a message saying that non-premium users cannot create publications with a duration greater than one month\.")]
        public void ThenTheSystemWillReturnAMessageSayingThatNonPremiumUsersCannotCreatePublicationsWithADurationGreaterThanOneMonth()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"the user is premium")]
        public void GivenTheUserIsPremium()
        {
            ScenarioContext.StepIsPending();
        }
    }
}