using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TestSandbox1.Tests
{
    [TestFixture]
    public class TestSite
    {
        internal IWebDriver D;

        [SetUp]
        public void SetupTest()
        {
            D = new ChromeDriver();
        }

        [TearDown]
        public void TeardownTest()
        {
            D.Quit();
        }

        [Test]
        public void Check()
        {
            Console.WriteLine("Test started!");
            const string mailUrl = "https://10minutemail.net";
            const string sportsUrl = "https://sports.ru/";
            using (D)
            {
                D.Navigate().GoToUrl(mailUrl);
                var email = D.FindElement(By.Id("fe_text")).Text;
                Console.WriteLine("Email = '{0}'", email);
                D.Navigate().GoToUrl(sportsUrl);
                //D.W
                Thread.Sleep(10000);
            }
        }
    }
}
