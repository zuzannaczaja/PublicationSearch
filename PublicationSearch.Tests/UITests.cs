using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace PublicationSearch.Tests
{
    public class UITests : IDisposable
    {
        private readonly IWebDriver _driver;

        public UITests() => _driver = new EdgeDriver();

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Should_NumberOfRecordsBeEqual()
        {
            _driver.Navigate().GoToUrl("https://localhost:7124");
            _driver.FindElement(By.Name("query")).SendKeys("Cyclosporin dermatology Covid-19");
            _driver.FindElement(By.ClassName("btn-primary")).Click();

            var value = _driver.FindElement(By.Name("recordsCount")).Text;
            value = string.Concat(value.Where(Char.IsDigit));

            Assert.AreEqual(value, "105");
        }

        [Test]
        public void Should_DisplayNoPublicationsText()
        {
            _driver.Navigate().GoToUrl("https://localhost:7124/");
            _driver.FindElement(By.Name("query")).SendKeys("hedjsdhgbdcvn");
            _driver.FindElement(By.ClassName("btn-primary")).Click();

            var value = _driver.FindElement(By.ClassName("text-center")).Text;

            Assert.IsTrue(value.Contains("Brak pasujących publikacji"), "Brak pasujących publikacji");
        }
    }
}
