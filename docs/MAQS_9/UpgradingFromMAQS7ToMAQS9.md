# Updating from MAQS 8 to MAQS 9

For the most part MAQS should be able to seamlessly update from 8 to 9.

## Caveat  

In this version of MAQS we are dropping Core 2.1 support for SpecFlow based tests, so you may need to update to Core 3.1 or above.


# Updating from MAQS 7 to MAQS 9

## Namespace
The name 'CognizantSoftvision' is replacing 'Magenic' in all namespaces. 
### Example
```csharp
// Old namespaces
// using Magenic.Maqs.Utilities.Logging;

// New namespaces
using CognizantSoftvision.Maqs.Utilities.Logging;
```

## Config
The 'MagenicMaqs' section of the configuration is being renamed 'GlobalMaqs'.
### Example (XML)
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="GlobalMaqs" type="System.Configuration.NameValueSectionHandler" />
    <!-- OLD <section name="MagenicMaqs" type="System.Configuration.NameValueSectionHandler" />-->
  </configSections>
  <GlobalMaqs>
    <add key="WaitTime" value="1000" />
    <add key="Timeout" value="10000" />
    <add key="Log" value="OnFail" />
    <add key="LogLevel" value="INFORMATION" />
    <add key="LogType" value="TXT" />
  </GlobalMaqs>
  <!-- OLD <MagenicMaqs>
    <add key="WaitTime" value="1000" />
    <add key="Timeout" value="10000" />
    <add key="Log" value="OnFail" />
    <add key="LogLevel" value="INFORMATION" />
    <add key="LogType" value="TXT" />
  </MagenicMaqs>-->
</configuration>
```
### Example (JSON)
```json
{
  "GlobalMaqs": {
    "WaitTime": "100",
    "Timeout": "10000",
    "Log": "OnFail",
    "LogLevel": "INFORMATION",
    "LogType": "TXT",
    "UseFirstChanceHandler": "YES",
    "SkipConfigValidation": "NO",
  }
}
```

