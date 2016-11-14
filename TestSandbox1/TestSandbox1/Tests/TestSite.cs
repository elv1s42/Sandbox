using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TestSandbox1.Tests.Helpers;

namespace TestSandbox1.Tests
{
    [TestFixture]
    public class TestSite
    {
        public IWebDriver D { get; set; }
        public Process TorProcess { get; set; }
        public WebDriverWait Wait { get; set; }

        public void SetupTest()
        {
            const string torBinaryPath = @"C:\TorBrowser\Browser\firefox.exe";
            TorProcess = new Process
            {
                StartInfo =
                {
                    FileName = torBinaryPath,
                    Arguments = "-n",
                    WindowStyle = ProcessWindowStyle.Maximized
                }
            };
            TorProcess.Start();

            var profile = new FirefoxProfile();
            profile.SetPreference("network.proxy.type", 1);
            profile.SetPreference("network.proxy.socks", "127.0.0.1");
            profile.SetPreference("network.proxy.socks_port", 9150);
            D = new FirefoxDriver(profile);
            Wait = new WebDriverWait(D, TimeSpan.FromSeconds(60));
        }

        public void TeardownTest()
        {
            D.Quit();
            TorProcess.Kill();
        }

        [Test]
        public void TryLoginSportsRu()
        {
            var auths = AuthSportsRu.GetFullList();
            foreach (var auth in auths)
            {
                try
                {
                    SetupTest();
                    D.TryLoginSportsRu(auth.Email, auth.Password);
                    TeardownTest();
                }
                catch (Exception)
                {
                    Console.WriteLine(auth.Email + " " + auth.Password + " not working");
                }
            }
            
        }

        //[TestCase(1)]
        //[TestCase(2)]
        //[TestCase(3)]
        public void Open_Tor_Browser(int s)
        {
            D.Navigate().GoToUrl(@"http://whatismyipaddress.com/");
            var expression = By.XPath("//*[@id='section_left']/div[2]");
            Wait.Until(x => x.FindElement(expression));
            var element = D.FindElement(expression);
            Console.WriteLine("84.40.65.000" + " " + element.Text);
            Assert.AreNotEqual("84.40.65.000", element.Text);
        }

        //TODO: add random emails
        //[TestCase(300)]
        public void GetRegistrationsSportsRu(int count)
        {
            for (var i = 0; i < count; i++)
            {
                try
                {
                    const string sportsUrl = "https://sports.ru/";
                    //using (D)
                    //{
                        var email = SportsRuHelper.GetRandomEmail();
                        var username = email.Split('@').First();
                        var password = Guid.NewGuid().ToString("d").Substring(0, 8).ToLower();
                        D.RegSportsRu(sportsUrl, email, password, username);
                    //}
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
            }
        }

        [TestCase(500)]
        public void GetRegistrationsForSportsRu(int count)
        {
            for (var i = 0; i < count; i++)
            {
                SetupTest();
                try
                {
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
                        var password = Guid.NewGuid().ToString("d").Substring(0, 8).ToLower();
                        D.RegSportsRu(sportsUrl, email, password, username);
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
                TeardownTest();
            }
        }
        
        [TestCase(10, "")]
        [TestCase(10, " ")]
        public void PutLikesToLastTenPost(int count, string url)
        {
            for (var i = 0; i < count; i++)
            {
                try
                {
                    //using (D)
                    //{
                        try
                        {
                            var a = AuthSportsRu.GetRandomItem();
                            D.LoginSportsRu(a.Email, a.Password);
                            Console.WriteLine("Log in done!");
                            D.Subscribe(url);
                            Console.WriteLine("Subscribed!");
                        }
                        catch
                        {
                            Thread.Sleep(500);
                        }
                        try
                        {
                            D.LikeLastTenPosts(url);
                            Console.WriteLine("Post liked!");
                        }
                        catch
                        {
                            Thread.Sleep(500);
                        }
                    //}
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
            }
        }

        [TestCase(1)]
        [TestCase(25)]
        [TestCase(50)]
        [TestCase(500)]
        public void PutLikesToLastPost(int count)
        {
            var urls = new List<string>
            {
                //"http://www.sports.ru/tribuna/blogs/barcaonline/",
                //"http://www.sports.ru/tribuna/blogs/smonline/",
                //"http://www.sports.ru/tribuna/blogs/sportsweekend/",
                "http://www.sports.ru/tribuna/blogs/funpics/"
            };

            for (var i = 0; i < count; i++)
            {
                try
                {
                    SetupTest();
                    try
                    {
                        const string sportsUrl = "https://sports.ru/";
                        var email = SportsRuHelper.GetRandomEmail();
                        var username = email.Split('@').First();
                        var password = Guid.NewGuid().ToString("d").Substring(0, 8).ToLower();
                        D.RegSportsRu(sportsUrl, email, password, username);
                        Console.WriteLine("Reg is done!");
                        foreach (var url in urls)
                        {
                            try
                            {
                                D.Subscribe(url);
                                Console.WriteLine("Subscribed!");
                                D.LikeLastTenPosts(url);
                                Console.WriteLine("Post liked!");
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(500);
                            }
                        }
                    }
                    catch
                    {
                        Thread.Sleep(500);
                    }
                    TeardownTest();
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
            }
        }

        [TestCase(50, @"http://www.sports.ru/football/1037018186.html")]
        [TestCase(50, @"http://www.sports.ru/football/1036882356.html")]
        [TestCase(50, @"http://www.sports.ru/football/1037013785.html")]
        [TestCase(50, @"http://www.sports.ru/football/1037023240.html")]
        [TestCase(50, @"http://www.sports.ru/football/1037016222.html")]
        [TestCase(50, @"http://www.sports.ru/football/1037020906.html")]
        [TestCase(50, @"http://www.sports.ru/football/1037015816.html")]
        [TestCase(50, @"http://www.sports.ru/football/146665972.html")]
        public void PutLikesToComment(int count, string url)
        {
            for (var i = 0; i < count; i++)
            {
                //using (D)
                //{
                    try
                    {
                        var a = AuthSportsRu.GetRandomItem();
                        D.LoginSportsRu(a.Email, a.Password);
                        Console.WriteLine("Log in done!");
                        D.LikeComments(url);
                        Console.WriteLine("Comments liked!");
                    }
                    catch
                    {
                        Thread.Sleep(500);
                    }
                //}
            }
        }

        //[Test]
        public void PutLikesToComment_Test()
        {
            for (var i = 0; i < 1; i++)
            {
                //using (D)
                //{
                    try
                    {
                        var a = AuthSportsRu.GetRandomItem();
                        D.LoginSportsRu(a.Email, a.Password);
                        Console.WriteLine("Log in done!");
                        D.LikeComments("http://www.sports.ru/football/146665972.html");
                    
                        Console.WriteLine("Comments liked!");
                    }
                    catch
                    {
                        Thread.Sleep(500);
                    }
                //}
            }
        }
    }
}
