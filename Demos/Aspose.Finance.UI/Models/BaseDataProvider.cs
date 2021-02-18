using Aspose.Finance.UI.Services;

namespace Aspose.Finance.UI.Models
{
    /// <summary>
    /// Base class to be used for all the database access and provider design, Uses and forces database calls
    /// </summary>
    public abstract class BaseDataProvider
    {
        protected static GeneratedPagesService _GeneratedPagesService = new GeneratedPagesService();
        protected static SitemapService _SitemapService = new SitemapService();
        protected static FileFormatService _FileFormatService = new FileFormatService();
    }
}