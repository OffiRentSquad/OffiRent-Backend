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
    public sealed class AccountManagementSteps
    {
        Driver _driver;
        LogInPage logInPage;
        HomePage homePage;
        ProfilePage profilePage;

        public AccountManagementSteps(Driver driver)
        {
            _driver = driver;
            _driver.WebDriver.Navigate().GoToUrl("http://localhost:8080/login");
            logInPage = new LogInPage(_driver.WebDriver);
            homePage = new HomePage(_driver.WebDriver);
            profilePage = new ProfilePage(_driver.WebDriver);
        }

        [Given(@"you are a registered user")]
        public void GivenYouAreARegisteredUser(Table table)
        {
            logInPage.fillLoginFields(table);
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => logInPage.btnLogin);
            logInPage.ClickBtnLogin();
        }

        [Given(@"you are in the Profile page")]
        public void GivenYouAreInTheProfilePage()
        {
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => homePage.btnOpenMenu);
            homePage.ClickBtnOpenMenu();
            wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => homePage.linkProfile);
            homePage.ClickProfile();
        }

        [Given(@"you click Edit profile button")]
        public void GivenYouClickEditProfileButton()
        {
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => profilePage.btnEditProfile);
            profilePage.ClickBtnEditProfile();

            Console.WriteLine(">Console:" + profilePage.btnEditProfile.Text);
        }

        [Given(@"you update fullName")]
        public void GivenYouUpdateFullName(Table table)
        {
            profilePage.fillEditProfileFields(table);
        }

        [When(@"you click save changes button")]
        public void WhenYouClickSaveChangesButton()
        {
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => profilePage.btnSaveChanges);
            profilePage.ClickBtnSaveChanges();
        }

        [Then(@"the system updates and records your data")]
        public void ThenTheSystemUpdatesAndRecordsYourData()
        {
            Console.WriteLine(">Console:" + profilePage.textFullName.Text);

            bool result = profilePage.doesBtnEditProfileExist();
            Assert.IsTrue(result);//CAMBIAR

            Debug.WriteLine(">Debug:" + result);
            Console.WriteLine(">Console:" + result);
        }

        [When(@"you click cancel button")]
        public void WhenYouClickCancelButton()
        {
            var wait = new WebDriverWait(_driver.WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => profilePage.btnCancel);
            profilePage.ClickBtnCancel();
        }

        [Then(@"the system does not change your data")]
        public void ThenTheSystemDoesNotChangeYourData()
        {
            bool result = profilePage.doesBtnEditProfileExist();
            Assert.IsTrue(result);//CAMBIAR
            Debug.WriteLine(">Debug:" + "Your info has not been changed");
            Console.WriteLine(">Console:" + "Your info has not been changed");
        }


    }
}
