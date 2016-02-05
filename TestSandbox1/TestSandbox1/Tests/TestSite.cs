using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TestSandbox1.Tests.Helpers;

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
            //D.Quit();
        }

        [Test]
        public void TestSportsRu()
        {
            Console.WriteLine("Test started!");
            const string mailUrl = "https://10minutemail.net";
            const string sportsUrl = "https://sports.ru/";
            using (D)
            {
                D.Navigate().GoToUrl(mailUrl);

                var wait = new WebDriverWait(D, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fe_text")));

                D.FindElement(By.Id("fe_text")).Click();

                var email = D.FindElement(By.Id("fe_text")).GetAttribute("value");
                var username = email.Split('@').First();
                var password = "www12342";

                D.RegSportsRu(sportsUrl, email, password, username);
                
                D.Navigate().GoToUrl(mailUrl);

                Thread.Sleep(5000);
            }
        }

        [Test]
        public void Check()
        {
            Console.WriteLine("Test started!");
            const string mailUrl = "https://10minutemail.net";
            const string sportsUrl = "https://sports.ru/";
            const string gitUrl = "https://github.com/join";
            using (D)
            {
                D.Navigate().GoToUrl(mailUrl);

                var wait = new WebDriverWait(D, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fe_text")));

                D.FindElement(By.Id("fe_text")).Click();

                var email = D.FindElement(By.Id("fe_text")).GetAttribute("value");
                var username = email.Split('@').First();
                var password = "www12342";
                Console.WriteLine("Email = '{0}'", email);
                D.Navigate().GoToUrl(sportsUrl);
                Thread.Sleep(10000);
                D.Navigate().GoToUrl(gitUrl);
                D.FindElement(By.Id("user_login")).SendKeys(username);
                D.FindElement(By.Id("user_email")).SendKeys(email);
                D.FindElement(By.Id("user_password")).SendKeys(password);
                
                D.Navigate().GoToUrl(mailUrl);

                Thread.Sleep(5000);
            }
        }
    }
}
