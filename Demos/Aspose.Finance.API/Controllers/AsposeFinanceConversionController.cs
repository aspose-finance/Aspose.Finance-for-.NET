using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
// using Aspose.Finance.API.Config;
using Aspose.Finance.API.Models;
using Tools.Foundation.Models;

namespace Aspose.Finance.API.Controllers
{
    ///<Summary>
    /// AsposeFinanceConversionController class to convert cells files to different formats
    ///</Summary>
    public class AsposeFinanceConversionController : AsposeFinanceBaseController
    {
        [MimeMultipart]
        [HttpPost]
        [ActionName("Convert")]
        public async Task<Response> Convert(string outputType)
        {
            var sessionId = Guid.NewGuid().ToString();
            var action = $"Convert to {outputType.Trim().ToLower()}";
            try
            {
                var docs = await UploadWorkBooks();
                if (docs == null || docs.Length == 0)
                    return FileParseErrorResponse;
                bool hasValid = false;
                foreach (DocumentInfo documentInfo in docs)
                {
                    if (documentInfo != null)
                    {
                        if (documentInfo.FileFormat == FileFormat.xbrl || documentInfo.FileFormat == FileFormat.ixbrl)
                        {
                            hasValid = true;
                            break;
                        }
                    }
                }
                if (!hasValid)
                {
                    return FileParseErrorResponse;
                }
                if (docs.Length > MaximumUploadFiles)
                    return MaximumFileLimitsResponse;
                SetDefaultOptions(docs);
                Opts.AppName = ConversionApp;
                Opts.MethodName = "Convert";
                Opts.ZipFileName = docs.Length > 1 ? "Converted files" : Path.GetFileNameWithoutExtension(docs[0].FileName);
                //Opts.OutputType = this.GetOutputType(outputType.Trim().ToLower());


                var saveOpt = GetSaveOptions(outputType.Trim().ToLower());
                return await Process((inFilePath, outPath, zipOutFolder) =>
                {
                    var tasks = docs.Select(doc =>
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SaveDocument(doc, outPath, zipOutFolder, saveOpt);
                            }
                            catch (Exception e)
                            {
                            // WriteLog(doc.FileName, e.ToString());
                            Console.WriteLine(e);
                                throw;
                            }
                        })
                    ).ToArray();
                    Task.WaitAll(tasks);
                });
            }
            catch (AppException ex)
            {
                NLogger.LogError(ex, $"{sessionId}-{action}");
                return AppErrorResponse(ex.Message, sessionId, action);
            }
            catch (Exception ex)
            {
                NLogger.LogError(ex, $"{sessionId}-{action}");
                return InternalServerErrorResponse(sessionId, action);
            }
        }



        // todo https://issue.nanjing.dynabic.com/issues/CELLSAPP-31
        /*private static void WriteLog(string sourceFile, string exception)
        {
            var path = AppSettings.FilesBaseDirectory + "\\log\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var dateTime = DateTime.Now;
            var time = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var filename = $"{path}/{dateTime:yyyy-MM-dd}.log";

            using (var mySw = File.AppendText(filename))
            {
                var writeContent = $"{time} || {sourceFile}\r\n{exception}\r\n";
                mySw.WriteLine(writeContent);
                mySw.Close();
            }
        }*/
    }
}