using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Finance.UI.Models;

namespace Aspose.Finance.UI.Config
{
    public class BasePage : BaseRootPage
    {
        private string _product;

        /// <summary>
        /// Product name (e.g. words, cells)
        /// </summary>
        public string Product
        {
            get
            {
                if (string.IsNullOrEmpty(_product))
                {
                    if (Page.RouteData.Values["Product"] != null)
                    {
                        _product = Page.RouteData.Values["Product"].ToString().ToLower();
                    }

                    if (_product == null && Page.RouteData.DataTokens != null &&
                        Page.RouteData.DataTokens.ContainsKey("Product"))
                    {
                        _product = Page.RouteData.DataTokens["Product"].ToString().ToLower();
                    }

                    if (_product == null)
                    {
                        _product = Request.QueryString["Product"]?.ToLower();
                    }
                }

                return _product;
            }
            set => _product = value;
        }

        private string _feature;

        /// <summary>
        /// Product name (e.g. words, cells)
        /// </summary>
        public string Feature
        {
            get
            {
                if (_feature == null)
                    if (Page.RouteData.Values.ContainsKey("Feature"))
                        _feature = Page.RouteData.Values["Feature"].ToString().ToLower();
                    else
                        _feature = "";

                return _feature;
            }
            set => _feature = value;
        }


        public string _pageProductTitle;

        /// <summary>
        /// Product title (e.g. Aspose.Words)
        /// </summary>
        public string PageProductTitle
        {
            get
            {
                if (_pageProductTitle == null)
                    _pageProductTitle = Resources["Aspose" + TitleCase(Product)];
                return _pageProductTitle;
            }
        }

        private string _productH1 = "";

        public string ProductH1
        {
            get { return _productH1; }
        }

        private string _productH4 = "";

        public string ProductH4
        {
            get { return _productH4; }
        }

        private string _extension1 = "";

        public string Extension1
        {
            get { return _extension1; }
        }

        private string _appURLID = string.Empty;

        public string AppURLID
        {
            get { return _appURLID; }
        }

        private string _extension1Description = "";

        public string Extension1Description
        {
            get { return _extension1Description; }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Resources != null)
            {
                Page.Title = Resources["ApplicationTitle"];
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// Set validation for RegularExpressionValidators, InputFile and ViewStates using Resources[Product + "ValidationExpression"]
        /// </summary>
        private void SetAccept(string validationExpression, params HtmlInputFile[] inpufiles)
        {
            var accept = validationExpression.ToLower().Replace("|", ",");
            foreach (var inpufile in inpufiles)
                inpufile.Attributes.Add("accept", accept.ToLower().Replace("|", ","));
        }

        /// <summary>
        /// Set validation for RegularExpressionValidators and ViewStates using Resources[Product + "ValidationExpression"].
        /// If the 'ControlToValidate' option is HtmlInputFile, it sets accept an attribute to that control
        /// </summary>
        /// <param name="validators"></param>
        protected void SetValidation(params RegularExpressionValidator[] validators)
        {
            var validationExpression = "";
            var validFileExtensions = "";
            // Check for format like .Doc
            if (Page.RouteData.Values["Format"] != null)
            {
                validFileExtensions = Page.RouteData.Values["Format"].ToString().ToUpper();
                validationExpression = "." + validFileExtensions.ToLower();
            }
            else
            {
                validationExpression = Resources[Product + "ValidationExpression"];
                validFileExtensions = GetValidFileExtensions(validationExpression);
            }

            SetValidation(validationExpression, validators);

            ViewState["product"] = Product;
            ViewState["validFileExtensions"] = validFileExtensions;
        }

        protected void SetValidationExpression(string validationExpression, params RegularExpressionValidator[] validators)
        {
            var validFileExtensions = GetValidFileExtensions(validationExpression);
            SetValidation(validationExpression, validators);

            ViewState["product"] = Product;
            ViewState["validFileExtensions"] = validFileExtensions;
        }

        /// <summary>
        /// Set validation for RegularExpressionValidators, InputFile and ViewStates using validationExpression
        /// </summary>
        protected string SetValidation(string validationExpression, params RegularExpressionValidator[] validators)
        {
            // Check for format if format is available then valid expression will be only format for auto generated URLs
            if (Page.RouteData.Values["Format"] != null)
            {
                validationExpression = "." + Page.RouteData.Values["Format"].ToString().ToLower();
            }

            var validFileExtensions = GetValidFileExtensions(validationExpression);

            foreach (var v in validators)
            {
                v.ValidationExpression =
                    @"(.*?)(" + validationExpression.ToLower() + "|" + validationExpression.ToUpper() + ")$";
                v.ErrorMessage = Resources["financeInvalidFileExtension"] + " " + validFileExtensions;
                if (!string.IsNullOrEmpty(v.ControlToValidate))
                {
                    var control = v.FindControl(v.ControlToValidate);
                    if (control is HtmlInputFile inpufile)
                        SetAccept(validationExpression, inpufile);
                }
            }

            return validFileExtensions;
        }

        /// <summary>
        /// Get the text of valid file extensions
        /// e.g. DOC, DOCX, DOT, DOTX, RTF or ODT
        /// </summary>
        /// <param name="validationExpression"></param>
        /// <returns></returns>
        protected string GetValidFileExtensions(string validationExpression)
        {
            var validFileExtensions = validationExpression.Replace(".", "").Replace("|", ", ").ToUpper();

            var index = validFileExtensions.LastIndexOf(",");
            if (index != -1)
            {
                var substr = validFileExtensions.Substring(index);
                var str = substr.Replace(",", " or");
                validFileExtensions = validFileExtensions.Replace(substr, str);
            }

            return validFileExtensions;
        }

        protected Collection<FileUploadResult> UploadFilesToAPI(params HtmlInputFile[] fileInputs)
        {
            var directory = Path.Combine(Configuration.AssetPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(directory);
            var fileLocations = fileInputs.Select(x => SaveUploadedFile(directory, x)).ToArray();
            var isFileUploaded = FileManager.UploadFiles("", fileLocations);
            Directory.Delete(directory, true);
            return isFileUploaded;
        }

        /// <summary>
        /// Check for null and ContentLength of the PostedFile
        /// </summary>
        /// <param name="fileInputs"></param>
        /// <returns></returns>
        protected bool CheckFileInputs(params HtmlInputFile[] fileInputs)
        {
            return fileInputs.All(x => x != null && x.PostedFile.ContentLength > 0);
        }

        /// <summary>
        /// Save uploaded file to the directory
        /// </summary>
        /// <returns>SaveLocation with filename</returns>
        private string SaveUploadedFile(string directory, HtmlInputFile fileInput)
        {
            var fn = Path.GetFileName(fileInput.PostedFile.FileName); // Edge browser sents a full path for a filename
            var saveLocation = Path.Combine(directory, fn);
            fileInput.PostedFile.SaveAs(saveLocation);
            return saveLocation;
        }

        /// <summary>
        /// Check response for null and StatusCode. Call action if everything is fine or show error message if not
        /// </summary>
        /// <param name="response"></param>
        /// <param name="controlErrorMessage"></param>
        /// <param name="action"></param>
        protected void PerformResponse(Response response, HtmlGenericControl controlErrorMessage, Action<Response> action)
        {
            if (response == null)
                throw new Exception(Resources["financeAPIResponseTime"]);

            if (response.StatusCode == 200 && response.FileProcessingErrorCode == 0)
                action(response);
            else
                ShowErrorMessage(controlErrorMessage, response);
        }

        /// <summary>
        /// Check FileProcessingErrorCode of the response and show the error message
        /// </summary>
        /// <param name="control"></param>
        /// <param name="response"></param>
        protected void ShowErrorMessage(HtmlGenericControl control, Response response)
        {
            string txt;
            switch (response.FileProcessingErrorCode)
            {
                case FileProcessingErrorCode.NoImagesFound:
                    txt = Resources["financeNoImagesFoundMessage"];
                    break;
                case FileProcessingErrorCode.NoSearchResults:
                    txt = Resources["financeNoSearchResultsMessage"];
                    break;
                case FileProcessingErrorCode.WrongRegExp:
                    txt = Resources["financeWrongRegExpMessage"];
                    break;
                default:
                    txt = response.Status;
                    break;
            }

            ShowErrorMessage(control, txt);
        }

        protected void SetFormatInformations(string format, HtmlControl dvAppProductSection, HtmlControl dvHowToSection,
            HtmlControl dvExtensionDescription)
        {
            if (Page.RouteData.Values[format] != null)
            {
                // Populate contents from database based on URL
                string _url = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
                if (dvAppProductSection != null)
                {
                    dvAppProductSection.Visible = false;
                }

                GeneratedPage _generatedPage = (new GeneratedPage(_url)).GetByURL();
                if (_generatedPage != null)
                {
                    dvHowToSection.Visible = dvExtensionDescription.Visible = true;

                    _appURLID = _generatedPage.App_URL_ID;
                    if (_generatedPage.Extension1 != string.Empty)
                    {
                        _extension1 = _generatedPage.Extension1;
                        FileFormat _fileFormat = (new FileFormat(_extension1.ToLower())).GetByExtension();

                        _productH1 = string.Format(_generatedPage.MainHeadline, _extension1.ToUpper());
                        _productH4 = string.Format(_generatedPage.SubHeadline, _extension1.ToUpper());

                        ViewState["Extension1"] = _extension1.ToUpper();


                        if (_fileFormat != null)
                        {
                            ViewState["Extension1Name"] = _fileFormat.Name;
                            _extension1Description = _fileFormat.Description + "<br/><br/> <a target=\"_blank\" href=\"" +
                                                     _fileFormat.FileFormat_Com_URL + "\" class=\"btn btn-success btn-sm\">" +
                                                     "Read More" + " </a>";
                        }
                    }
                    else
                    {
                        Response.Redirect("~/errorpage");
                    }
                }
                else
                {
                    Response.Redirect("~/errorpage");
                }

                //Page.Title = hMainTitle.InnerText; // Resources["PerformOCR"];		
            }
        }

        protected void ShowErrorMessage(HtmlGenericControl control, string message)
        {
            if (message.ToLower().Contains("password"))
            {
                if ("pdf words cells slides note".Contains(Product.ToLower()) &&
                    !AppRelativeVirtualPath.ToLower().Contains("unlock"))
                {
                    string asposeUrl = "/" + Product + "/unlock";
                    message = "Your file seems to be encrypted. Please use our <a style=\"color:yellow\" href=\"" + asposeUrl +
                              "\">" + PageProductTitle + " Unlock</a> app to remove the password.";
                }
            }

            control.Visible = true;
            control.InnerHtml = message;
            control.Attributes.Add("class", "alert alert-danger");
        }

        protected void ShowSuccessMessage(HtmlGenericControl control, string message)
        {
            control.Visible = true;
            control.InnerHtml = message;
            control.Attributes.Add("class", "alert alert-success");
        }

        protected void CheckReturnFromViewer(Action<Response> action)
        {
            if (Request.QueryString["folderName"] != null && Request.QueryString["fileName"] != null)
            {
                var response = new Response
                {
                    FileName = Request.QueryString["fileName"],
                    FolderName = Request.QueryString["folderName"]
                };
                action(response);
            }
        }

        public void InflateOtherAppsPanel(HtmlGenericControl panel, string product, string currentAppName)
        {
            var builder = new StringBuilder();
            builder.Append(@"<div id=""otherapps-panel"">");


            foreach (var app in GetAppsList(product))
            {
                builder.AppendLine($@"<a href=""{app.Href}""
				   title=""{Resources[TitleCase(product) + app.AppName + "Desc"]}""
				   onclick=""otherAppClick('@app.AppName', true)""
				   class=""otherapps-panel__button {(currentAppName == app.AppName ? "otherapps-panel__button_current" : "")}""
				   style=""background-image: url('{app.ImageSource}')"">{app.AppName}</a>");
            }

            builder.AppendLine("</div>");

            panel.InnerHtml = builder.ToString();
        }

        private IEnumerable<AnotherApp> GetAppsList(string product)
        {
            if (product == "email")
            {
                yield return new AnotherApp("Conversion");
                yield return new AnotherApp("Metadata");
                yield return new AnotherApp("Headers");
                yield return new AnotherApp("Viewer");
                yield return new AnotherApp("Editor");
                yield return new AnotherApp("Search");
                yield return new AnotherApp("Assembly");
                yield return new AnotherApp("Parser");
                yield return new AnotherApp("Merger");
                yield return new AnotherApp("Watermark");
                yield return new AnotherApp("Redaction");
                yield return new AnotherApp("Signature");
                yield return new AnotherApp("Annotation");
                yield return new AnotherApp("Comparison");
            }
        }

        protected string TitleCase(string value)
        {
            return new CultureInfo("en-US", false).TextInfo.ToTitleCase(value);
        }
    }
}