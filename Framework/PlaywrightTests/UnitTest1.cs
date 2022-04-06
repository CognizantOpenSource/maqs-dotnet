using CognizantSoftvision.Maqs.BasePlaywrightTest;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaywrightTests
{
    [TestClass]
    public class UnitTest1 : BasePlaywrightTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            this.PageDriver.Goto(PlaywrightConfig.GetWebBase());
            this.PageDriver.Hover("text=Automation",  new PageHoverOptions
            {
                Force = true
            });


            var newPage = PageDriverFactory.GetPageDriverFromBrowser(this.PageDriver.ParentBrower);
            newPage.Goto(PlaywrightConfig.GetWebBase());


            // Click text=Page Elements
            this.PageDriver.Click("text=Page Elements");

            // Click text=500 Error page
            this.PageDriver.Click("text=Async page");

            this.PageDriver.SelectOption("select", new[] { "Second" });
            this.PageDriver.Click("text=Loaded");

            Assert.Fail();
        }

        [TestMethod]
        public void TestMethod14()
        {
            var page = this.PageDriver.AsyncPage;
            var restul = page.GotoAsync("https://cognizantopensource.github.io/maqs-dotnet-templates/Static/Automation/").Result;

            page.Locator("text=Automation").HoverAsync(new LocatorHoverOptions
            {
                Force = true
            }).Wait();

            // Click text=Page Elements
            page.ClickAsync("text=Page Elements").Wait();

        }


        [TestMethod]
        public void TestMethod13()
        {
            var page = this.PageDriver.AsyncPage;
            var restul = page.GotoAsync("https://cognizantopensource.github.io/maqs-dotnet-templates/Static/Automation/").Result;

            page.Locator("text=Automation").HoverAsync(new LocatorHoverOptions
            {
                Force = true
            }).Wait();

            // Click text=Page Elements
            page.ClickAsync("text=Page Elements").Wait();

            // Click text=500 Error page
            page.ClickAsync("text=Async page").Wait();

            page.Locator("select").SelectOptionAsync(new[] { "Second" }).Wait();
            page.Locator("text=Loaded").ClickAsync().Wait();
        }

        [TestMethod]
        public void TestMethod12()
        {
            var page = this.PageDriver.AsyncPage;
            var restul = page.GotoAsync("https://cognizantopensource.github.io/maqs-dotnet-templates/Static/Automation/").Result;

            page.Locator("text=Automation").HoverAsync(new LocatorHoverOptions
            {
                Force = true
            }).Wait();

            // Click text=Page Elements
            page.ClickAsync("text=Page Elements").Wait();

            // Click text=500 Error page
            page.ClickAsync("text=Async page").Wait();

            page.Locator("select").SelectOptionAsync(new[] { "Second" }).Wait();
            page.Locator("text=Loaded").ClickAsync().Wait();
        }


        [TestMethod]
        public void TestMethod11()
        {
            var page = this.PageDriver.AsyncPage;
            var restul = page.GotoAsync("https://cognizantopensource.github.io/maqs-dotnet-templates/Static/Automation/").Result;

            page.Locator("text=Automation").HoverAsync(new LocatorHoverOptions
            {
                Force = true
            }).Wait();

            // Click text=Page Elements
            page.ClickAsync("text=Page Elements").Wait();

            // Click text=500 Error page
            page.ClickAsync("text=Async page").Wait();

            Assert.Fail();
        }
    }
}
