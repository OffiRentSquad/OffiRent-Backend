using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OffiRent.AcceptanteTests.Features
{
    [Binding]
    public sealed class CreateOffice
    {
        

        [Given(@"the user is creating an office")]
        public void GivenTheUserIsCreatingAnOffice()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"the user is not premium")]
        public void GivenTheUserIsNotPremium()
        {
            ScenarioContext.StepIsPending();
        }

        [When(@"the office creation office is confirmed")]
        public void WhenTheOfficeCreationOfficeIsConfirmed()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the system will return an error message saying non premium users cannot have more")]
        public void ThenTheSystemWillReturnAnErrorMessageSayingNonPremiumUsersCannotHaveMore()
        {
            ScenarioContext.StepIsPending();
        }


        [Given(@"the there is no office description")]
        public void GivenTheThereIsNoOfficeDescription()
        {
            ScenarioContext.StepIsPending();
        }

        [When(@"the intention to create the office is confirmed")]
        public void WhenTheIntentionToCreateTheOfficeIsConfirmed()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the system will return an error message saying that every office needs a description")]
        public void ThenTheSystemWillReturnAnErrorMessageSayingThatEveryOfficeNeedsADescription()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"he follows all validations")]
        public void GivenHeFollowsAllValidations()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the system will successfully crea the office and save it on the database")]
        public void ThenTheSystemWillSuccessfullyCreaTheOfficeAndSaveItOnTheDatabase()
        {
            ScenarioContext.StepIsPending();
        }
    }
}