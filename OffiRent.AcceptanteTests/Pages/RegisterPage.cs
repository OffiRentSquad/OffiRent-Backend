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
    class RegisterPage
    {
        public IWebDriver WebDriver { get; }
        public RegisterPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        IWebElement inputName => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[1]/div/div[1]/div[1]/input"));
        IWebElement inputSurname => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[2]/div/div[1]/div[1]/input"));
        IWebElement inputPhoneNumber => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[3]/div/div[1]/div[1]/input"));
        IWebElement inputBirthday => WebDriver.FindElement(By.XPath("/html/body/div/div[1]/main/div/div/div/form/div[4]/div[1]/div/div[1]/div/input"));

        IWebElement inputEmail => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[5]/div/div[1]/div[1]/input"));
        IWebElement inputPassword => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[6]/div/div[1]/div[1]/input"));
        IWebElement inputConfirmPassword => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/form/div[7]/div/div[1]/div[1]/input"));

        public void fillRegisterFields(Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            inputName.SendKeys((string)data.firstName);
            inputSurname.SendKeys((string)data.surname);
            inputPhoneNumber.SendKeys((string)data.phoneNumber);
            selectBirthday();
            inputEmail.SendKeys((string)data.email);
            inputPassword.SendKeys((string)data.password);
            inputConfirmPassword.SendKeys((string)data.confirmPassword);
        }

        void selectBirthday()
        {

        }

        IWebElement btnRegister => WebDriver.FindElement(By.XPath("/html/body/div/div[1]/main/div/div/div/form/div[8]/button"));
        public void ClickBtnRegister() => btnRegister.Click();

        public bool doesInputConfirmPasswordExist()
        {
            try
            {
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
                wait.Until(e => inputConfirmPassword);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
