using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Aspose.Finance.UI.Config;
using Tools.Foundation.Models;

namespace Aspose.Finance.UI
{
    public class Global : HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = ((HttpApplication) sender).Context.Error;
            NLogger.LogError(
                ex,
                $"ControllerName = {nameof(Global)}, MethodName = {nameof(Application_Error)}",
                "",
                ProductFamilyNameKeysEnum.unassigned,
                ""
            );
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // TODO: LocalizationModule.OnAcquireRequestState(sender, e);
        }

        private void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterCustomRoutes(RouteTable.Routes);

        }

        private void Session_Start(object sender, EventArgs e)
        {
            //Check URL to set language resource file
            var language = "EN";

            if (Request.Url.Host.StartsWith("zh."))
            {
                language = "ZH";
            }

            var sessionId = Configuration.ResourceFileSessionName + Request.Url.Host.Trim().Replace(".", "");

            SetResourceFile(language, sessionId);
        }

        private void SetResourceFile(string strLanguage, string sessionId)
        {
            if (Session["AsposeAppResources"] == null)
                Session["AsposeAppResources"] = new GlobalAppHelper(HttpContext.Current, sessionId, strLanguage);
        }

        private static void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "SEOSitemapRoute",
                "sitemaps/{product}.xml",
                new {controller = "SEO", action = "Sitemap"}
            );

            routes.MapRoute(
                "SEOFillResourcesRoute",
                "SEO/FillResources",
                new {controller = "SEO", action = "FillResources"}
            );
            routes.MapRoute(
                "SEOResetResourcesCacheRoute",
                "SEO/ResetResourcesCache",
                new {controller = "SEO", action = "ResetResourcesCache"}
            );

            routes.MapRoute(
                "CommonResourcesRoute",
                "common/resources",
                new {controller = "Common", action = "GetResources"}
            );

            routes.MapRoute(
                "CommonTestFail",
                "common/TestFail",
                new {controller = "Common", action = "TestFail"}
            );

            routes.MapRoute(
                "CommonSendUrlToEmailRoute",
                "common/SendUrlToEmail/{product}/{method}",
                new {controller = "Common", action = "SendUrlToEmail"}
            );
            routes.MapRoute(
                "FinanceConversionRoute",
                "finance/conversion/{fileformat}",
                new {controller = "Finance", action = "Conversion", fileformat = ""}
            );
            routes.MapRoute(
                "FinanceSendEmailRoute",
                "finance/sendemail",
                new {controller = "Finance", action = "SendEmail"}
            );
            routes.MapRoute(
                "FinanceSendFeedbackRoute",
                "finance/sendfeedback",
                new {controller = "Finance", action = "SendFeedback"}
            );
            

            RegisterEmailAppInternalRoutes(routes);
            RegisterEmailDefaultRoutes(routes);

            MapProductToolPageRoute(routes,
                "AsposeToolsTaskDownloaderRoute",
                "{Product}/downloader",
                "~/DownloaderApp/DownloaderTasksApp.aspx",
                "^tasks$"
            );

            routes.MapRoute(
                "BaseSendFeedbackRoute",
                "common/sendfeedback",
                new {controller = "Common", action = "SendFeedback"}
            );

            routes.MapPageRoute(
                "AsposeToolsProductsRoute",
                "{PageName}",
                "~/Default.aspx"
            );
        }

        private static void RegisterEmailAppInternalRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "AsposeToolsViewerEmail",
                "email/view",
                "~/ViewerApp/Default.aspx"
            );

            routes.MapPageRoute(
                "AsposeToolsEditorEmail",
                "email/edit",
                "~/Editor/Default.aspx"
            );
        }

        private static void RegisterEmailDefaultRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "AsposeAppEmailApps",
                "email/{action}/{extension}",
                new {controller = "Email", extension = UrlParameter.Optional}
            );
        }

        private static void MapProductToolPageRoute(RouteCollection routes, string routeName, string routeUrl, string physicalFile, string productRegex)
        {
            routes.MapPageRoute(
                routeName,
                routeUrl,
                physicalFile,
                false,
                null,
                new RouteValueDictionary
                {
                    {"Product", productRegex}
                }
            );
        }
    }
}