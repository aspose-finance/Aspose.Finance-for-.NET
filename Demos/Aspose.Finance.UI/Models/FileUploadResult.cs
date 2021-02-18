using System;
using System.Text;
using System.Web;
using Aspose.Finance.UI.Config;

namespace Aspose.Finance.UI.Models
{
    public class FileUploadResult
    {
        public string LocalFilePath { get; set; }
        public string FileName { get; set; }
        public string FolderId { get; set; }
        public long FileLength { get; set; }

        public string DownloadURL()
        {
            var url = new StringBuilder(Configuration.FileDownloadLink);
            url.Append("?FileName=");
            url.Append(HttpUtility.UrlPathEncode(FileName));
            url.Append("&Time=");
            url.Append(DateTime.Now);
            if (!string.IsNullOrEmpty(FolderId))
            {
                url.Append("&FolderName=");
                url.Append(FolderId);
            }
            return url.ToString();
        }
    }
}
