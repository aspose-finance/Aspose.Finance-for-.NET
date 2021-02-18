using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aspose.Finance.UI.Config;
using Aspose.Finance.UI.Models;

namespace Aspose.Finance.UI.Controllers
{
    public class FinanceController : BaseController
    {
        public override string Product => "finance";
        private readonly string[] _feedbackEmails = {"william.shen@aspose.com"};

        internal readonly Dictionary<string, string[]> _supportedFormats = new Dictionary<string, string[]>
        {
            {"xbrl", new[] {"ixbrl","xlsx"}},
            {"ixbrl", new[] {"xbrl","xlsx"}},
            
        };


        public ActionResult Conversion()
        {
            var model = new ViewModel(this, "Conversion")
            {
                SaveAsComponent = true,
                SaveAsOriginal = false,
                ShowViewerButton = false
            };
            var isSupportedExt = IsSupportedConversionExtensions(model.Extension, model.Extension2);
            if (!isSupportedExt)
            {
                return Redirect("/finance/" + model.AppName.ToLower());
            }

            if (string.IsNullOrEmpty(model.Extension)) return View(model);

            var res = _supportedFormats[model.Extension.ToLower()].Select(x => x.ToUpper()).ToList();
            if (!string.IsNullOrEmpty(model.Extension2))
            {
                var index = res.FindIndex(x => x == model.Extension2.ToUpper());
                if (index >= 0)
                {
                    res.RemoveAt(index);
                    res.Insert(0, model.Extension2.ToUpper());
                }
            }

            model.SaveAsOptions = res.ToArray();

            return View(model);
        }


        #region API

        [HttpPost]
        public JsonResult SendEmail(string url, string title, string appName, string email)
        {
            try
            {
                if (IsValidEmail(email))
                {
                    Task.Factory.StartNew(() =>
                    {
                        var u = HttpUtility.UrlDecode(url);
                        var successMessage = Resources[Product + appName + "SuccessMessage"];
                        var emailBody = EmailManager.PopulateBody(title, u, successMessage);
                        EmailManager.SendEmail(email, Configuration.FromEmailAddress, Resources["EmailTitle"], emailBody, "");
                    });
                    Session["emailTo"] = email;
                    return Json(new {message = Resources[Product + appName + "SentEmailSuccessMessage"]});
                }

                HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return Json(new {message = Resources["ValidateEmailMessage"]});
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return Json(new {message = ex.Message});
            }
        }

        [HttpPost]
        public JsonResult SendFeedback(string text, string appName, int score = 0)
        {
            try
            {
                if (text.Length > 1000)
                {
                    HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    return Json(new {message = Resources["FeedbackLengthError"]});
                }

                var subject = $"aspose.app feedback {appName}";
                var body = score == 0
                    ? text
                    : $"Score: {score}{Environment.NewLine}{text}";
                Task.Factory.StartNew(() =>
                {
                    foreach (var email in _feedbackEmails)
                    {
                        try
                        {
                            EmailManager.SendEmail(email, Configuration.FromEmailAddress, subject, body, "");
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                });
                return Json(new {message = Resources["FeedbackSuccess"]});
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return Json(new {message = ex.Message});
            }
        }

        #endregion

        #region private methods

        private bool IsSupportedConversionExtensions(string extFrom, string extTo)
        {
            if (string.IsNullOrEmpty(extFrom))
            {
                return true;
            }

            extTo = string.IsNullOrEmpty(extTo) ? "" : extTo.ToLower();

            var isValExist = _supportedFormats.TryGetValue(extFrom.ToLower(), out var exToArray);
            if (!isValExist)
                return false;

            return string.IsNullOrEmpty(extTo) || exToArray.Contains(extTo);
        }

        #endregion
    }
}