using System.Web.Optimization;
using System.Web.UI;

namespace Aspose.Finance.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                "~/finance/scripts/WebForms/WebForms.js",
                "~/finance/scripts/WebForms/WebUIValidation.js",
                "~/finance/scripts/WebForms/MenuStandards.js",
                "~/finance/scripts/WebForms/Focus.js",
                "~/finance/scripts/WebForms/GridView.js",
                "~/finance/scripts/WebForms/DetailsView.js",
                "~/finance/scripts/WebForms/TreeView.js",
                "~/finance/scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/finance/scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/finance/scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/finance/scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/finance/scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/finance/scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include(
                    //"~/Scripts/jquery-{version}.js",
                    "~/finance/scripts/jquery.form.min.js",
                    "~/finance/scripts/jquery.unobtrusive-ajax.min.js"
                )
            );

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/finance/scripts/respond.min.js",
                    DebugPath = "~/finance/scripts/respond.js",
                });


            bundles.Add(
                new ScriptBundle("~/bundles/AsposeShared")
                    .Include(
                        "~/finance/scripts/Shared/SEO.js",
                        "~/finance/scripts/Shared/Alert.js",
                        "~/finance/scripts/Shared/Resources.js",
                        "~/finance/scripts/Shared/DownloadResult.js",
                        "~/finance/scripts/Shared/Loader.js",
                        "~/finance/scripts/Shared/Metadata.js",
                        "~/finance/scripts/Shared/UploadFile.js",
                        "~/finance/scripts/Shared/MultiFileUploader.js",
                        "~/finance/scripts/Shared/Work.js"
                    )
            );

        }
    }
}