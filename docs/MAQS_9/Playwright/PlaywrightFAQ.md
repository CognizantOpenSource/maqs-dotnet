# <img src="resources/maqslogo.ico" height="32" width="32"> Playwright FAQ

## Is there any setup required
- Yes, see https://playwright.dev/dotnet/docs/intro for more details

## What Browsers can I use?
- Chromium
- Chrome
- Edge (latest Chromium base version) 
- Firefox
- WebKit  

## Why add Playwright when Selenium already exists
- Admittedly Playwright and Selenium  are very similar tools.  The however have their owns strengths and weaknesses. 
- Selenium supports more languages and browser/OS combination than Playwright.  The lack of IE support is a deal breaker for many projects. 
- Playwright is faster then Selenium, has smart waiting/scrolling built in, and impressing test reporting capablities.  

## Does Playwright support something like Selenium grid or do all the test need to run locally
- Yes!  
  You can use Selenium Grid 4 (or Sauce Labs or BrowserStack) to run tests against Chrome or the Chromium based versions Edge. 

## Where can I get more information about Playwright
- https://playwright.dev/dotnet/docs/intro
- https://github.com/microsoft/playwright-dotnet


## How do I look at the zip files created for Playwright tests
- These files can be viewed with the Playwright trace viewer:  
https://playwright.dev/dotnet/docs/trace-viewer
