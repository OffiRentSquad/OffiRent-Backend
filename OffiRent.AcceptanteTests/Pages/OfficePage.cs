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
    class OfficePage
    {
        public IWebDriver WebDriver { get; }
        public OfficePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        IWebElement inputTitle => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[2]/div/div/div[1]/div/div[1]/div[1]/div/div/div[1]/div/input"));
        IWebElement inputDescription => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div[1]/div/input"));

        public IWebElement btnSelectDistrict => WebDriver.FindElement(By.XPath("/html/body/div/div[1]/main/div/div/div/div/div[2]/div/div/div[1]/div/div[1]/div[2]/div/div/div[1]/div[1]/div/div/i"));
        public void ClickBtnSelectDistrict() => btnSelectDistrict.Click();
        public IWebElement optionBarranco => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]"));
        public void ClickOptionBarranco() => optionBarranco.Click();

        public void fillCreateOfficeFields(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            inputTitle.SendKeys((string)data.title);
            inputDescription.SendKeys((string)data.description);

            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(e => btnSelectDistrict);
            ClickBtnSelectDistrict();
            wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(e => optionBarranco);
            ClickOptionBarranco();
        }


        IWebElement btnCreate => WebDriver.FindElement(By.XPath(""));
        public void ClickBtnCreate()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(e => btnCreate);
            btnCreate.Click();
        }

        IWebElement btnCancel => WebDriver.FindElement(By.XPath(""));
        public void ClickBtnCancel() => btnCancel.Click();

    }
}
