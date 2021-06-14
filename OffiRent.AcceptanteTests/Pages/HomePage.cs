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
    class HomePage
    {
        public IWebDriver WebDriver { get; }
        public HomePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebElement btnOpenMenu => WebDriver.FindElement(By.XPath("//*[@id=\"app\"]/div/header/div/button"));
        public void ClickBtnOpenMenu() => btnOpenMenu.Click();

        public IWebElement linkProfile => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[2]/div[2]/a"));
        public void ClickProfile() => linkProfile.Click();

        public IWebElement btnOpenOfficeOptions => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[4]/div/div[1]/div[2]/div"));
        public void ClickBtnOpenOfficeOptions() => btnOpenOfficeOptions.Click();

        public IWebElement linkAddOffice => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[4]/div/div[2]/div[1]/a"));
        public void ClickAddOffice() => linkAddOffice.Click();
        public IWebElement linkSeeOffices => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[4]/div/div[2]/div[2]/a"));
        public void ClickSeeOffices() => linkSeeOffices.Click();


        public IWebElement linkPosts => WebDriver.FindElement(By.XPath("/html/body/div/div/nav/div[1]/div[2]/div[5]/div[2]/a"));
        public void ClickLinkPosts() => linkPosts.Click();
        public IWebElement linkReservations => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[6]/div[2]/a"));
        public void ClickLinkReservations() => linkReservations.Click();

        public IWebElement btnOpenOfficeOptions2 => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[4]/div/div[1]/div[2]"));
        public void ClickBtnOpenOfficeOptions2() => btnOpenOfficeOptions2.Click();
        public IWebElement btnOpenOfficeOptions3 => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[4]/div/div[1]"));
        public void ClickBtnOpenOfficeOptions3() => btnOpenOfficeOptions3.Click();

        public bool doesInputMinPriceExist()
        {
            try
            {
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60));
                wait.Until(e => inputMinPrice);
                return true;
            }
            catch
            {
                return false;
            }
        }

        IWebElement linkCerrarSesion => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/nav/div[1]/div[2]/div[7]"));
        public void ClickCerrarSesion()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => linkCerrarSesion);
            linkCerrarSesion.Click();
        }

        IWebElement inputMinPrice => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div[1]/div/div/div[1]/div[2]/div[1]/div/div/div[1]/div[1]/input"));
        IWebElement inputMaxPrice => WebDriver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div[1]/div/div/div[1]/div[2]/div[2]/div/div/div[1]/div[1]/input"));
        IWebElement selectDistrict => WebDriver.FindElement(By.XPath("/html/body/div/div[1]/main/div/div/div/div[1]/div/div/div[1]/div[2]/div[3]/div/div[2]/div[1]"));
        void OpenSelectDistrict() => selectDistrict.Click();

        IWebElement barrancoOption => WebDriver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]"));
        void SelectBarrancoOption() => barrancoOption.Click();

        IWebElement oficinasTitle => WebDriver.FindElement(By.XPath("/html/body/div/div[1]/main/div/div/div/div[2]/div[1]"));

        public void fillSearchByPriceRange(Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => inputMinPrice);
            inputMinPrice.SendKeys((string)data.minPrice);
            wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => inputMaxPrice);
            inputMaxPrice.SendKeys((string)data.maxPrice);
        }
        public void fillSearchByDistrict(Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => selectDistrict);
            OpenSelectDistrict();
            wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => barrancoOption);
            SelectBarrancoOption();
        }
        public IWebElement btnSearch => WebDriver.FindElement(By.XPath("/html/body/div/div[1]/main/div/div/div/div[1]/div/div/div[1]/div[2]/div[4]/div/button"));
        public void ClickBtnSearch()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(e => btnSearch);
            btnSearch.Click();
        }

        public bool IsBtnSearchEnabled() => btnSearch.Enabled;
        public bool IsOficinasTitleDisplayed() => oficinasTitle.Displayed;

    }
}
