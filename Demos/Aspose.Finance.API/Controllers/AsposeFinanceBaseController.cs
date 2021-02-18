using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aspose.Finance.API.Config;
using Aspose.Finance.API.Models;
using Aspose.Finance.API.Services;
using Aspose.Finance.Xbrl;
using Aspose.Finance.Xbrl.Inline;
using Tools.Foundation.Models;
using license = Aspose.Finance.API.Models.License;

namespace Aspose.Finance.API.Controllers
{
    ///<Summary>
    /// Aspose Finance Base Class
    ///</Summary>
    public class AsposeFinanceBaseController : ApiControllerBase
    {
        /// <summary>
        /// Maximum number of files which can be uploaded for MVC Aspose.Words apps
        /// </summary>
        protected const int MaximumUploadFiles = 10;

        /// <summary>
        /// Original file format SaveAs option for multiple files uploading. By default, "-"
        /// </summary>
        private const string SaveAsOriginalName = ".-";

        /// <summary>
        /// Response when uploaded files exceed the limits
        /// </summary>
        protected readonly Response MaximumFileLimitsResponse = new Response
        {
            Status = $"Number of files should be less {MaximumUploadFiles}",
            StatusCode = 500
        };

        protected readonly Response FileParseErrorResponse = new Response
        {
            Status = "You document is not xbrl or ixbrl file.",
            StatusCode = 500
        };

        /// <summary>
        /// Response when uploaded files exceed the limits
        /// </summary>
        protected readonly Response PasswordProtectedResponse = new Response
        {
            Status = "Some of your documents are password protected",
            StatusCode = 500
        };

        /// <summary>
        /// 
        /// </summary>
        protected Response InternalServerErrorResponse(string FolderName, string Text)
        {
            return new Response
            {
                Status = "Internal server error",
                StatusCode = 500,
                FolderName = FolderName,
                Text = Text,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected Response AppErrorResponse(string msg, string FolderName, string Text)
        {
            return new Response
            {
                Status = msg,
                StatusCode = 500,
                FolderName = FolderName,
                Text = Text,
            };
        }


        ///<Summary>
        /// Aspose Finance Options Class
        ///</Summary>
        protected class Options
        {
            ///<Summary>
            /// AppName
            ///</Summary>
            public string AppName;

            ///<Summary>
            /// FolderName
            ///</Summary>
            public string FolderName;

            ///<Summary>
            /// FileName
            ///</Summary>
            public string FileName;

            private string _outputType;

            /// <summary>
            /// By default, it is the extension of FileName
            /// </summary>
            public string OutputType
            {
                get => _outputType;
                set
                {
                    if (!value.StartsWith("."))
                        value = "." + value;
                    _outputType = value;
                }
            }

            /// <summary>
            /// Check if OuputType is a picture extension
            /// </summary>
            public bool IsPicture
            {
                get
                {
                    switch (_outputType.ToLower())
                    {
                        case ".bmp":
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                        case ".emf":
                        case ".wmf":
                        case ".tiff":
                            return true;
                        default:
                            return false;
                    }
                }
            }

            ///<Summary>
            /// ResultFileName
            ///</Summary>
            public string ResultFileName;

            ///<Summary>
            /// MethodName
            ///</Summary>
            public string MethodName;

            ///<Summary>
            /// ControllerName
            ///</Summary>
            public string ControllerName;

            ///<Summary>
            /// CreateZip
            ///</Summary>
            public bool CreateZip;

            ///<Summary>
            /// CheckNumberOfPages
            ///</Summary>
            public bool CheckNumberOfPages = false;

            ///<Summary>
            /// DeleteSourceFolder
            ///</Summary>
            public bool DeleteSourceFolder = false;

            ///<Summary>
            /// CalculateZipFileName
            ///</Summary>
            public bool CalculateZipFileName = true;

            /// <summary>
            /// Output zip filename (without '.zip'), if CreateZip property is true
            /// By default, FileName + AppName
            /// </summary>
            public string ZipFileName;

            /// <summary>
            /// AppSettings.WorkingDirectory + FolderName + "/" + FileName
            /// </summary>
            public string WorkingFileName
            {
                get
                {
                    if (File.Exists(AppSettings.WorkingDirectory + FolderName + "/" + FileName))
                        return AppSettings.WorkingDirectory + FolderName + "/" + FileName;
                    return AppSettings.OutputDirectory + FolderName + "/" + FileName;
                }
            }
        }

        /// <summary>
        /// initialize Options
        /// </summary>
        protected Options Opts = new Options();

        /// <summary>
        /// UTF8WithoutBom
        /// </summary>
        protected static readonly Encoding UTF8WithoutBom = new UTF8Encoding(false);

        /// <summary>
        /// ProductFamily
        /// </summary>
        public ProductFamilyNameKeysEnum ProductFamily => ProductFamilyNameKeysEnum.finance;

        /// <summary>
        /// initialize AsposeFinanceBaseController
        /// </summary>
        public AsposeFinanceBaseController()
        {
            license.SetFinanceLicense();
        }

        /// <summary>
        /// Prepare upload files and return as documents
        /// </summary>
        protected async Task<DocumentInfo[]> UploadWorkBooks()
        {
            try
            {
                var folderName = Guid.NewGuid().ToString();
                var pathProcessor = new PathProcessor(folderName);
                var uploadProvider = new MultipartFormDataStreamProviderSafe(pathProcessor.SourceFolder);
                await Request.Content.ReadAsMultipartAsync(uploadProvider);
                return uploadProvider.FileData.Select(x =>
                {

                    FileFormat fileFormat = this.GetInputFileFormat(x.LocalFileName);
                    if(fileFormat == FileFormat.xbrl)
                    {
                        return new DocumentInfo
                        {
                            FolderName = folderName,
                            FileName = x.LocalFileName,
                            XbrlDocument = new XbrlDocument(x.LocalFileName),
                            FileFormat = fileFormat
                        };
                    }
                    else if(fileFormat == FileFormat.ixbrl)
                    {
                        return new DocumentInfo
                        {
                            FolderName = folderName,
                            FileName = x.LocalFileName,
                            InlineXbrlDocument = new InlineXbrlDocument(x.LocalFileName),
                            FileFormat = fileFormat
                        };
                    }
                    else if(fileFormat == FileFormat.xsd)
                    {
                        return new DocumentInfo
                        {
                            FolderName = folderName,
                            FileName = x.LocalFileName,
                            FileFormat = fileFormat
                        };
                    }
                    else if(fileFormat == FileFormat.linkbase)
                    {
                        return new DocumentInfo
                        {
                            FolderName = folderName,
                            FileName = x.LocalFileName,
                            FileFormat = fileFormat
                        };
                    }
                    else
                    {
                        return null;
                    }
                    
                }).ToArray();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new DocumentInfo[0];
            }
        }

        /// <summary>
        /// Set default parameters into Opts
        /// </summary>
        /// <param name="docs"></param>
        protected void SetDefaultOptions(DocumentInfo[] docs)
        {
            if (docs.Length <= 0) return;
            SetDefaultOptions(Path.GetFileName(docs[0].FileName));
            Opts.CreateZip = docs.Length > 1 || Opts.IsPicture;
        }

        /// <summary>
        /// Set default parameters into Opts
        /// </summary>
        /// <param name="filename"></param>
        private void SetDefaultOptions(string filename)
        {
            Opts.ResultFileName = filename;
            Opts.FileName = Path.GetFileName(filename);

            var query = Request.GetQueryNameValuePairs()
                .ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            string outputType = null;
            if (query.ContainsKey("outputType"))
                outputType = query["outputType"];
            Opts.OutputType = !string.IsNullOrEmpty(outputType)
                ? this.GetOutputType(outputType.ToLower())
                : Path.GetExtension(Opts.FileName).ToLower();

            Opts.ResultFileName = Opts.OutputType == SaveAsOriginalName
                ? Opts.FileName
                : Path.GetFileNameWithoutExtension(Opts.FileName) + Opts.OutputType;
        }

        /// <summary>
        /// GetSaveOptions
        /// </summary>
        /// <param name="outputType"></param>
        /// <returns></returns>
        protected SaveFormatType GetSaveOptions(string outputType)
        {
            var saveFormat = new SaveFormatType {};
            switch (outputType)
            {
                
                case "xbrl":
                {
                    saveFormat.SaveType = SaveType.xbrl;
                    break;
                }
                case "ixbrl":
                {
                    saveFormat.SaveType = SaveType.ixbrl;
                    break;
                }
                case "xlsx":
                    saveFormat.SaveType = SaveType.xlsx;
                    break;
                default:
                    saveFormat.SaveType = null;
                    break;
            }

            return saveFormat;
        }

        protected string GetOutputType(string outputType)
        {
            string ret = "xml";
            switch (outputType)
            {
                case "xbrl":
                    {
                        ret = "xml";
                        break;
                    }
                case "ixbrl":
                    {
                        ret = "html";
                        break;
                    }
                case "xlsx":
                    ret = "xlsx";
                    break;
            }
            return ret;
        }

        /// <summary>
        /// GetSaveFormatType by filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        protected SaveFormatType GetSaveFormatType(string filename)
        {
            string outputType = Path.GetExtension(filename);
            if (!string.IsNullOrEmpty(outputType))
            {
                if (outputType[0] == '.')
                {
                    outputType = outputType.Substring(1);
                }

                outputType = outputType.ToLower();
            }

            return GetSaveOptions(outputType);
        }


        /// <summary>
        /// Process
        /// </summary>
        protected Task<Response> Process(ActionDelegate action)
        {
            if (string.IsNullOrEmpty(Opts.OutputType))
                Opts.OutputType = Path.GetExtension(Opts.FileName);
            
            if (string.IsNullOrEmpty(Opts.ZipFileName) && Opts.CalculateZipFileName)
                Opts.ZipFileName = Path.GetFileNameWithoutExtension(Opts.FileName) + Opts.AppName;


            return Process(
                GetType().Name,
                Opts.ResultFileName,
                Opts.FolderName,
                Opts.OutputType,
                Opts.CreateZip,
                Opts.CheckNumberOfPages,
                AsposeFinance + Opts.AppName,
                ProductFamilyNameKeysEnum.finance,
                Opts.MethodName,
                action,
                Opts.DeleteSourceFolder,
                Opts.ZipFileName
            );
        }

        /// <summary>
        /// Check if the OutputType is a picture and saves the document
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="outPath"></param>
        /// <param name="zipOutFolder"></param>
        /// <param name="saveOptions"></param>
        protected void SaveDocument(DocumentInfo doc, string outPath, string zipOutFolder, SaveFormatType saveOptions)
        {
            string filename;
            if (Opts.CreateZip)
            {
                if (doc.FileFormat != FileFormat.xbrl && doc.FileFormat != FileFormat.ixbrl)
                {
                    filename = zipOutFolder + "/" + Path.GetFileName(doc.FileName);
                }
                else
                {
                    filename = zipOutFolder + "/" +
                           (Opts.OutputType == SaveAsOriginalName
                               ? Path.GetFileName(doc.FileName)
                               : Path.GetFileNameWithoutExtension(doc.FileName) + Opts.OutputType);
                }
            }
            else
                filename = outPath;


            SaveDocument(doc, filename, saveOptions);
        }

        /// <summary>
        /// Check if the OutputType is a picture and saves the document
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="filename">The output full FileName</param>
        /// <param name="saveOptions"></param>
        protected void SaveDocument(DocumentInfo doc, string filename, SaveFormatType saveOptions)
        {
            if (doc.FileFormat != FileFormat.xbrl && doc.FileFormat != FileFormat.ixbrl)
            {
                System.IO.File.Copy(doc.FileName, filename, true);
                return;
            }
            var zipOutFolder = $"{Path.GetDirectoryName(filename)}\\{Path.GetFileNameWithoutExtension(filename)}";

            //if (Opts.CreateZip)
            //    Directory.CreateDirectory(zipOutFolder);
            var outPath = zipOutFolder + Opts.OutputType;
            switch (saveOptions.SaveType)
            {
                case SaveType.xbrl:
                    if(doc.FileFormat == FileFormat.ixbrl)
                    {
                        doc.InlineXbrlDocument.ExportToXbrl(outPath);
                    }
                    else
                    {
                        System.IO.File.Copy(doc.FileName, outPath, true);
                    }
                    break;
                case SaveType.ixbrl:
                    if (doc.FileFormat == FileFormat.xbrl)
                    {
                        Aspose.Finance.Xbrl.SaveOptions saveOption = new SaveOptions();
                        saveOption.SaveFormat = SaveFormat.IXBRL;
                        doc.XbrlDocument.Save(outPath, saveOption);
                    }
                    else
                    {
                        System.IO.File.Copy(doc.FileName, outPath, true);
                    }
                    break;

                case SaveType.xlsx:
                    if (doc.FileFormat == FileFormat.ixbrl)
                    {
                        XbrlDocument xbrlDocument = doc.InlineXbrlDocument.ExportToXbrl();
                        Aspose.Finance.Xbrl.SaveOptions saveOption = new SaveOptions();
                        saveOption.SaveFormat = SaveFormat.XLSX;
                        xbrlDocument.Save(outPath, saveOption);
                    }
                    else if(doc.FileFormat == FileFormat.xbrl)
                    {
                        Aspose.Finance.Xbrl.SaveOptions saveOption = new SaveOptions();
                        saveOption.SaveFormat = SaveFormat.XLSX;
                        doc.XbrlDocument.Save(outPath, saveOption);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// LogError method to log errors
        /// </summary>
        protected void LogError(Exception ex)
        {
            var logMsg = "ControllerName = " + Opts.ControllerName + ", " + "MethodName = " + Opts.MethodName + ", " +
                         "Folder = " + Opts.FolderName;
            NLogger.LogError(ex, logMsg, AsposeFinance + Opts.AppName, ProductFamily, Opts.FileName);
        }

        /// <summary>
        /// SaveFormatType
        /// </summary>
        public class SaveFormatType
        {

            /// <summary>
            /// SaveType
            /// </summary>
            public SaveType? SaveType { get; set; }

        }

        public enum FileFormat
        {
            xbrl,
            ixbrl,
            xsd,
            linkbase,
            unknown
        }

        /// <summary>
        /// SaveType
        /// </summary>
        public enum SaveType
        {
            xbrl,
            ixbrl,
            xlsx
        }

        /// <summary>
        /// DocumentInfo
        /// </summary>
        public class DocumentInfo
        {
            /// <summary>
            /// FolderName
            /// </summary>
            public string FolderName { get; set; }

            /// <summary>
            /// FileName
            /// </summary>
            public string FileName { get; set; }

            /// <summary>
            /// XbrlDocument
            /// </summary>
            public XbrlDocument XbrlDocument { get; set; }

            public InlineXbrlDocument InlineXbrlDocument { get; set; }

            public FileFormat FileFormat { get; set; }
        }

        /// <summary>
        /// Prepare output folder for using when multiple files are uploaded
        /// Creates folder by filename without extension
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path">Zip folder name</param>
        /// <returns>Tuple(original filename, output folder)</returns>
        protected static (string, string) PrepareFolder(DocumentInfo doc, string path)
        {
            var filename = Path.GetFileNameWithoutExtension(doc.FileName);
            var folder = path + "/";
            folder += filename;
            while (Directory.Exists(folder))
                folder += "_";
            folder += "/";
            Directory.CreateDirectory(folder);
            return (Path.GetFileName(doc.FileName), folder);
        }

        public FileFormat GetInputFileFormat(string fileName)
        {
            FileFormat ret = FileFormat.unknown;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            foreach (XmlNode node in xmlDocument.ChildNodes)
            {
                if (node.LocalName.Equals("html"))
                {
                    ret = FileFormat.ixbrl;
                }
                else if (node.LocalName.Equals("xbrl"))
                {
                    ret = FileFormat.xbrl;
                }
                else if (node.LocalName.Equals("schema"))
                {
                    ret = FileFormat.xsd;
                }
                else if (node.LocalName.Equals("linkbase"))
                {
                    ret = FileFormat.linkbase;
                }
            }
            return ret;
        }

        public bool IsImage(string path)
        {
            try
            {
                Image.FromFile(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}