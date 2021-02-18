using System;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using NLog;

namespace Tools.Foundation.Models
{
    public enum ProductFamilyNameKeysEnum { words, pdf, cells, email, slides, imaging, barcode, diagram, tasks, ocr, note, cad, threeD, html, gis, zip, eps, xps, psd, omr, page, finance, unassigned };

    public  class  NLogger
    {
        protected static  NLog.Logger nLogger = NLog.LogManager.GetLogger("databaseLogger");

        public static void LogInfo(string info, string productName, ProductFamilyNameKeysEnum productFamily, string fileName)
        {
			LogEventInfo _info = new LogEventInfo(LogLevel.Info, "databaseLogger", info);
			_info.Properties["product"] = productName;
			_info.Properties["productfamily"] = productFamily;
			_info.Properties["filename"] = fileName;

			nLogger.Info(_info);
        }

        public static void LogError(Exception ex, string message = "Error")
        {
            nLogger.Error(message + "-[" + ex.Message + "]" + ex.StackTrace);
        }

        public static void LogError(Exception ex, string info, string productName, ProductFamilyNameKeysEnum productFamily, string fileName)
        {
			LogEventInfo _info = new LogEventInfo(LogLevel.Info, "databaseLogger", null, info, null, ex);
			
			_info.Properties["product"] = productName;
			_info.Properties["productfamily"] = productFamily;
			_info.Properties["filename"] = fileName;

			nLogger.Error(_info);
            //SendEmail("Aspose.app Error Notification", info + "<br/>" + ex.Message + "<br/>" + ex.StackTrace);
        }

        public static bool SendEmail(string subject, string body)
        {
			string smtpHost;
			try
			{
				smtpHost = ConfigurationManager.AppSettings["MailServer"];
			}
			catch
			{
				smtpHost = null;
			}

			if (smtpHost == null)
				return false;

            bool retVal = true;
            SmtpClient smtp = new SmtpClient();
            MailMessage message = new MailMessage();
            try
            {
                string fromEmailAddress = ConfigurationManager.AppSettings["FromAddress"];
                string[] toEmailAddress = ConfigurationManager.AppSettings["ToAddress"].Split(',');

                message.From = new MailAddress(fromEmailAddress);
                foreach (string emailId in toEmailAddress)
                {
                    message.To.Add(emailId);
                }

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                smtp.UseDefaultCredentials = false;
				smtp.Host = smtpHost;
				smtp.Port = Int32.Parse(ConfigurationManager.AppSettings["MailServerPort"]);
                smtp.Timeout = Int32.Parse(ConfigurationManager.AppSettings["SmtpTimeOut"]);

                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailServerUserId"], ConfigurationManager.AppSettings["MailServerPassword"]);
                if (message.To.Count > 0)
                {
                    smtp.Send(message);
                }

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                retVal = false;
            }
            finally
            {
                message.Dispose();
            }

            return retVal;
        }
    }
}
