using Aspose.Finance.Xbrl;
using Aspose.Finance.Xbrl.Validator;
using System;
using System.Collections.Generic;

namespace CSharp.ValidateXbrlFiles
{
    class ValidateXbrlInstance
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
                List<ValidationError> validationErrors = xbrlInstance.ValidationErrors;
            }
            // ExEnd:1

            Console.WriteLine("ValidateXbrlInstance executed successfully.");
        }
    }
}
