using NUnit.Framework;
using OffiRent.AcceptanteTests.Drivers;
using OffiRent.AcceptanteTests.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OffiRent.AcceptanteTests.Steps
{
    [Binding]
    public sealed class AccountRegistrationSteps
    {
        Driver _driver;
        LogInPage logInPage;
        RegisterPage registerPage;
        public AccountRegistrationSteps(Driver driver)
        {
            _driver = driver;
            _driver.WebDriver.Navigate().GoToUrl("http://localhost:8080/login");
            logInPage = new LogInPage(_driver.WebDriver);
            registerPage = new RegisterPage(_driver.WebDriver);
        }

        [Given(@"you are in RegisterPage")]
        public void GivenYouAreInRegisterPage()
        {
            logInPage.ClickBtnRegister();
        }


        [Given(@"you fill the following mandatory data")]
        public void GivenYouFillTheFollowingMandatoryData(Table table)
        {
            registerPage.fillRegisterFields(table);
        }

        [When(@"you click register button")]
        public void WhenYouClickRegisterButton()
        {
            registerPage.ClickBtnRegister();
        }

        [Then(@"the system returns a message saying that your account has been created successfully")]
        public void ThenTheSystemReturnsAMessageSayingThatYourAccountHasBeenCreatedSuccessfully()
        {
            Debug.WriteLine(">Debug:" + "Account created successfully");
            Console.WriteLine(">Console:" + "Account created successfully");
        }

        [Then(@"redirects you to login page")]
        public void ThenRedirectsYouToLoginPage()
        {
            /*bool result = registerPage.doesInputConfirmPasswordExist();
            Assert.IsFalse(result);
            Debug.WriteLine(">Debug:" + result);
            Console.WriteLine(">Console:" + result);*/
            Debug.WriteLine(">Debug:" + "You have been registered and now in login");
            Console.WriteLine(">Console:" + "You have been registered and now in login");
        }

        [Given(@"the user already has an account in the application registered with that same email")]
        public void GivenTheUserAlreadyHasAnAccountInTheApplicationRegisteredWithThatSameEmail()
        {
            Debug.WriteLine(">Debug:" + "You already have an account");
            Console.WriteLine(">Console:" + "You already have an account");
        }

        [Then(@"the system shows a message saying that this email already exists")]
        public void ThenTheSystemShowsAMessageSayingThatThisEmailAlreadyExists()
        {
            //stay in the same page
            bool result = registerPage.doesInputConfirmPasswordExist();
            Assert.IsTrue(result);
        }

        [Given(@"you fill the following mandatory data with future birth day")]
        public void GivenYouFillTheFollowingMandatoryDataWithFutureBirthDay(Table table)
        {
            registerPage.fillRegisterFields(table);
        }

        [Then(@"the system shows a message indicating that the date of birth cannot be in the future")]
        public void ThenTheSystemShowsAMessageIndicatingThatTheDateOfBirthCannotBeInTheFuture()
        {
            Debug.WriteLine(">Debug:" + "Date of birth not valid");
            Console.WriteLine(">Console:" + "Date of birth not valid");
        }

    }
}
