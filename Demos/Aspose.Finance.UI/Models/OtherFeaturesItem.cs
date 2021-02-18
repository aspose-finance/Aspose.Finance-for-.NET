namespace Aspose.Finance.UI.Models
{
    public class OtherFeaturesItem
    {
        /// <summary>
        /// Url for anchor
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// TitleSub
        /// </summary>
        public string TitleSub { get; set; }

        public OtherFeaturesItem(GeneratedPage page)
        {
            URL = page.URL;
            /*if (!string.IsNullOrEmpty(URL))
            {
                if (URL.StartsWith("/cells/"))
                {
                    int index = "/cells/".Length - 1;
                    URL = URL.Substring(index, URL.Length - index);
                }
            }*/
            Title = page.Name;
            TitleSub = $"({page.MainHeadline})";
        }
    }
}