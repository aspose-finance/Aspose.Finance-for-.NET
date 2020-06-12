# Aspose.Finance for .NET

[Aspose.Finance for .NET](https://products.aspose.com/finance/net) Aspose.Finance, as a pure .NET library provides much better performance and ease of use to manipulate finance-related formats, such as XBRL, iXBRL. The finance API is extensible, easy to use and compact and provides all common functionality so developers write less code to do common operations.

This repository contains [Examples](Examples), for [Aspose.Finance for .NET](https://products.aspose.com/finance/net) to help you learn and write your applications.

<p align="center">
<a title="Download complete Aspose.Finance for .NET source code" href="https://github.com/aspose-finance/Aspose.Finance-for-.NET/archive/master.zip">
	<img src="https://raw.github.com/AsposeExamples/java-examples-dashboard/master/images/downloadZip-Button-Large.png" />
  </a>
</p>

Directory | Description
--------- | -----------
[CSharp](CSharp)  | A collection of .NET examples that help you learn the product features
[Data](Data)  | Data files used in the examples

<h2 id="finance-processing-features">Finance Processing Features</h2>
<ul>
<li><a href="https://docs.aspose.com/display/financenet/Create+XBRL+files#CreateXBRLfiles-CreateXBRLInstance">Create XBRL instance</a> from scratch.</li>
<li>Read XBRL &amp; iXBRL format.</li>
<li>Supports XBRL &amp; iXBRL validation.</li>
<li>Import and export XBRL &amp; iXBRL format files.</li>
<li>Handle the abstract element in XBRL taxonomy.</li>
</ul>
<h2 id="read-finance-formats">Read Finance Formats</h2>
<p>XBRL, iXBRL</p>
<h2 id="platform-independence">Platform Independence</h2>
<p>Aspose.Finance for .NET is implemented using Managed C# and can be used with any .NET language like C#, VB.NET, F# and so on. It can be integrated with any kind of .NET application, from ASP.NET web applications to Windows .NET applications.</p>
<h2 id="getting-started-with-aspose-finance-for-net">Getting Started with Aspose.Finance for .NET</h2>
<p>Are you ready to give Aspose.Finance for .NET a try? Simply execute <code>Install-Package Aspose.Finance</code> from Package Manager Console in Visual Studio to fetch the NuGet package. If you already have Aspose.Finance for .NET and want to upgrade the version, please execute <code>Update-Package Aspose.Finance</code> to get the latest version.</p>



## How to Run the Examples

* You can either clone the repository using your favorite GitHub client or download the ZIP file from here.
* Extract the contents of the ZIP file to any folder on your computer. All the examples are located in the Examples folder.
* Open the solution file in Visual Studio and build the project.
* On the first run, the dependencies will automatically be downloaded via NuGet.
* Data folder at the root folder of Examples contains input files. It is mandatory that you download the Data folder along with the examples project.
* Open **RunExamples.cs**, all the examples are called from here.
* Uncomment the examples you want to run from within the project.

Please find more details on how to run the examples [here](https://docs.aspose.com/display/financenet/How+to+Run+the+Examples).

## Add Role Reference using C# Code

You can execute the following code snippet to see how Aspose.Finance API works in your environment or check the [GitHub Repository](https://github.com/aspose-finance/Aspose.finance-for-.NET) for other common usage scenarios.

```csharp
XbrlDocument document = new XbrlDocument();
XbrlInstanceCollection xbrlInstances = document.XbrlInstances;
XbrlInstance xbrlInstance = xbrlInstances[xbrlInstances.Add()];
SchemaRefCollection schemaRefs = xbrlInstance.SchemaRefs;
schemaRefs.Add(XbrlFilePath + @"schema.xsd", "example", "http://example.com/xbrl/taxonomy");
SchemaRef schema = schemaRefs[0];
RoleType roleType = schema.GetRoleTypeByURI("http://abc.com/role/link1");
if (roleType != null)
{
    RoleReference roleReference = new RoleReference(roleType);
    xbrlInstance.RoleReferences.Add(roleReference);
}
document.Save(XbrlFilePath + @"output\document7.xbrl");
```

[Product Page](https://products.aspose.com/finance/net) | [Docs](https://docs.aspose.com/display/financenet/Home) | [Demos](https://products.aspose.app/finance/family) | [API Reference](https://apireference.aspose.com/finance/net) | [Examples](https://github.com/aspose-finance/Aspose.finance-for-.NET) | [Blog](https://blog.aspose.com/category/finance/) | [Free Support](https://forum.aspose.com/c/finance) | [Temporary License](https://purchase.aspose.com/temporary-license)
