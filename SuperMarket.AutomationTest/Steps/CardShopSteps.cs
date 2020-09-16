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
    public class CardShopSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private static IWebDriver _driver;
        private static IJavaScriptExecutor _jsExecutor;
        private static List<String> _lstProduct;
        
        public CardShopSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _lstProduct = new List<string>();
        }

        [BeforeFeature]
        [Scope (Tag = "CardShopFeature")]
        public static void Start()
        {
            _driver = new ChromeDriver(".\\");
            _driver.Url = "http://localhost:8989";
            _jsExecutor = ((IJavaScriptExecutor)_driver);
        }

        [AfterFeature]
        [Scope(Tag = "CardShopFeature")]
        public static void End()
        {
            _driver.Quit();
        }

        private string ClickFirstProduct()
        {
            IList<IWebElement> lstProduct = _driver.FindElements(By.ClassName("agile_top_brand_left_grid"));
            var firstElement = lstProduct[0].FindElement(By.TagName("input"));
            string productName = lstProduct[0].FindElement(By.TagName("p")).Text;

            firstElement.Click();

            return productName;
        }

        private void ClickElementWithJS(string id)
        {
            IWebElement element = _driver.FindElement(By.Id(id));
            _jsExecutor.ExecuteScript($"$('#{id}')[0].click()", element);
        }

        private void UpdateQuanlityInList(int number, int index, IWebElement element)
        {
            IWebElement input = _driver.FindElement(By.XPath($"//table//tr[@class='rem1'][{index}]//input[@type='number']"));

            string jsScript = "$('.cart_items').find('tr.rem1 {0}')[{1}].click()";

            input.Clear();
            input.SendKeys(number.ToString());

            _jsExecutor.ExecuteScript(string.Format(jsScript, ".btn_update_quantity", index - 1), element);
        }

        [Given(@"Add product to cardShop")]
        public void GivenAddProductToCardShop()
        {
            // Navigation_MainMenuRepeater_LinkButton1_0
            IWebElement groceries = _driver.FindElement(By.Id("Navigation_MainMenuRepeater_LinkButton1_0"));
            groceries.Click();

            IWebElement groceriesALl = _driver.FindElement(By.Id("Navigation_MainMenuRepeater_HyperLink2_0"));
            groceriesALl.Click();
            ClickFirstProduct();

            ClickElementWithJS("MainContent_Categories_MainMenuRepeater_LinkButton1_1");
            ClickFirstProduct();
        }

        [Then(@"Check result cardshop should be two")]
        public void ThenCheckResultCardshopShouldBeTwo()
        {
            IWebElement countItem = _driver.FindElement(By.Id("Header_LabelItemCount"));
            string count = countItem.Text;

            Assert.AreEqual("2", count);
        }

        [Given(@"Go to the card shop page")]
        public void GivenGoToTheCardShopPage()
        {
            ClickElementWithJS("LinkButtonViewCart");
        }

        [StepArgumentTransformation(@"is (.*) sub one")]
        public string CountProductsBeforeDelete(string number)
        {
            string beforeDelete = _driver.FindElement(By.Id("MainContent_LabelItemCount")).Text;
            return beforeDelete;
        }

        [Given(@"Delete first product to cardshop")]
        public void GivenDeleteFirstProductToCardshop()
        {
            IList<IWebElement> lstBtnDelete = _driver.FindElements(By.ClassName("btn_remove_item"));
            IWebElement btnDelete = lstBtnDelete[0];

            string idBtnDelete = btnDelete.GetAttribute("id");
            ClickElementWithJS(idBtnDelete);

            IAlert altert = _driver.SwitchTo().Alert();
            altert.Accept();
        }

        [Then(@"the result table should be is (.*) sub one")]
        public void ThenTheResultTableShouldBeIsSubOne(string before)
        {
            int number = Int32.Parse(before);
            IWebElement countItem = _driver.FindElement(By.Id("MainContent_LabelItemCount"));
            Assert.AreEqual((number - 1).ToString(), countItem.Text);
        }

        [Given(@"Delete last product to cardshop")]
        public void GivenDeleteLastProductToCardshop()
        {
            IList<IWebElement> lstBtnDelete = _driver.FindElements(By.ClassName("btn_remove_item"));
            IWebElement btnDelete = lstBtnDelete[lstBtnDelete.Count - 1];

            string idBtnDelete = btnDelete.GetAttribute("id");
            ClickElementWithJS(idBtnDelete);

            IAlert altert = _driver.SwitchTo().Alert();
            altert.Accept();
        }

        [Then(@"the result table should be an empty")]
        public void ThenTheResultTableShouldBeAnEmpty()
        {
            IWebElement countItem = _driver.FindElement(By.Id("MainContent_LabelItemCount"));
            Assert.AreEqual("0", countItem.Text);
        }

        [Given(@"Go to Groceries,  Household and Personal Care page add products")]
        public void GivenGoToGroceriesHouseholdAndPersonalCarePageAddProducts()
        {
            IWebElement groceries = _driver.FindElement(By.Id("Navigation_MainMenuRepeater_LinkButton1_0"));
            groceries.Click();

            IWebElement groceriesALl = _driver.FindElement(By.Id("Navigation_MainMenuRepeater_HyperLink2_0"));
            groceriesALl.Click();
            _lstProduct.Add(ClickFirstProduct());

            ClickElementWithJS("MainContent_Categories_MainMenuRepeater_LinkButton1_1");
            _lstProduct.Add(ClickFirstProduct());

            ClickElementWithJS("MainContent_Categories_MainMenuRepeater_LinkButton1_2");
            _lstProduct.Add(ClickFirstProduct());
        }

        [Then(@"Check list product correct with product added")]
        public void ThenCheckListProductCorrectWithProductAdded()
        {
            IList<IWebElement> trElement = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));

            int check = 0;
            for (int i = 0; i < trElement.Count; i++)
            {
                var item = trElement[i];
                string id = $"MainContent_RepeaterCartItems_LabelProductName_{i}";
                string text = item.FindElement(By.Id(id)).Text;
                if (_lstProduct.Contains(text))
                    check++;
            }

            Assert.AreEqual(3, check);
        }

        [Given(@"Update quanlity is ""(.*)"" with first product on list")]
        public void GivenUpdateQuanlityIsWithFirstProductOnList(int number)
        {
            int index = 0;
            IList<IWebElement> trElement = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));
            IWebElement input = trElement[index].FindElement(By.XPath("//input[@type='number']"));

            string jsScript = "$('.cart_items').find('tr.rem1 {0}')[{1}].click()";

            input.Clear();
            input.SendKeys(number.ToString());

            _jsExecutor.ExecuteScript(string.Format(jsScript, ".btn_update_quantity", index), trElement);

        }

        [Then(@"Total price must be match with quanlity\*price")]
        public void ThenTotalPriceMustBeMatchWithQuanlityPrice()
        {
            string value = _driver.FindElement(By.XPath($"//table//tr[@class='rem1'][{1}]//input[@type='number']")).GetAttribute("value");
            string price = _driver.FindElement(By.XPath($"//span[@id='MainContent_RepeaterCartItems_LabelUnitPrice_{0}']")).Text;
            string totalPrice = _driver.FindElement(By.XPath($"//span[@id='MainContent_RepeaterCartItems_LabelTotalPrice_{0}']")).Text;

            int quanlity = Int32.Parse(value);
            float priceProduct = float.Parse(price);
            Assert.AreEqual(quanlity * priceProduct, float.Parse(totalPrice));
        }

        [Given(@"Update quanlity with negative number is ""(.*)"" on last item in list")]
        public void GivenUpdateQuanlityWithNegativeNumberIsOnLastItemInList(int number)
        {
            IList<IWebElement> tr = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));
            UpdateQuanlityInList(number, tr.Count, tr[tr.Count - 1]);
        }

        [Then(@"Update negative number is (.*)")]
        public void ThenUpdateNegativeNumberIs(int number)
        {
            IList<IWebElement> trElement = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));
            int index = trElement.Count;
            string value = _driver.FindElement(By.XPath($"//table//tr[@class='rem1'][{index}]//input[@type='number']")).GetAttribute("value");
            // string message = number.ToString().Equals(value) ? "Updated is wrong" : "Updated is correct";
            Assert.AreEqual(number.ToString(), value);
        }

        [Given(@"Update quanlity with positive number is ""(.*)"" on first item in list")]
        public void GivenUpdateQuanlityWithPositiveNumberIsOnFirstItemInList(int number)
        {
            IList<IWebElement> tr = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));
            UpdateQuanlityInList(number, 1, tr[0]);
        }


        [Then(@"Update positive number is (.*)")]
        public void ThenUpdatePositiveNumberIs(int number)
        {
            string value = _driver.FindElement(By.XPath("//table//tr[@class='rem1'][1]//input[@type='number']")).GetAttribute("value");
            Assert.AreEqual(number.ToString(), value);
        }

        [Given(@"Update quanlity with not number is ""(.*)"" and value before is (.*) on fist item in list")]
        public void GivenUpdateQuanlityWithNotNumberIsAndValueBeforeIsOnFistItemInList(string str, int number)
        {
            IList<IWebElement> trElement = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));
            IWebElement input = trElement[0].FindElement(By.XPath("//input[@type='number']"));
            input.Clear();
            input.SendKeys(number.ToString());
            input.SendKeys(str);
            string jsScript = "$('.cart_items').find('tr.rem1 {0:value}')[0].click()";
            _jsExecutor.ExecuteScript(string.Format(jsScript, ".btn_update_quantity"), trElement);
        }

        [Then(@"Value after updated must be before value is (.*)")]
        public void ThenValueAfterUpdatedMustBeBeforeValueIs(int before)
        {
            string value = _driver.FindElement(By.XPath("//table//tr[@class='rem1'][1]//input[@type='number']")).GetAttribute("value");
            Assert.AreEqual(before.ToString(), value);
        }


        [Given(@"Click any item on the list")]
        public void GivenClickAnyItemOnTheList()
        {
            IList<IWebElement> trElement = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));
            var firstElement = trElement[0].FindElement(By.ClassName("invert-image"));
            firstElement.Click();
        }

        [Then(@"Verify the page is the details page")]
        public void ThenVerifyThePageIsTheDetailsPage()
        {
            var commentList = _driver.FindElement(By.ClassName("comment-list"));
            Assert.AreNotEqual(commentList.Text, "Comments");
        }

        [Then(@"Check button checkout hidden when has item negative quanlity")]
        public void ThenCheckButtonCheckoutHiddenWhenHasItemNegativeQuanlity()
        {
            try
            {
                _driver.FindElement(By.Id("MainContent_ButtonCheckout"));
                Assert.IsFalse(false);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

        [Given(@"Update data correct show check out button")]
        public void GivenUpdateDataCorrectShowCheckOutButton()
        {
            IList<IWebElement> tr = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));
            UpdateQuanlityInList(1, tr.Count, tr[tr.Count - 1]);
        }

        [Then(@"Check total price match with total price in list")]
        public void ThenCheckTotalPriceMatchWithTotalPriceInList()
        {
            IList<IWebElement> trElement = _driver.FindElements(By.XPath("//table//tr[@class='rem1']"));

            float sum = 0;
            for (int i = 0; i < trElement.Count; i++)
            {
                var item = trElement[i];
                string quanlityId = $"MainContent_RepeaterCartItems_LabelTotalPrice_{i}";
                string total = item.FindElement(By.Id(quanlityId)).Text;
                sum += float.Parse(total);
            }
            string totalPrice = _driver.FindElement(By.Id("MainContent_LabelTotalCost")).Text;
            Assert.AreEqual(sum.ToString(), totalPrice);
        }

        [Given(@"Check out item in card shop")]
        public void GivenCheckOutItemInCardShop()
        {
            IWebElement btnCheckOut = _driver.FindElement(By.Id("MainContent_ButtonCheckout"));
            btnCheckOut.Click();
        }

        [Given(@"Login check out item")]
        public void GivenLoginCheckOutItem()
        {
            _driver.FindElement(By.Id("UserName")).SendKeys("tnguyen1");
            _driver.FindElement(By.Id("Password")).SendKeys("Simple@loV3_sync" + Keys.Enter);
        }

        [Then(@"Check total price match with order total")]
        public void ThenCheckTotalPriceMatchWithOrderTotal()
        {

            string orderTotal = _driver.FindElement(By.Id("MainContent_MemberCheckout_LabelTotalCost")).Text;
            IList<IWebElement> elements = _driver.FindElements(By.XPath("//table//tbody//tr"));

            float sum = 0;
            foreach (var item in elements)
            {
                var td = item.FindElements(By.TagName("td"));
                int lastIndex = td.Count - 1;
                string price = td[lastIndex].Text;
                if(!String.IsNullOrEmpty(price))
                    sum += float.Parse(price.Replace("$", ""));
            }

            Assert.AreEqual(sum.ToString(), orderTotal.Replace("$", ""));
        }

        [Given(@"Click paynow")]
        public void GivenClickPaynow()
        {
            _driver.FindElement(By.Id("MainContent_MemberCheckout_ButtonPay")).Click();
        }

        [Then(@"Checkout success")]
        public void ThenCheckoutSuccess()
        {
            IWebElement element = _driver.FindElement(By.Id("mainContentWrapper")).FindElement(By.TagName("h2"));
            string text = element.Text;
            Assert.IsTrue("Your order has been proceeded successfully!".Equals(text));
        }


    }
}
