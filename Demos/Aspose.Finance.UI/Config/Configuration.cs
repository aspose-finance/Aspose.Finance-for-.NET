using System.Configuration;

/// <summary>
/// Summary description for Configuration
/// </summary>
namespace Aspose.Finance.UI.Config
{
    public static class Configuration  
    {
        private static string _asposeReverseSearchApiURL = ConfigurationManager.AppSettings["AsposeReverseSearchAPIBasePath"];
        private static string _assetPath = ConfigurationManager.AppSettings["ASSETPATH"]; 
        private static string _gridWebCachePath = ConfigurationManager.AppSettings["GridWebCachePath"];
        private static string _gridWebFontFolderPath = ConfigurationManager.AppSettings["GridWebFontFolderPath"];
        private static string _appName = ConfigurationManager.AppSettings["AppName"];
        private static string _AsposeToolsAPIBasePath = ConfigurationManager.AppSettings["AsposeAppAPIBasePath"];
        private static int _smtpPort = int.Parse(ConfigurationManager.AppSettings["MailServerPort"]);
        private static string _smtpServer = ConfigurationManager.AppSettings["MailServer"];
        private static string _fromEmailAddress = ConfigurationManager.AppSettings["FromAddress"];
        private static string _mailServerUserId = ConfigurationManager.AppSettings["MailServerUserId"];
        private static string _mailServerUserPassword = ConfigurationManager.AppSettings["MailServerPassword"];
        private static int _mailServerTimeOut = int.Parse(ConfigurationManager.AppSettings["SmtpTimeOut"]);
        private static string _fileDownloadLink = ConfigurationManager.AppSettings["FileDownloadLink"];
        private static string _productsAsposeToolsURL = ConfigurationManager.AppSettings["ProductsAsposeAppURL"];
		private static string _fileViewLink = ConfigurationManager.AppSettings["FileViewLink"];

		//private static string _googleClientID = ConfigurationManager.AppSettings["GoogleClientID"];
		//private static string _googleAppID = ConfigurationManager.AppSettings["GoogleAppID"];
		//private static string _dropboxAppKey =  ConfigurationManager.AppSettings["DropboxAppKey"];
		private static string _productsAsposeAppAssetURL = ConfigurationManager.AppSettings["ProductsAsposeAppAssetURL"];
		
		private static string _resourceFileSessionName = ConfigurationManager.AppSettings["ResourceFileSessionName"];

		public static string OptimizationSEOUrl { get; set; } = ConfigurationManager.AppSettings["OptimizationSEOUrl"];
		public static string OptimizationSEOKey { get; set; } = ConfigurationManager.AppSettings["OptimizationSEOKey"];
		
		public static string ResourceFileSessionName
		{
			get { return _resourceFileSessionName; }
		}
		//public static string DropboxAppKey
		//{
		//	get { return _dropboxAppKey; }
		//}
		//public static string GoogleClientID
		//{
		//	get { return _googleClientID; }
		//}
		//public static string GoogleAppID
		//{
		//	get { return _googleAppID; }
		//}

		public static string AsposeReverseSearchApiURL
        {
            get { return _asposeReverseSearchApiURL; }
        }

        public static string ProductsAsposeToolsURL
        {
            get { return _productsAsposeToolsURL; }
        }
		public static string ProductsAsposeAppAssetsURL
		{
			get { return _productsAsposeAppAssetURL; }
		}		
		public static string FileDownloadLink
        {
            get { return _fileDownloadLink; }
        }
        public static int MailServerTimeOut
        {
            get { return _mailServerTimeOut; }            
        }
		public static string AssetPath
		{
			get { return _assetPath; }
		}

		public static string GridWebCachePath => _gridWebCachePath;
        public static string GridWebFontFolderPath => _gridWebFontFolderPath;

        public static string AppName
        {
            get { return _appName; }
        }
        public static string AsposeToolsAPIBasePath
        {
            get { return _AsposeToolsAPIBasePath; }
        }
        public static int MailServerPort
        {
            get { return _smtpPort; }            
        }
        public static string MailServer
        {
            get { return _smtpServer; }            
        }
        public static string FromEmailAddress
        {
            get { return _fromEmailAddress; }            
        }
        public static string MailServerUserId
        {
            get { return _mailServerUserId; }            
        }
        public static string MailServerUserPassword
        {
            get { return _mailServerUserPassword; }           
        }
		public static string FileViewLink
		{
			get { return _fileViewLink; }
		}
	}
}
