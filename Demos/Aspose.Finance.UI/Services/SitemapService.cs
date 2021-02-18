using Aspose.Finance.UI.Config;
using Aspose.Finance.UI.Models.DTO.SEOApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Aspose.Finance.UI.Services
{
	public class SitemapService : BaseAPICacheService
	{
		public async Task<List<SitemapUrl>> GetUrls(
			string baseUrl,
			string product
		)
		{
			using (var response = await _httpClient.GetAsync($"{Configuration.OptimizationSEOUrl}/Sitemap?baseUrl={baseUrl}&product={product}&AuthenticationToken={Configuration.OptimizationSEOKey}"))
			{
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsAsync<List<SitemapUrl>>();
			}
		}

		public List<SitemapUrl> GetCachedUrls(string baseUrl, string product)
			=> GetCached(_Urls, (baseUrl, product), async () => await GetUrls(baseUrl, product));

		static Dictionary<(string, string), List<SitemapUrl>> _Urls = new Dictionary<(string, string), List<SitemapUrl>>();

		public static void PurgeCache()
		{
			lock (_Urls) _Urls.Clear();
		}
	}
}
