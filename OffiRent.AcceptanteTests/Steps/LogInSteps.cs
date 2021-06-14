using NUnit.Framework;
using OffiRent.AcceptanteTests.Drivers;
using OffiRent.AcceptanteTests.Pages;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OffiRent.AcceptanteTests.Steps
{
    [Binding]
    public sealed class LogInSteps
    {
        Driver _driver;
        LogInPage logInPage;
        HomePage homePage;
        public LogInSteps(Driver driver)
        {
            _driver = driver;
            _driver.WebDriver.Navigate().GoToUrl("http://localhost:8080/login");
            logInPage = new LogInPage(_driver.WebDriver);
            homePage = new HomePage(_driver.WebDriver);
        }

        [Given(@"you introduce your credentials")]
        public void GivenYouIntroduceYourCredentials(Table table)
        {
            logInPage.fillLoginFields(table);
        }

        [When(@"you click login button")]
        public void WhenYouClickLoginButton()
        {
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(e => logInPage.btnLogin);
            logInPage.ClickBtnLogin();
        }

        [Then(@"the system redirects you to home page")]
        public void ThenTheSystemRedirectsYouToHomePage()
        {
            bool result = homePage.doesInputMinPriceExist();
            Assert.IsTrue(result);

            Debug.WriteLine(">Debug:" + result);
            Console.WriteLine(">Console:" + result);
        }

        [Then(@"the system shows a message indicating credentials are not correct")]
        public void ThenTheSystemShowsAMessageIndicatingCredentialsAreNotCorrect()
        {
            bool result = homePage.doesInputMinPriceExist();
            Assert.IsFalse(result);

            Debug.WriteLine(">Debug:" + result);
            Console.WriteLine(">Console:" + result);
        }


    }

}
