using Aspose.Finance.Ofx;
using System;

namespace CSharp.WorkingWithOfxFiles
{
    class ConvertOfxRequestFileToOfxRequestV2
    {
        public static void Run()
        {
            // ExStart:1
            // Working directories
            string sourceDir = RunExamples.Get_SourceDirectory();
            string outputDir = RunExamples.Get_OutputDirectory();

            OfxRequestDocument document = new OfxRequestDocument(sourceDir + @"bankTransactionReq.sgml");
            document.Save(outputDir + @"bankTransactionReq.xml", OfxVersionEnum.V2x);
            // ExEnd:1

            Console.WriteLine("ConvertOfxRequestFileToOfxRequestV2 executed successfully.");
        }
    }
}
