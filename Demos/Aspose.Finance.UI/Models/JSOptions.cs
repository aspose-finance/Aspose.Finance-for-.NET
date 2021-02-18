using System.Collections.Generic;
using Aspose.Finance.UI.Config;
using Newtonsoft.Json;

namespace Aspose.Finance.UI.Models
{
    public class JSOptions
    {
        private readonly ViewModel Parent;
        private FlexibleResources Resources => Parent.Resources;

        public string AppURL => Parent.AppURL;
        public string AppName => Parent.AppName;
        public string AppRoute => Parent.AppRoute;

        public string APIBasePath => $"{Configuration.AsposeToolsAPIBasePath}api/";
        public string UIBasePath => Configuration.ProductsAsposeToolsURL;

        public string ViewerPathWF => $"{UIBasePath}/{Parent.Product}/viewer/";

        public string ViewerPath => $"{UIBasePath}/{Parent.Product}/view?";
        public string EditorPath => $"{UIBasePath}/{Parent.Product}/edit?";

        public string FileSelectMessage => Resources["financeFileSelectMessage"];

        public bool ShowViewerButton => Parent.ShowViewerButton;
        public int MaximumUploadFiles => ViewModel.MaximumUploadFiles;

        public string FileAmountMessage => Resources["financeFileAmountMessageLessTen"];

        /// <summary>
        /// Apps like Viewer and Editor
        /// </summary>
        public bool UploadAndRedirect => Parent.UploadAndRedirect;

        public bool UseSorting => Parent.UseSorting;

        public string FileWrongTypeMessage { get; }

        public Dictionary<int, string> FileProcessingErrorCodes => new Dictionary<int, string>()
        {
            {(int) FileProcessingErrorCode.NoSearchResults, Resources["financeNoSearchResultsMessage"]},
            {(int) FileProcessingErrorCode.WrongRegExp, Resources["financeWrongRegExpMessage"]}
        };

        /// <summary>
        /// ['DOCX', 'DOC', ...]
        /// </summary>
        public IEnumerable<string> UploadOptions => Parent.ExtensionsString.Replace(".", "").ToUpper().Split('|');

        #region FileDrop

        public bool Multiple => !UploadAndRedirect;
        public string DropFilesPrompt => Resources["financeDropOrUploadFiles"];
        public string Accept => Parent.ExtensionsString.Replace("|.", ",.");
        public bool CanWorkWithoutFiles => Parent.CanWorkWithoutFiles;
        public bool DefaultFileBlockDisabled => Parent.DefaultFileBlockDisabled;

        #endregion

        public JSOptions(ViewModel model)
        {
            Parent = model;
            if (string.IsNullOrEmpty(model.Extension) || model.IsCanonical)
                FileWrongTypeMessage = Resources["financeFileWrongTypeMessage"];
            else
                FileWrongTypeMessage = string.Format(Resources["financeFileWrongTypeMessage2"], $"<a href=\"/{Parent.Product}/{AppName.ToLower()}\">{AppName}</a>");
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}