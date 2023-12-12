using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyExample2;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Security.Policy;
using static System.Net.WebRequestMethods;


namespace CreatePaste.Tests
{
    [TestClass]
    public class PasteTests
    {
        public FirefoxDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }
        [TestInitialize]
        public void SetupTest()
        {
            Driver = new FirefoxDriver();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        }
        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }
        [TestMethod]
        public void CreatePaste_Test()
        {
            string pasteCode = "git config --global user.name  \"New Sheriff in Town\"" + "\n" + 
                "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")" + "\n" +
                "git push origin master --force";
            string pasteExp = "10 Minutes";
            string pasteName = "how to gain dominance among developers";
            string message = "Your guest paste has been posted";
            string sintax = "Bash";
            Driver.Url = "https://pastebin.com/";
            PastePage createNewPaste = new PastePage(Driver);
            createNewPaste.CreateNewPaste(pasteCode, pasteExp, pasteName, sintax);
            createNewPaste.ValidatePaste(message, pasteName);
            createNewPaste.ValidateBash(sintax);
            createNewPaste.ValidateCode(pasteCode);
        }
    }
}