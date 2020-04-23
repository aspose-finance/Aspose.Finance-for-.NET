using Aspose.Finance.Xbrl;
using System;

namespace CSharp.CreateXbrlFiles
{
    class AddContextToXbrlDocument
    {
        public static void Run()
        {
            // ExStart:1
            // Output directory
            string outputDir = RunExamples.Get_OutputDirectory();

            XbrlDocument document = new XbrlDocument();
            XbrlInstanceCollection xbrlInstances = document.XbrlInstances;
            XbrlInstance xbrlInstance = xbrlInstances[xbrlInstances.Add()];
            ContextPeriod contextPeriod = new ContextPeriod(DateTime.Parse("2020-01-01"), DateTime.Parse("2020-02-10"));
            ContextEntity contextEntity = new ContextEntity("exampleIdentifierScheme", "exampleIdentifier");
            Context context = new Context(contextPeriod, contextEntity);
            xbrlInstance.Contexts.Add(context);
            document.Save(outputDir + @"document3.xbrl");
            // ExEnd:1

            Console.WriteLine("AddContextToXbrlDocument executed successfully.");
        }
    }
}
