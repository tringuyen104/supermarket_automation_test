using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SuperMarket.AutomationTest.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private static IWebDriver _driver;
        private static IJavaScriptExecutor _jsExecutor;

        [BeforeFeature]
        [Scope (Tag = "FeatureRegister")]
        public static void Start()
        {
            _driver = new ChromeDriver(".\\");
            _driver.Url = "http://localhost:8989";
            _jsExecutor = ((IJavaScriptExecutor)_driver);
        }

        [AfterFeature]
        [Scope(Tag = "FeatureRegister")]
        public static void End()
        {
            _driver.Quit();
        }

        [BeforeScenario]
        [Scope(Tag = "FeatureRegister")]
        public void GoToRegisterPage()
        {
            IWebElement register = _driver.FindElement(By.Id("Header_LoginViewHeader_HyperLink1"));
            register.Click();
        }

        [Given(@"Register with wrong format email value is ""(.*)""")]
        public void GivenRegisterWithWrongFormatEmailValueIs(string str)
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("tnguyen1");
            _driver.FindElement(By.Id("Email")).SendKeys("abc");
            _driver.FindElement(By.Id("UserName")).SendKeys("user1");
            _driver.FindElement(By.Id("Password")).SendKeys("abc1234");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc1234");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");

            var element = _driver.FindElement(By.Id("agreement"));
            _jsExecutor.ExecuteScript("$('#agreement').trigger('click')", element);
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }


        [Given(@"Register with password length less then seven")]
        public void GivenRegisterWithPasswordLengthLessThenSeven()
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("tnguyen1");
            _driver.FindElement(By.Id("Email")).SendKeys("abc@gmail.com");
            _driver.FindElement(By.Id("UserName")).SendKeys("user1");
            _driver.FindElement(By.Id("Password")).SendKeys("abc");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");

            var element = _driver.FindElement(By.Id("agreement"));
            _jsExecutor.ExecuteScript("$('#agreement').trigger('click')", element);
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }
        
        [Given(@"Register with password and confirm password not match")]
        public void GivenRegisterWithPasswordAndConfirmPasswordNotMatch()
        {
            _driver.FindElement(By.Id("Password")).SendKeys("tnguyen1234");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc123");
        }
        
        [Given(@"Register none typing security question")]
        public void GivenRegisterNoneTypingSecurityQuestion()
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("tnguyen1");
            _driver.FindElement(By.Id("Email")).SendKeys("abc@gmail.com");
            _driver.FindElement(By.Id("UserName")).SendKeys("user1");
            _driver.FindElement(By.Id("Password")).SendKeys("abc1234");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc1234");

            var element = _driver.FindElement(By.Id("agreement"));
            _jsExecutor.ExecuteScript("$('#agreement').trigger('click')", element);
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }
        
        [Given(@"Register none typing fullname")]
        public void GivenRegisterNoneTypingFullname()
        {
            _driver.FindElement(By.Id("Email")).SendKeys("abc@gmail.com");
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.FindElement(By.Id("UserName")).SendKeys("user1");
            _driver.FindElement(By.Id("Password")).SendKeys("abc1234");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc1234");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");

            ((IJavaScriptExecutor)_driver).ExecuteScript("$('#agreement').trigger('click')", _driver.FindElement(By.Id("agreement")));
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }
        
        [Given(@"Register none typing Email")]
        public void GivenRegisterNoneTypingEmail()
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("abccom");
            _driver.FindElement(By.Id("UserName")).SendKeys("user1");
            _driver.FindElement(By.Id("Password")).SendKeys("abc1234");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc1234");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");

            ((IJavaScriptExecutor)_driver).ExecuteScript("$('#agreement').trigger('click')", _driver.FindElement(By.Id("agreement")));
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }
        
        [Given(@"Register none typing Password")]
        public void GivenRegisterNoneTypingPassword()
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("abccom");
            _driver.FindElement(By.Id("Email")).SendKeys("abc@test.com");
            _driver.FindElement(By.Id("UserName")).SendKeys("user1");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc1234");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");

            ((IJavaScriptExecutor)_driver).ExecuteScript("$('#agreement').trigger('click')", _driver.FindElement(By.Id("agreement")));
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }
        
        [Given(@"Register none check term and conditions")]
        public void GivenRegisterNoneCheckTermAndConditions()
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("abccom");
            _driver.FindElement(By.Id("Email")).SendKeys("abc@test.com");
            _driver.FindElement(By.Id("UserName")).SendKeys("user1");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("abc1234");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }
        
        [Given(@"Register with user exist")]
        public void GivenRegisterWithUserExist()
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("tnguyen1");
            _driver.FindElement(By.Id("Email")).SendKeys("abc@gmail.com");
            _driver.FindElement(By.Id("UserName")).SendKeys("tnguyen1");
            _driver.FindElement(By.Id("Password")).SendKeys("tnguyen@123");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("tnguyen@123");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");

            var element = _driver.FindElement(By.Id("agreement"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("$('#agreement').trigger('click')", element);
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }

        [Given(@"Register with email exist")]
        public void GivenRegisterWithEmailExist()
        {
            _driver.FindElement(By.Id("FullName")).SendKeys("test1");
            _driver.FindElement(By.Id("Email")).SendKeys("tnguyen1@gmail.com");
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.FindElement(By.Id("UserName")).SendKeys("emailExist");
            _driver.FindElement(By.Id("Password")).SendKeys("tnguyen@123");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("tnguyen@123");
            _driver.FindElement(By.Id("Answer")).SendKeys("red");

            var element = _driver.FindElement(By.Id("agreement"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("$('#agreement').trigger('click')", element);
            _driver.FindElement(By.Id("StepNextButton")).Click();
        }

        [Then(@"Show error message is ""(.*)""")]
        public void ThenShowErrorMessageIs(string message)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            String error = _driver.FindElement(By.ClassName("text-danger")).Text;
            Assert.AreEqual(error, message);
        }
        
        [Then(@"Show error message password not match is ""(.*)""")]
        public void ThenShowErrorMessagePasswordNotMatchIs(string message)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            var element = _driver.FindElement(By.Id("CompareValidatorPassword"));
            string error = element.Text;
            string style = element.GetAttribute("style");
            Assert.AreEqual(error, message);
            Assert.AreEqual(style, "display: inline;");
        }

        //[Then(@"Show error message validate input is ""(.*)""")]
        //public void ThenShowErrorMessageValidateInputIs(string message)
        //{
        //    new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
        //    var element = _driver.FindElement(By.Id("CreateUserWizardMember"));
        //    String validateMessage = (String)_jsExecutor.ExecuteScript("return arguments[0].validationMessage;", element);
        //    Assert.AreEqual(validateMessage, message);
        //}
        //[Then(@"Show error message validate input is ""(.*)"" with Id is ""(.*)""")]
        //public void ThenShowErrorMessageValidateInputIsWithIdIs(string message, string id)
        //{
        //    new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
        //    var element = _driver.FindElement(By.Id(id));
        //    String validateMessage = (String)_jsExecutor.ExecuteScript("return arguments[0].validationMessage;", element);
        //    Assert.AreEqual(validateMessage, message);
        //}

        [Then(@"Show error message validate input is ""(.*)""\twith Id is ""(.*)""")]
        public void ThenShowErrorMessageValidateInputIsWithIdIs(string message, string id)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            var element = _driver.FindElement(By.Id(id));
            String validateMessage = (String)_jsExecutor.ExecuteScript("return arguments[0].validationMessage;", element);
            Assert.AreEqual(validateMessage, message);
        }

        [Then(@"Show error message with alert is ""(.*)""")]
        public void ThenShowErrorMessageWithAlertIs(string errorMessage)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            IAlert alert = _driver.SwitchTo().Alert();
            // Get the text of the alert or prompt
            string message = alert.Text;
            Assert.AreEqual(message, errorMessage);
            alert.Dismiss();
        }

    }
}
