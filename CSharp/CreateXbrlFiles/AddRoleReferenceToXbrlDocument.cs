using Aspose.Finance.Xbrl;
using System;

namespace CSharp.CreateXbrlFiles
{
    class AddRoleReferenceToXbrlDocument
    {
        public static void Run()
        {
            // ExStart:1
            // Source directory
            string sourceDir = RunExamples.Get_SourceDirectory();

            // Output directory
            string outputDir = RunExamples.Get_OutputDirectory();

            XbrlDocument document = new XbrlDocument();
            XbrlInstanceCollection xbrlInstances = document.XbrlInstances;
            XbrlInstance xbrlInstance = xbrlInstances[xbrlInstances.Add()];
            SchemaRefCollection schemaRefs = xbrlInstance.SchemaRefs;
            schemaRefs.Add(sourceDir + @"schema.xsd", "example", "http://example.com/xbrl/taxonomy");
            SchemaRef schema = schemaRefs[0];
            RoleType roleType = schema.GetRoleTypeByURI("http://abc.com/role/link1");
            if (roleType != null)
            {
                RoleReference roleReference = new RoleReference(roleType);
                xbrlInstance.RoleReferences.Add(roleReference);
            }
            document.Save(outputDir + @"document7.xbrl");
            // ExEnd:1

            Console.WriteLine("AddRoleReferenceToXbrlDocument executed successfully.");
        }
    }
}
