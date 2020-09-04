using Aspose.Finance.Xbrl;
using System;
using System.Collections.Generic;

namespace CSharp.Conversion
{
    class ConvertXbrlToXlsx
    {
        public static void Run()
        {
            // ExStart:1
            // Working directories
            string sourceDir = RunExamples.Get_SourceDirectory();
            string outputDir = RunExamples.Get_OutputDirectory();

            XbrlDocument document = new XbrlDocument(sourceDir + @"IdScopeContextPeriodStartAfterEnd.xml");

            // Set save options
            SaveOptions saveOptions = new SaveOptions();
            saveOptions.SaveFormat = SaveFormat.XLSX;

            // Save file to XLSX format
            document.Save(outputDir + @"ConvertXbrlToXlsx_out.xlsx", saveOptions);
            // ExEnd:1

            Console.WriteLine("ConvertXbrlToXlsx executed successfully.");
        }
    }
}
