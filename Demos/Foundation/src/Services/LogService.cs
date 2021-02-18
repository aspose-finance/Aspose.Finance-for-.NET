using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tools.Foundation.Helpers;
using Tools.Foundation.Models.DTO.LoggingApi;

namespace Tools.Foundation.Services
{
	public class LogService
	{
		public static string OptimizationLogsUrl { get; set; } = ConfigurationManager.AppSettings["OptimizationLogsUrl"];
		public static string OptimizationLogsKey { get; set; } = ConfigurationManager.AppSettings["OptimizationLogsKey"];

		static HttpClient _httpClient = new HttpClient();

		public async Task<int> SendLog(Log log)
		{
			using (var response = await _httpClient.PostAsync(
				$"{OptimizationLogsUrl}/Logs/?AuthenticationToken={OptimizationLogsKey}",
				new StringContent(log.ToJson(false), Encoding.UTF8, "application/json")
			))
			{
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsAsync<int>();
			}
		}
	}
}
