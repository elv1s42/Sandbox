using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
        public void Check()
        {
            Console.WriteLine("Test started!");
            const string mailUrl = "https://10minutemail.net";
            const string sportsUrl = "https://sports.ru/";
            const string gitUrl = "https://github.com/join";
            //using (D)
            //{
                D.Navigate().GoToUrl(mailUrl);
            
                var wait = new WebDriverWait(D, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fe_text")));

                D.FindElement(By.Id("fe_text")).Click();

                var email = D.FindElement(By.Id("fe_text")).GetAttribute("value");
                var username = email.Split('@').First();
                var password = "www12342";
                Console.WriteLine("Email = '{0}'", email);
                //D.Navigate().GoToUrl(sportsUrl);
                //Thread.Sleep(10000);
                /*D.Navigate().GoToUrl(gitUrl);
                D.FindElement(By.Id("user_login")).SendKeys(username);
                D.FindElement(By.Id("user_email")).SendKeys(email);
                D.FindElement(By.Id("user_password")).SendKeys(password);
                */
                D.Navigate().GoToUrl(sportsUrl);

                wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Зарегистрироваться")));
                D.FindElement(By.PartialLinkText("Зарегистрироваться")).Click();

                //wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));

                Console.WriteLine("1 " + D.FindElements(By.ClassName("popup")).Count);
                Console.WriteLine("2 " + D.FindElements(By.ClassName("popup")).First().FindElements(By.Name("login")).Count);
                Console.WriteLine("3 " + D.FindElements(By.ClassName("popup")).Count(x => x.GetAttribute("value").Contains("Ваш e-mail")));
                Console.WriteLine("4 " + D.FindElements(By.ClassName("popup")).Count(x => x.GetAttribute("placeholder").Contains("Ваш e-mail")));

                D.FindElement(By.Name("login")).Click();
                D.FindElement(By.Name("login")).SendKeys(email);
                D.FindElement(By.Name("nick")).Click();
                D.FindElement(By.Name("nick")).SendKeys(username);
                D.FindElement(By.Name("password")).Click();
                D.FindElement(By.Name("rassword")).SendKeys(password);
                D.FindElement(By.Name("repasswd")).Click();
                D.FindElement(By.Name("repasswd")).SendKeys(password);

                D.FindElements(By.TagName("button")).First(x => x.GetAttribute("value").Contains("Зарегистрироваться")).Click();
                

                //D.Navigate().GoToUrl(mailUrl);

                //Thread.Sleep(5000);
            //}
        }
    }
}
