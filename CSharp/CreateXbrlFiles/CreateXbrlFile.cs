using Aspose.Finance.Xbrl;
using System;

namespace CSharp.CreateXbrlFiles
{
    class CreateXbrlFile
    {
        public static void Run()
        {
            // ExStart:1
            // Output directory
            string outputDir = RunExamples.Get_OutputDirectory();

            XbrlDocument document = new XbrlDocument();
            XbrlInstanceCollection xbrlInstances = document.XbrlInstances;
            XbrlInstance xbrlInstance = xbrlInstances[xbrlInstances.Add()];
            document.Save(outputDir + @"document1.xbrl");
            // ExEnd:1

            Console.WriteLine("CreateXbrlFile executed successfully.");
        }
    }
}
