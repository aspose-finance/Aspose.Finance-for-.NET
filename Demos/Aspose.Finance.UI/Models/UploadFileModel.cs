using System;

namespace Aspose.Finance.UI.Models
{
    public class UploadFileModel
    {
        public bool AcceptMultipleFiles { get; set; }
        public string FileDropKey { get; set; }
        public string AcceptedExtentions { get; set; }
        public FlexibleResources Resources { get; }
        public string UploadId { get; set; } = $"{Guid.NewGuid()}";

        public UploadFileModel(FlexibleResources resources)
        {
            this.Resources = resources;
        }
    }
}