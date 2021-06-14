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
    public sealed class SearchPostSteps
    {
        Driver _driver;
        LogInPage logInPage;
        HomePage homePage;
        public SearchPostSteps(Driver driver)
        {
            _driver = driver;
            _driver.WebDriver.Navigate().GoToUrl("http://localhost:8080/login");
            logInPage = new LogInPage(_driver.WebDriver);
            homePage = new HomePage(_driver.WebDriver);
        }
        [Given(@"you are a reegistered user")]
        public void GivenYouAreAReegisteredUser(Table table)
        {
            logInPage.fillLoginFields(table);
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(240));
            wait.Until(e => logInPage.btnLogin);
            logInPage.ClickBtnLogin();
        }

        [Given(@"the user searches an office post by price range")]
        public void GivenTheUserSearchesAnOfficePostByPriceRange(Table table)
        {
            homePage.fillSearchByPriceRange(table);
        }

        [When(@"the user clicks Search button")]
        public void WhenTheUserClicksSearchButton()
        {
            homePage.ClickBtnSearch();
        }

        [Then(@"the system returns a list of offices in the price range wanted")]
        public void ThenTheSystemReturnsAListOfOfficesInThePriceRangeWanted()
        {
            bool result = homePage.IsOficinasTitleDisplayed();

            Debug.WriteLine(">Debug:" + result);
            Console.WriteLine(">Console:" + result);
            Assert.IsTrue(result);
        }

        [When(@"the user searches an office post only by district")]
        public void WhenTheUserSearchesAnOfficePostOnlyByDistrict(Table table)
        {
            homePage.fillSearchByDistrict(table);
        }

        [Then(@"the system wont let you click the button")]
        public void ThenTheSystemWontLetYouClickTheButton()
        {
            bool result = homePage.IsBtnSearchEnabled();

            Debug.WriteLine(">Debug:" + result);
            Console.WriteLine(">Console:" + result);
            Assert.IsFalse(result);
        }


    }
}
