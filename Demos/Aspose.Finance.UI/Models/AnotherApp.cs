namespace Aspose.Finance.UI.Models
{
    public class AnotherApp
    {
        public string ImageSource { get; set; }
        public string ImageAlt { get; set; }
        public string AppName { get; set; }
        public string Href { get; set; }

        public AnotherApp()
        {
            // Empty for inheritance
        }

        public AnotherApp(string appName)
        {
            AppName = appName;
            Href = $"/finance/{appName.ToLower()}";
            // ImageSource = $"https://products.aspose.app/img/apps/aspose_{appName.ToLower()}-app.png";
            ImageSource = $"/finance/img/apps/aspose_{appName.ToLower()}-app.png";
            ImageAlt = $"{AppName}";
        }
    }
}