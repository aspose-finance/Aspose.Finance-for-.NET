using Aspose.Finance.Xbrl;
using Aspose.Finance.Xbrl.Inline;
using System;
using System.Collections.Generic;

namespace CSharp.ReadXbrlFiles
{
    class ReadIxbrlDocument
    {
        public static void Run()
        {
            // ExStart:1
            // Source directory
            string sourceDir = RunExamples.Get_SourceDirectory();

            InlineXbrlDocument document = new InlineXbrlDocument(sourceDir + @"account_1.html");
            List<InlineFact> inlineFacts = document.Facts;
            List<Context> contexts = document.Contexts;
            List<Unit> units = document.Units;
            // ExEnd:1

            Console.WriteLine("ReadIxbrlDocument executed successfully.");
        }
    }
}
