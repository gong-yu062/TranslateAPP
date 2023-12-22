using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SeleniumTest
{
    [TestClass]
    public class SeleniumTest1
    {
        //private readonly string webAppUri = "http://localhost:5026/";
        private IWebDriver? driver;

        [TestInitialize]
        public void Initialize()
        {
            // Setup - Initialize WebDriver
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestMethod]
        public void TestTranslationFeature()
        {
            // Navigate to the Translate page
            driver.Navigate().GoToUrl("http://localhost:5026/translate"); // Update with your app's URL

            // Find elements
            var textArea = driver.FindElement(By.Id("textToTranslate"));
            var targetLanguageSelect = driver.FindElement(By.Id("targetLanguage"));
            var submitButton = driver.FindElement(By.TagName("button"));

            // Perform actions
            textArea.SendKeys("Hello");
            targetLanguageSelect.SendKeys("Spanish"); // Adjust as needed
            submitButton.Click();

            // Wait and assert for the translation result
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var resultElement = wait.Until(drv => drv.FindElement(By.ClassName("alert")));

            // Assertions
            Assert.IsTrue(resultElement.Displayed, "Translation result is not displayed.");
            Assert.IsTrue(resultElement.Text.Contains("Translation Result:"), "The translation result text is incorrect.");

            // Additional assertions and interactions can be added here
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Teardown - Close the WebDriver
            driver.Quit();
        }


    }
}