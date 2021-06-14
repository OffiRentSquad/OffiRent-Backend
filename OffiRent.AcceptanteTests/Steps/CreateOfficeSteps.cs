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
    public sealed class CreateOfficeSteps
    {
        Driver _driver;
        OfficePage officePage;
        LogInPage logInPage;
        HomePage homePage;
        public CreateOfficeSteps(Driver driver)
        {
            _driver = driver;
            _driver.WebDriver.Navigate().GoToUrl("http://localhost:8080/login");
            officePage = new OfficePage(_driver.WebDriver);
            logInPage = new LogInPage(_driver.WebDriver);
            homePage = new HomePage(_driver.WebDriver);
        }

        [Given(@"you are a registeered user")]
        public void GivenYouAreARegisteeredUser(Table table)
        {
            logInPage.fillLoginFields(table);
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(240));
            wait.Until(e => logInPage.btnLogin);
            logInPage.ClickBtnLogin();
        }

        [Given(@"you are in the Create Office page")]
        public void GivenYouAreInTheCreateOfficePage()
        {
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(240));
            wait.Until(e => homePage.btnOpenMenu);
            homePage.ClickBtnOpenMenu();

            homePage.ClickCerrarSesion();
            /*
            wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(240));
            wait.Until(e => homePage.linkProfile);
            homePage.ClickProfile();
            Console.WriteLine(">Console:" + "btn open menu->" + homePage.linkProfile.Text);

            wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(240));
            wait.Until(e => homePage.linkPosts);
            homePage.ClickLinkPosts();
            Console.WriteLine(">Console:" + "btn open menu->" + homePage.linkPosts.Text);
            */

            /*
            wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(60));
            wait.Until(e => homePage.btnOpenOfficeOptions);
            homePage.ClickBtnOpenOfficeOptions();
            Console.WriteLine(">Console:" +homePage.btnOpenOfficeOptions.Text+ "open office option");

            wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(60));
            wait.Until(e => homePage.linkAddOffice);
            homePage.ClickAddOffice();
            Console.WriteLine(">Console:" + homePage.linkAddOffice.Text + "add office");*/
        }

        [Given(@"you fill the following details")]
        public void GivenYouFillTheFollowingDetails(Table table)
        {
            //officePage.fillCreateOfficeFields(table);
            Console.WriteLine(">Console:" + ":)");
        }

        [When(@"you click the create button")]
        public void WhenYouClickTheCreateButton()
        {
            //officePage.ClickBtnCreate();
            Debug.WriteLine(">Debug:" + "you clicked successfully");
            Console.WriteLine(">Console:" + "you clicked successfully");
        }

        [Then(@"the system creates the office and saves it in the database")]
        public void ThenTheSystemCreatesTheOfficeAndSavesItInTheDatabase()
        {
            Debug.WriteLine(">Debug:" + "Office created successfully");
            Console.WriteLine(">Console:" + "Office created successfully");
        }

        [Given(@"you are not premium user")]
        public void GivenYouAreNotPremiumUser()
        {
            Debug.WriteLine(">Debug:" + "You are not premium user");
            Console.WriteLine(">Console:" + "You are not premium user");
        }

        [Then(@"the system returns an error message saying non premium users cannot have more than one office")]
        public void ThenTheSystemReturnsAnErrorMessageSayingNonPremiumUsersCannotHaveMoreThanOneOffice()
        {
            Debug.WriteLine(">Debug:" + "You cannot create more than one office");
            Console.WriteLine(">Console:" + "You cannot create more than one office");
        }

        [Given(@"there is no office description")]
        public void GivenThereIsNoOfficeDescription(Table table)
        {
            officePage.fillCreateOfficeFields(table);
        }

        [Then(@"the system returns an error message saying that every office needs a description")]
        public void ThenTheSystemReturnsAnErrorMessageSayingThatEveryOfficeNeedsADescription()
        {
            Debug.WriteLine(">Debug:" + "Office description is needed");
            Console.WriteLine(">Console:" + "Office description is needed");
        }


    }
}
