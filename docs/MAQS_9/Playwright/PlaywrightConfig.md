# <img src="resources/maqslogo.ico" height="32" width="32"> Playwright Configuration

## Overview
The PlaywrightConfig class is used to get values from the PlaywrightMaqs section of your test run properties.
<br>These values come from your App.config, appsettings.json and/or test run parameters.

## PlaywrightMaqs
The PlaywrightMaqs configuration section contains the following Keys:  
* ***WebBase*** : The base website url
* ***BrowserType*** : Enum representing the desire browser
* ***BrowserName*** : String representing the desire browser  
*This value should only be used for validation, MAQS uses 'GetBrowserType'
* ***Headless*** : If we want Playwright tests to be run headless
* ***CommandTimeout*** : How long wait before saying the connection to Playwright has died
* ***TimeoutTime*** : How long to wait for something before timing out
* ***CaptureVideo*** : If you want to capture video
* ***CaptureScreenshots*** : If you want to capture screenshots  
*This option can bloat your test result, so be very careful about using it with big test runs
* ***CaptureSnapshots*** : If we want to capture snapshots (DOM and network activity) on each action
* ***BrowserSize*** : The browser resolution
* ***UseProxy*** : If the browser should use a proxy address
    * If this value is "YES" then ***ProxyAddress*** is required
* ***ProxyAddress*** : The proxy address and port the browser will use

## Available methods
Get the base web site url:
```csharp
string siteUrl = PlaywrightConfig.WebBase();
```
Get the browser name:
```csharp
string browserName = PlaywrightConfig.BrowserName();
```
Get a page driver type based on your configuration:
```csharp
PlaywrightBrowser type = PlaywrightConfig.BrowserType();
```
Get a page driver type for the specified browser:
```csharp
BrowserType type = PlaywrightConfig.BrowserType("Chrome");
```
Get the command timeout:
```csharp
int initTimeout = PlaywrightConfig.CommandTimeout();
```
Get the search timeout:
```csharp
int findTimeout = PlaywrightConfig.TimeoutTime();
```
Get if we capture video:
```csharp
bool captureVideo = PlaywrightConfig.CaptureVideo();
```
Get if we capture screenshots:
```csharp
bool captureScreenshots = PlaywrightConfig.CaptureScreenshots();
```
Get if we capture snapshots:
```csharp
bool captureSnapshots = PlaywrightConfig.CaptureSnapshots();
```

Get the if page driver should use proxy
```csharp
bool useProxy = PlaywrightConfig.GetUseProxy();
```
Get the proxy address to use
```csharp
string proxyAddress = PlaywrightConfig.GetProxyAddress();
```

# Sample config files
## App.config
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="PlaywrightMaqs" type="System.Configuration.NameValueSectionHandler"/>
    <section name="RemotePlaywrightCapsMaqs" type="System.Configuration.NameValueSectionHandler"/>
    <section name="GlobalMaqs" type="System.Configuration.NameValueSectionHandler" />
  </configSections>
	<PlaywrightMaqs>
		<!--Default base web url-->
		<add key="WebBase" value="https://cognizantopensource.github.io/maqs-dotnet-templates/Static/Automation/" />

		<!--Local browser settings
		<add key="Browser" value="Chrome"/>
		<add key="Browser" value="Chromium"/>
		<add key="Browser" value="Firefox"/>
		<add key="Browser" value="Edge"/>
		<add key="Browser" value="Webkit"/>-->
		<add key="Browser" value="Chromium" />
		<add key="Headless" value="YES" />

		<!--Playwright specific timeouts in milliseconds-->
		<add key="Timeout" value="20000" />
		<add key="CommandTimeout" value="200000" />

		<!--Playwright specific logging options-->
		<add key="CaptureVideo" value="No" />
		<add key="CaptureScreenshots" value="No" />
		<add key="CaptureSnapshots" value="No" />

		<!--Browser Resize settings - The Default is 1280x720
		<add key="BrowserSize" value="DEFAULT" />
		<add key="BrowserSize" value="300x300" />-->

		<!--Proxy  settings-->
		<add key="UseProxy" value="No" />
		<add key="ProxyAddress" value="http://localhost:8002" />
	</PlaywrightMaqs>
  <GlobalMaqs>
    <!-- Generic wait time in milliseconds - AKA how long do you wait for rechecking something -->
    <add key="WaitTime" value="1000" />

    <!-- Generic time-out in milliseconds -->
    <add key="Timeout" value="10000" />

    <!-- Do you want to create logs for your tests
    <add key="Log" value="YES"/>
    <add key="Log" value="NO"/>
    <add key="Log" value="OnFail"/>-->
    <add key="Log" value="OnFail" />

    <!--Logging Levels
    <add key="LogLevel" value="VERBOSE"/>
    <add key="LogLevel" value="INFORMATION"/>
    <add key="LogLevel" value="GENERIC"/>
    <add key="LogLevel" value="SUCCESS"/>
    <add key="LogLevel" value="WARNING"/>
    <add key="LogLevel" value="ERROR"/>-->
    <add key="LogLevel" value="INFORMATION" />

    <!-- Logging Types
    <add key="LogType" value="CONSOLE"/>
    <add key="LogType" value="TXT"/>
    <add key="LogType" value="HTML"/>-->
    <add key="LogType" value="TXT" />

    <!-- Log file path - Defaults to build location if no value is defined
    <add key="FileLoggerPath" value="C:\Frameworks\"/>-->
  </GlobalMaqs>
</configuration>
```
## appsettings.json
```json
{
  "PlaywrightMaqs": {
    "WebBase": "https://cognizantopensource.github.io/maqs-dotnet-templates/Static/Automation/",
    "Browser": "Chrome",
    "Headless": "YES",
    "CommandTimeout": "60000",
    "Timeout": "10000",
    "BrowserSize": "DEFAULT",
    "CaptureVideo": "NO",
    "CaptureScreenshots": "NO",
    "CaptureSnapshots": "NO",
    "UseProxy": "NO",
    "ProxyAddress": "127.0.0.1:8080"
  }
  "GlobalMaqs": {
    "WaitTime": "100",
    "Timeout": "10000",
    "Log": "OnFail",
    "LogLevel": "INFORMATION",
    "LogType": "TXT"
  }
}
```