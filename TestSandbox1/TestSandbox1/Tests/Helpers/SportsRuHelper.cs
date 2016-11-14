using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSandbox1.Tests.Helpers
{
    public static class SportsRuHelper
    {
        public static void RegSportsRu(this IWebDriver d, string sportsUrl, string email, string password, string username)
        {
            d.Navigate().GoToUrl(sportsUrl);

            wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Зарегистрироваться")));
            d.FindElement(By.PartialLinkText("Зарегистрироваться")).Click();

            //wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));

            Console.WriteLine("1 " + d.FindElements(By.ClassName("popup")).Count);
            Console.WriteLine("2 " + d.FindElements(By.ClassName("popup")).First().FindElements(By.Name("login")).Count);
            Console.WriteLine("3 " + d.FindElements(By.ClassName("popup")).Count(x => x.GetAttribute("value").Contains("Ваш e-mail")));
            Console.WriteLine("4 " + d.FindElements(By.ClassName("popup")).Count(x => x.GetAttribute("placeholder").Contains("Ваш e-mail")));

            d.FindElement(By.Name("login")).Click();
            d.FindElement(By.Name("login")).SendKeys(email);
            d.FindElement(By.Name("nick")).Click();
            d.FindElement(By.Name("nick")).SendKeys(username);
            d.FindElement(By.Name("password")).Click();
            d.FindElement(By.Name("rassword")).SendKeys(password);
            d.FindElement(By.Name("repasswd")).Click();
            d.FindElement(By.Name("repasswd")).SendKeys(password);

            d.FindElements(By.TagName("button")).First(x => x.GetAttribute("value").Contains("Зарегистрироваться")).Click();
        }
    }
}
