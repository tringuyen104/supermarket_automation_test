using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.AutomationTest.Hooks
{
    public class BaseSetup
    {
        const string _URL = "http://localhost:8989";
        // const string _URL = "https://google.com";
        public  IWebDriver _driver;
        public  IJavaScriptExecutor _jsExecutor;
        [OneTimeSetUp]
        public void Open()
        {
            _driver = new ChromeDriver(".\\");
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Url = _URL;
            _jsExecutor = ((IJavaScriptExecutor)_driver);
        }

        [OneTimeTearDown]
        public void Close()
        {
            _driver.Quit();
        }
    }
}
