using Aspose.Finance.Ofx;
using System;

namespace CSharp.WorkingWithOfxFiles
{
    class ConvertOfxResponseFileToOfxResponseV2
    {
        public static void Run()
        {
            // ExStart:1
            // Working directories
            string sourceDir = RunExamples.Get_SourceDirectory();
            string outputDir = RunExamples.Get_OutputDirectory();

            OfxResponseDocument document = new OfxResponseDocument(sourceDir + @"bankTransactionRes.sgml");
            document.Save(outputDir + @"bankTransactionRes.xml", OfxVersionEnum.V2x);
            // ExEnd:1

            Console.WriteLine("ConvertOfxResponseFileToOfxResponseV2 executed successfully.");
        }
    }
}
