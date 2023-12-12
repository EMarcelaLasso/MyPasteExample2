using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace MyExample2
{
    public class PastePage
    {
        private readonly FirefoxDriver driver;
        public PastePage(FirefoxDriver browser)
        {
            driver = browser;
        }
  
        private readonly By newPasteButton = By.XPath("//a[@class='header__btn']");
        private readonly By newPasteTitle = By.XPath("//div[@class='content__title -no-border']");
        private readonly By newCode = By.Id("postform-text");
        private readonly By pasteExpiration = By.XPath("//span[@id='select2-postform-expiration-container']");
        private readonly By pasteName = By.Id("postform-name");
        private readonly By createNewPaste = By.XPath("//button[contains(text(),'Create New Paste')]");
        private readonly By validationLabel = By.XPath("//div[@class='notice -success -post-view']");
        private readonly By sintax = By.Id("select2-postform-format-container");
        private readonly By inputSintax = By.XPath("//input[@class='select2-search__field']");
        private readonly By bash = By.XPath("//a[@class='btn -small h_800']");
        private readonly By code = By.XPath("//ol[@class='bash']");

        public void CreateNewPaste(string code, string exp, string name, string sintaxText)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            if (driver.FindElement(newPasteButton).Displayed)
            {
                driver.FindElement(newPasteButton).Click();
                driver.FindElement(newCode).SendKeys(code);
                driver.FindElement(newPasteButton).SendKeys(Keys.PageDown);
                driver.FindElement(sintax).Click();
                driver.FindElement(inputSintax).SendKeys(sintaxText);
                driver.FindElement(inputSintax).SendKeys(Keys.Enter);
                driver.FindElement(pasteExpiration).Click();
                string expiration = "//li[contains(text(),'" + exp + "')]";
                driver.FindElement(By.XPath(expiration)).Click();
                driver.FindElement(newPasteButton).SendKeys(Keys.PageDown);
                driver.FindElement(pasteName).SendKeys(name);
                driver.FindElement(newPasteButton).SendKeys(Keys.PageDown);
                driver.FindElement(createNewPaste).Click();
            }
        }

        public void ValidatePaste(string expectedText, string name)
        {
            if (driver.FindElement(validationLabel).Displayed)
            {
                Assert.IsTrue(driver.FindElement(validationLabel).Text.Contains(expectedText));
                string vname = "//h1[contains(text(),'" + name + "')]";
                Assert.IsTrue(driver.FindElement(By.XPath(vname)).Displayed, "Name is not the same");
            }
        }

        public void ValidateBash(string sintax)
        {
            if (driver.FindElement(bash).Displayed)
            {
                Assert.IsTrue(driver.FindElement(bash).Text.Contains(sintax), "Bash was not selected to create Paste");
            }
        }

        public void ValidateCode(string vCode)
        {
            vCode = vCode.Replace("\u00A0", " ").Replace("\n", " ");
            string test = driver.FindElement(code).Text.Replace(System.Environment.NewLine, " ");
            Assert.IsTrue(test.Equals(vCode), "Code Displayed is not equal to code regitered");
        }
    }
}
