using Aspose.Finance.Xbrl;
using Aspose.Finance.Xbrl.Validator;
using System;

namespace CSharp.ValidateXbrlFiles
{
    class ValidateXBRLWithStardardErrorMessage
    {
        public static void Run()
        {
            // ExStart:1
            // Source directory
            string sourceDir = RunExamples.Get_SourceDirectory();

            XbrlDocument document = new XbrlDocument(sourceDir + @"IdScopeContextPeriodStartAfterEnd.xml");
            XbrlInstanceCollection xbrlInstances = document.XbrlInstances;
            XbrlInstance xbrlInstance = xbrlInstances[0];
            xbrlInstance.Validate();
            if (xbrlInstance.ValidationErrors.Count > 0)
            {
                foreach (ValidationError validationError in xbrlInstance.ValidationErrors)
                {
                    Console.WriteLine("Find validation error:" + validationError.Message);
                }
            }
            // ExEnd:1

            Console.WriteLine("ValidateXBRLWithStardardErrorMessage executed successfully.");
        }
    }
}
