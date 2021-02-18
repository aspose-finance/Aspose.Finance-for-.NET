using System.Configuration;

namespace Aspose.Finance.API.Config
{
    /// <summary>
    /// Summary description for App Setting
    /// </summary>
    public static class AppSettings
    {
        private static string _filesBaseDirectory = ConfigurationManager.AppSettings["FilesBaseDirectory"];
        private static string _outputDirectory = ConfigurationManager.AppSettings["OutputDirectory"];
        private static string _workingDirectory = ConfigurationManager.AppSettings["WorkingDirectory"];
        private static string _languageResourceFilePath = ConfigurationManager.AppSettings["LanguageResourceFilePath"];
        private static string _fontFolderPath = ConfigurationManager.AppSettings["FontFolderPath"];
        private static string _forumCategoryId = ConfigurationManager.AppSettings["ForumCategoryId"].ToString();
        private static string _forumUrl = ConfigurationManager.AppSettings["ForumUrl"].ToString();
        private static string _forumKey = ConfigurationManager.AppSettings["ForumKey"].ToString();
        private static string _reportDirectory = ConfigurationManager.AppSettings["ReportDirectory"].ToString();
        public static string ForumCategoryId
        {
            get { return _forumCategoryId; }
        }

        public static string ForumUrl
        {
            get { return _forumUrl; }
        }

        public static string ForumKey
        {
            get { return _forumKey; }
        }

        /// <summary>
        /// Get Output Directory
        /// </summary>
        public static string OutputDirectory => _outputDirectory;

        /// <summary>
        /// Get Output Files Base Directory
        /// </summary>
        public static string FilesBaseDirectory => _filesBaseDirectory;

        /// <summary>
        /// Get Working Directory
        /// </summary>
        public static string WorkingDirectory => _workingDirectory;

        /// <summary>
        /// Get Language Resource File Path
        /// </summary>
        public static string LanguageResourceFilePath => _languageResourceFilePath;

        /// <summary>
        /// Get Font Folder Path
        /// </summary>
        public static string FontFolderPath => _fontFolderPath;

        /// <summary>
        /// Get Output Files Base Directory
        /// </summary>
        public static string ReportDirectory
        {
            get { return _reportDirectory; }
        }
    }
}