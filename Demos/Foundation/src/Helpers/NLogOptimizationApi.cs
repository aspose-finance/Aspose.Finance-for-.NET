using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tools.Foundation.Services;

namespace Tools.Foundation.Helpers
{
	[Target("OptimizationApi")]
	public sealed class NLogOptimizationApi : AsyncTaskTarget
	{
		static LogService _LogService = new LogService();
#if DEBUG
		internal static List<(DateTime sent, Models.DTO.LoggingApi.Log log, int? id, Exception ex)?> sentLogs = new List<(DateTime sent, Models.DTO.LoggingApi.Log log, int? id, Exception ex)?>();
#endif

		public NLogOptimizationApi()
		{
			//this.IncludeEventProperties = true; // Include LogEvent Properties by default
			//this.Host = "localhost";
		}

		//[RequiredParameter]
		//public string Host { get; set; }

		protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken token)
		{
			//IDictionary<string, object> logProperties = this.GetAllProperties(logEvent);			
			string getPropertyOrNull(string key) =>
				logEvent.Properties.ContainsKey(key)
					? $"{logEvent.Properties[key]}"
					: null;

			var log = new Models.DTO.LoggingApi.Log();
			log.Level = $"{logEvent.Level}";
			log.Callsite = $"{logEvent.CallerClassName}.{logEvent.CallerMemberName}";
			log.Type = $"{logEvent.Exception?.GetType()}";
			log.Message = logEvent.Exception?.Message;
			log.Stacktrace = logEvent.Exception?.StackTrace;
			log.InnerException = logEvent.Exception?.InnerException?.ToString();
			log.AdditionalInfo = logEvent.Message;
			log.Product = getPropertyOrNull("product");
			log.Productfamily = getPropertyOrNull("productfamily");
			log.Filename = getPropertyOrNull("filename");
#if DEBUG
			int? id = null;
			Exception ex = null;
			try
			{
				id = await _LogService.SendLog(log);
			}
			catch (Exception e)
			{
				ex = e;
				throw;
			}
			finally
			{
				lock (sentLogs)
					sentLogs.Add((DateTime.UtcNow, log, id, ex));
			}
#else
			await _LogService.SendLog(log);
#endif
		}
	}
}
