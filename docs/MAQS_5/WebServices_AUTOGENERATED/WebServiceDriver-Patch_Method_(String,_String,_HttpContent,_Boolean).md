# WebServiceDriver.Patch Method (String, String, HttpContent, Boolean)
 

Execute a web service patch

**Namespace:**&nbsp;<a href="MAQS_5/WebServices_AUTOGENERATED/Magenic-Maqs-BaseWebServiceTest_Namespace">Magenic.Maqs.BaseWebServiceTest</a><br />**Assembly:**&nbsp;Magenic.Maqs.WebServiceTester (in Magenic.Maqs.WebServiceTester.dll) Version: 5.3.0

## Syntax

**C#**<br />
``` C#
public string Patch(
	string requestUri,
	string expectedMediaType,
	HttpContent content,
	bool expectSuccess = true
)
```


#### Parameters
&nbsp;<dl><dt>requestUri</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The request uri</dd><dt>expectedMediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The type of media being requested</dd><dt>content</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/hh193687" target="_blank">System.Net.Http.HttpContent</a><br />The put content</dd><dt>expectSuccess (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Assert a success code was returned</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">String</a><br />The response body as a string

## Examples

**C#**<br />
``` C#
!ERROR: See log file!
```


## See Also


#### Reference
<a href="MAQS_5/WebServices_AUTOGENERATED/WebServiceDriver_Class">WebServiceDriver Class</a><br /><a href="MAQS_5/WebServices_AUTOGENERATED/WebServiceDriver-Patch_Method">Patch Overload</a><br /><a href="MAQS_5/WebServices_AUTOGENERATED/Magenic-Maqs-BaseWebServiceTest_Namespace">Magenic.Maqs.BaseWebServiceTest Namespace</a><br />