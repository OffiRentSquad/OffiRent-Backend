using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace OffiRent.AcceptanteTests.Pages
{
    class LogInPage
    {
        public IWebDriver WebDriver { get; }
        public LogInPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        IWebElement inputEmail => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[1]/div/div[1]/div[1]/input"));
        IWebElement inputPassword => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[2]/div/div[1]/div[1]/input"));

        public void fillLoginFields(Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            inputEmail.SendKeys((string)data.email);
            inputPassword.SendKeys((string)data.password);
        }

        public IWebElement btnLogin => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[3]/button"));

        public void ClickBtnLogin() => btnLogin.Click();

        public IWebElement btnRegister => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[4]/a"));

        public void ClickBtnRegister() => btnRegister.Click();

    }
}
