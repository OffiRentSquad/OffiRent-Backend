using OffiRent.AcceptanteTests.Drivers;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace OffiRent.AcceptanteTests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private Driver _driver;

        public Hooks(Driver driver)
        {
            _driver = driver;
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver.WebDriver = new ChromeDriver();
            _driver.WebDriver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //_driver.WebDriver.Quit();
        }

    }
}