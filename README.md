# Aspose.Finance for .NET

[Aspose.Finance for .NET](https://products.aspose.com/finance/net) Aspose.Finance, as a pure .NET library provides much better performance and ease of use to manipulate finance-related formats, such as XBRL, iXBRL. The finance API is extensible, easy to use and compact and provides all common functionality so developers write less code to do common operations.

<p align="center">
<a title="Download complete Aspose.Finance for .NET source code" href="https://github.com/aspose-finance/Aspose.Finance-for-.NET/archive/master.zip">
	<img src="https://raw.github.com/AsposeExamples/java-examples-dashboard/master/images/downloadZip-Button-Large.png" />
  </a>
</p>

This repository contains [Examples](Examples), for [Aspose.Finance for .NET](https://products.aspose.com/finance/net) to help you learn and write your applications.

Directory | Description
--------- | -----------
[CSharp](CSharp)  | A collection of .NET examples that help you learn the product features
[Data](Data)  | Data files used in the examples

# .NET API to Manipulate Finance-related Formats

[Aspose.Finance for .NET](https://products.aspose.com/finance/net) is a standalone on-premise API consisting of C# classes that allow you to process and manipuate finance-related formats, such as, XBRL and iXBRL from within your .NET applications.

## Finance Processing Features

- [Create XBRL instance](https://docs.aspose.com/display/financenet/Create+XBRL+files#CreateXBRLfiles-CreateXBRLInstance) from scratch.
- Read XBRL & iXBRL format.
- Supports XBRL & iXBRL validation.
- Import and export XBRL & iXBRL format files.
- Handle the abstract element in XBRL taxonomy.

## Read Finance Formats

XBRL, iXBRL

## Platform Independence

Aspose.Finance for .NET is implemented using Managed C# and can be used with any .NET language like C#, VB.NET, F# and so on. It can be integrated with any kind of .NET application, from ASP.NET web applications to Windows .NET applications.

## Getting Started with Aspose.Finance for .NET

Are you ready to give Aspose.Finance for .NET a try? Simply execute `Install-Package Aspose.Finance` from Package Manager Console in Visual Studio to fetch the NuGet package. If you already have Aspose.Finance for .NET and want to upgrade the version, please execute `Update-Package Aspose.Finance` to get the latest version.

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
