# WebServiceDriver.Custom Method (String, String, String, String, Encoding, String, Boolean, Boolean)
 

Execute a web service call with a custom verb

**Namespace:**&nbsp;<a href="MAQS_5/WebServices_AUTOGENERATED/Magenic-Maqs-BaseWebServiceTest_Namespace">Magenic.Maqs.BaseWebServiceTest</a><br />**Assembly:**&nbsp;Magenic.Maqs.WebServiceTester (in Magenic.Maqs.WebServiceTester.dll) Version: 5.3.0

## Syntax

**C#**<br />
``` C#
public string Custom(
	string customType,
	string requestUri,
	string expectedMediaType,
	string content,
	Encoding contentEncoding,
	string postMediaType,
	bool contentAsString = true,
	bool expectSuccess = true
)
```


#### Parameters
&nbsp;<dl><dt>customType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Custom HTTP Verb</dd><dt>requestUri</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The request URI</dd><dt>expectedMediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The expected media type</dd><dt>content</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Content of the message</dd><dt>contentEncoding</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/86hf4sb8" target="_blank">System.Text.Encoding</a><br />How content was encoded</dd><dt>postMediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Media type</dd><dt>contentAsString (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />The message content as a string</dd><dt>expectSuccess (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Assert a success code was returned</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">String</a><br />The HTTP Response message

## See Also


#### Reference
<a href="MAQS_5/WebServices_AUTOGENERATED/WebServiceDriver_Class">WebServiceDriver Class</a><br /><a href="MAQS_5/WebServices_AUTOGENERATED/WebServiceDriver-Custom_Method">Custom Overload</a><br /><a href="MAQS_5/WebServices_AUTOGENERATED/Magenic-Maqs-BaseWebServiceTest_Namespace">Magenic.Maqs.BaseWebServiceTest Namespace</a><br />