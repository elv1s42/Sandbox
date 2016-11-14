using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestSandbox1.Tests.Helpers
{
    public static class SportsRuHelper
    {
        public static string GetRandomEmail()
        {
            return Guid.NewGuid().ToString("d").Substring(0, 8).ToLower() + "@" +
                Guid.NewGuid().ToString("d").Substring(0, 6).ToLower() + ".0" +
                Guid.NewGuid().ToString("d").Substring(0, 2).ToLower();
        }

        public static void RegSportsRu(this IWebDriver d, string sportsUrl, string email, string password, string username)
        {
            d.Navigate().GoToUrl(sportsUrl);

            var wait = new WebDriverWait(d, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Зарегистрироваться")));
            d.FindElement(By.PartialLinkText("Зарегистрироваться")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(.//input[contains(@placeholder,'Ваш e-mail')])[2]")));
            d.FindElement(By.XPath("(.//input[contains(@placeholder,'Ваш e-mail')])[2]")).SendKeys(email);
            d.FindElement(By.XPath("(.//input[contains(@placeholder,'Имя на сайте')])[1]")).SendKeys(username);
            d.FindElement(By.XPath("(.//input[contains(@placeholder,'Пароль')])[1]")).SendKeys(password);
            d.FindElement(By.XPath("(.//input[contains(@placeholder,'Пароль еще раз')])[1]")).SendKeys(password);
            d.FindElement(By.XPath("(.//div/button[contains(@type, 'submit')])[2]")).Click();
            
            Thread.Sleep(2000);

            Console.WriteLine("{0} {1}", email, password);

        }

        public static void LoginSportsRu(this IWebDriver d, string email, string password)
        {
            d.Navigate().GoToUrl("http://www.sports.ru/logon.html");

            var wait = new WebDriverWait(d, TimeSpan.FromSeconds(45));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//div/input[contains(@id, 'login')]")));

            Thread.Sleep(2000);
            d.FindElement(By.XPath(".//div/input[contains(@id, 'login')]")).SendKeys(email);
            Thread.Sleep(2000);
            d.FindElement(By.XPath(".//div/input[contains(@id, 'password')]")).SendKeys(password);
            Thread.Sleep(2000);
            d.FindElement(By.XPath(".//div/input[contains(@id, 'login')]")).Click();
            Thread.Sleep(3000);

            Console.WriteLine("Entered: {0} {1}", email, password);

            d.FindElement(By.XPath(".//div/input[contains(@name, 'submit')]")).Click();

        }

        public static void TryLoginSportsRu(this IWebDriver d, string email, string password)
        {
            var wait = new WebDriverWait(d, TimeSpan.FromSeconds(45));
            try
            {
                d.Navigate().GoToUrl("http://www.sports.ru/logon.html");

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//div/input[contains(@id, 'login')]")));

                Thread.Sleep(2000);
                d.FindElement(By.XPath(".//div/input[contains(@id, 'login')]")).SendKeys(email);
                Thread.Sleep(2000);
                d.FindElement(By.XPath(".//div/input[contains(@id, 'password')]")).SendKeys(password);
                Thread.Sleep(2000);
                d.FindElement(By.XPath(".//div/input[contains(@id, 'login')]")).Click();
                Thread.Sleep(3000);

                Console.WriteLine("Entered: {0} {1}", email, password);

                d.FindElement(By.XPath(".//div/input[contains(@name, 'submit')]")).Click();
            }
            catch (Exception)
            {
                var username = email.Split('@').First();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(
                    string.Format(".//a[text()='{0}']", username))));
                
            }
            

        }
        
        public static void LikeLastPosts(this IWebDriver d, string url)
        {
            d.Navigate().GoToUrl(url);

            var wait = new WebDriverWait(d, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By
                .XPath("(.//div[contains(@class, 'voting')]/i[contains(@class, 'plus')])[1]")));
            d.FindElement(By
                .XPath("(.//div[contains(@class, 'voting')]/i[contains(@class, 'plus')])[1]"))
                .Click();
        }

        public static void LikeComments(this IWebDriver d, string pageUrl)
        {
            d.Navigate().GoToUrl(pageUrl);

            var wait = new WebDriverWait(d, TimeSpan.FromSeconds(30));
            var plusXpath = ".//li[contains(@class, 'comments-list__item')]//div/" +
                            "a[contains(@title, 'Teddy Sheringham')]/../..//div[contains(@class, 'rating')]/" +
                            "div/a[contains(@class, 'plus')]/span";
            
            var js = (IJavaScriptExecutor)d;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

            var footer = d.FindElement(By.XPath(".//footer[@class = 'main-footer']"));
            var actions = new Actions(d);

            Thread.Sleep(2000);
            actions.MoveToElement(footer);
            Thread.Sleep(2000);

            var pluses = d.FindElements(By.XPath(plusXpath));
            Console.WriteLine(pluses.Count + " comments to like");
            foreach (var plus in pluses)
            {
                try
                {
                    Console.Write("Clicking... ");
                    actions.MoveToElement(plus).Build().Perform();
                    Thread.Sleep(2000);
                    actions.MoveToElement(plus).Build().Perform();
                    Thread.Sleep(3000);
                    js.ExecuteScript("window.scrollBy(0, -100)", "");
                    Thread.Sleep(3000);
                    actions.Click(plus).Perform();
                    //actions.MoveToElement(plus).Click().Perform();
                    //var point = plus.Location;
                    //actions.MoveByOffset(point.X, point.Y).Click().Perform();
                    //plus.Click();
                    Console.WriteLine(" done.");
                }
                catch (Exception)
                {
                    Console.WriteLine(" NOT done.");
                }
            }
        }

        public static void LikeLastTenPosts(this IWebDriver d, string url)
        {
            //d.Navigate().GoToUrl(url);

            //var wait = new WebDriverWait(d, TimeSpan.FromSeconds(30));
            //wait.Until(ExpectedConditions
            //    .ElementIsVisible(By
            //    .XPath(".//div[contains(@class, 'voting')]/i[contains(@class, 'plus')]")));
            
            var pluses = d.FindElements(By.XPath(".//div[contains(@class, 'voting')]/i[contains(@class, 'plus')]"));
            foreach (var plus in pluses)
            {
                try
                {
                    var js = (IJavaScriptExecutor)d;
                    var actions = new Actions(d);
                    Thread.Sleep(500);
                    actions.MoveToElement(plus).Build().Perform();
                    Thread.Sleep(500);
                    js.ExecuteScript("window.scrollBy(0, -100)", "");
                    Thread.Sleep(500);
                    actions.Click(plus).Perform();
                    //plus.Click();
                    Thread.Sleep(200);
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }
            }
        }

        public static void Subscribe(this IWebDriver d, string url)
        {
            d.Navigate().GoToUrl(url);
            var actions = new Actions(d);
            var wait = new WebDriverWait(d, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By
                .XPath("(.//span[contains(@class, 'like')]/i[contains(text(), 'Подписаться на блог')])")));

            actions.MoveToElement(d.FindElement(By
                .XPath("(.//span[contains(@class, 'like')]/i[contains(text(), 'Подписаться на блог')])")))
                .Build().Perform();
            Thread.Sleep(1000);
            var js = (IJavaScriptExecutor)d;
            js.ExecuteScript("window.scrollBy(0, -100)", "");
            Thread.Sleep(1000);
            actions.Click(d.FindElement(By
                .XPath("(.//span[contains(@class, 'like')]/i[contains(text(), 'Подписаться на блог')])")))
                .Perform();

            //d.FindElement(By
            //    .XPath("(.//span[contains(@class, 'like')]/i[contains(text(), 'Подписаться на блог')])")).Click();
            
        }
    }
}
