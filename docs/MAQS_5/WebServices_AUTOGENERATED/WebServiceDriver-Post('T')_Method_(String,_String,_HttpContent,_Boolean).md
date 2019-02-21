# WebServiceDriver.Post(*T*) Method (String, String, HttpContent, Boolean)
 

Execute a web service post

**Namespace:**&nbsp;<a href="MAQS_5/WebServices_AUTOGENERATED/Magenic-Maqs-BaseWebServiceTest_Namespace">Magenic.Maqs.BaseWebServiceTest</a><br />**Assembly:**&nbsp;Magenic.Maqs.WebServiceTester (in Magenic.Maqs.WebServiceTester.dll) Version: 5.3.0

## Syntax

**C#**<br />
``` C#
public T Post<T>(
	string requestUri,
	string expectedMediaType,
	HttpContent content,
	bool expectSuccess = true
)

```


#### Parameters
&nbsp;<dl><dt>requestUri</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The request uri</dd><dt>expectedMediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The type of media being requested</dd><dt>content</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/hh193687" target="_blank">System.Net.Http.HttpContent</a><br />The post content</dd><dt>expectSuccess (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Assert a success code was returned</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>The expected response type</dd></dl>

#### Return Value
Type: *T*<br />The response deserialized as - *T*

## Examples

**C#**<br />
``` C#
!ERROR: See log file!
```


## See Also


#### Reference
<a href="MAQS_5/WebServices_AUTOGENERATED/WebServiceDriver_Class">WebServiceDriver Class</a><br /><a href="MAQS_5/WebServices_AUTOGENERATED/WebServiceDriver-Post_Method">Post Overload</a><br /><a href="MAQS_5/WebServices_AUTOGENERATED/Magenic-Maqs-BaseWebServiceTest_Namespace">Magenic.Maqs.BaseWebServiceTest Namespace</a><br />