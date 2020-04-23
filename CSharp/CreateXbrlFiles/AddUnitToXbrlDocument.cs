using Aspose.Finance.Xbrl;
using System;

namespace CSharp.CreateXbrlFiles
{
    class AddUnitToXbrlDocument
    {
        public static void Run()
        {
            // ExStart:1
            // Output directory
            string outputDir = RunExamples.Get_OutputDirectory();

            XbrlDocument document = new XbrlDocument();
            XbrlInstanceCollection xbrlInstances = document.XbrlInstances;
            XbrlInstance xbrlInstance = xbrlInstances[xbrlInstances.Add()];
            Unit unit = new Unit(UnitType.Measure);
            unit.MeasureQualifiedNames.Add(new QualifiedName("USD", "iso4217", "http://www.xbrl.org/2003/iso4217"));
            xbrlInstance.Units.Add(unit);
            document.Save(outputDir + @"document4.xbrl");
            // ExEnd:1

            Console.WriteLine("AddUnitToXbrlDocument executed successfully.");
        }
    }
}
