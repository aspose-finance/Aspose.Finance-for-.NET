using Aspose.Finance;
using CSharp.Conversion;
using CSharp.CreateXbrlFiles;
using CSharp.ReadXbrlFiles;
using CSharp.ValidateXbrlFiles;
using CSharp.WorkingWithOfxFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class RunExamples
    {
        static void Main(string[] args)
        {
            // Set lisence
            //License financeLicense = new License();
            //financeLicense.SetLicense("LicenseFilePath");

            // Uncomment the one you want to try out

            //CreateXbrlFile.Run();
            //AddSchemaReferenceToXbrlDocument.Run();
            //AddContextToXbrlDocument.Run();
            //AddUnitToXbrlDocument.Run();
            //AddItemToXbrlDocument.Run();
            //AddFootnoteLinkToXbrlDocument.Run();
            //AddRoleReferenceToXbrlDocument.Run();
            //AddArcRoleReferenceToXbrlDocument.Run();
            //ReadXbrlDocument.Run();
            //ReadIxbrlDocument.Run();
            //ValidateXbrlInstance.Run();
            //ValidateIxbrlInstance.Run();
            //ValidateXBRLWithStardardErrorMessage.Run();
            //ValidateXBRLWithCustomizedErrorMessage.Run();

            //ConvertOfxRequestFileToOfxRequestV2.Run();
            //ConvertOfxResponseFileToOfxResponseV2.Run();
            //CreateOfxBankTransactionRequestFile.Run();
            //CreateOfxBankTransactionResponseFile.Run();

            //ConvertXbrlToXlsx.Run();
            //ConvertXbrlToIXbrl.Run();

            // Stop before exiting
            Console.WriteLine("\n\nProgram Finished. Press any key to exit....");
            Console.ReadKey();
        }

        private static string GetDataDir_Data()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            string startDirectory = null;
            if (parent != null)
            {
                var directoryInfo = parent.Parent;
                if (directoryInfo != null)
                {
                    startDirectory = directoryInfo.FullName;
                }
            }
            else
            {
                startDirectory = parent.FullName;
            }
            return startDirectory != null ? Path.Combine(startDirectory, "Data\\") : null;
        }

        internal static string Get_SourceDirectory()
        {
            return Path.GetFullPath(GetDataDir_Data() + "01_SourceDirectory/");
        }

        internal static string Get_OutputDirectory()
        {
            return Path.GetFullPath(GetDataDir_Data() + "02_OutputDirectory/");
        }
    }
}
