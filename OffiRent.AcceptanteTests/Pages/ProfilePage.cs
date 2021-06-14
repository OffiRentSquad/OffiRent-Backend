using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace OffiRent.AcceptanteTests.Pages
{
    class ProfilePage
    {
        public IWebDriver WebDriver { get; }
        public ProfilePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebElement textFullName => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[3]"));
        public IWebElement btnEditProfile => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[8]/a"));
        public void ClickBtnEditProfile() => btnEditProfile.Click();
        public bool doesBtnEditProfileExist()
        {
            try
            {
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
                wait.Until(e => btnEditProfile);
                return true;
            }
            catch
            {
                return false;
            }
        }


        IWebElement inputFullName => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[2]/form/div[1]/div/div[1]/div[1]/input"));

        //IWebElement inputPhoneNumber => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[2]/form/div[2]/div/div[1]/div[1]/input"));
        //IWebElement inputEmail => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[2]/form/div[3]/div/div[1]/div[1]/input"));

        public void fillEditProfileFields(Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            inputFullName.SendKeys((string)data.fullName);

            Console.WriteLine((string)data.fullName);
            //inputPhoneNumber.SendKeys((string)data.phoneNumber);
            //inputEmail.SendKeys((string)data.email);
        }

        public IWebElement btnCancel => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[3]/button"));
        public void ClickBtnCancel() => btnCancel.Click();

        public IWebElement btnSaveChanges => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[4]/button"));
        public void ClickBtnSaveChanges() => btnSaveChanges.Click();


    }
}
