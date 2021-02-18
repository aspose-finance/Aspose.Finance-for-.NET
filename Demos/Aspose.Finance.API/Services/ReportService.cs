using Aspose.Finance.API.Config;
using Aspose.Finance.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Aspose.Finance.API.Services
{
    public class ReportService
    {
        public static ReportResult Submit(ReportModel model)
        {
            try
            {
                Task.Run(() => { UploadFile(model); });
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-FORUM-API-Key", AppSettings.ForumKey);
                var requestData = ForumTopic.GetTopic(model);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(requestData));
                //content.Headers.Add("X-FORUM-API-Key", AppSettings.ForumKey);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(AppSettings.ForumUrl, content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<dynamic>(jsonResult);
                    if (data.error != null)
                    {
                        return new ReportResult()
                        {
                            StatusCode = 500,
                            Status = data.error,
                        };
                    }
                    else if (data.url != null)
                    {
                        return new ReportResult()
                        {
                            StatusCode = 200,
                            ForumLink = data.url,
                        };
                    }
                    else
                    {
                        throw new AppException("forum api result error");
                    }

                }
                else
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<dynamic>(jsonResult);
                    if (data.error != null)
                    {
                        throw new AppException("forum bad gateway:" + data.error);
                    }
                    else
                    {
                        throw new AppException("forum bad gateway:" + data.error);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UploadFile(ReportModel model)
        {
            var sourcePath = Path.Combine(AppSettings.WorkingDirectory, model.ErrorFolderName);
            var reportPath = Path.Combine(AppSettings.ReportDirectory, model.ErrorFolderName);
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                }
                foreach (string file in Directory.GetFiles(sourcePath))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.CopyTo(Path.Combine(reportPath, fileInfo.Name));
                }
            }
        }
    }
    public class ForumRequest
    {
        public ForumTopic topic { get; set; }
    }

    public class CustomFields
    {
        public bool is_private { get; set; }
    }

    public class ForumTopic
    {
        public string title { get; set; }
        public int category_id { get; set; }
        public bool notification { get; set; }
        public ForumUser user { get; set; }
        public Forumposts[] posts { get; set; }
        public CustomFields custom_fields { get; set; }

        public static ForumRequest GetTopic(ReportModel model)
        {
            var user = new ForumUser()
            {
                username = model.Email.Replace("@", "_at_"),
                email = model.Email,
            };
            var topic = new ForumTopic()
            {
                title = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + model.ErrorAction.Replace(" ", "-"),
                category_id = Int32.Parse(AppSettings.ForumCategoryId),
                notification = true,
                posts = new Forumposts[]{ new Forumposts() {
                    user = user,
                    raw = $"SessionId->{model.ErrorFolderName}->{model.ErrorAction}->{model.ErrorMessage}"
                } },
                custom_fields = new CustomFields()
                {
                    is_private = model.Private,
                },
                user = user,
            };
            return new ForumRequest() { topic = topic };
        }
    }
    public class ForumUser
    {
        public string email { get; set; }
        public string username { get; set; }
    }

    public class Forumposts
    {
        public string raw { get; set; }
        public ForumUser user { get; set; }
    }

    public class ReportModel
    {
        public string Email { get; set; }
        public string ErrorFolderName { get; set; }
        public string ErrorAction { get; set; }
        public string ErrorMessage { get; set; }

        public bool Private { get; set; }
    }

    public class ReportResult
    {
        public int StatusCode { get; set; }
        public string ForumLink { get; set; }

        public string Status { get; set; }
    }
}