using Newtonsoft.Json;

namespace Aspose.Finance.UI.Models.SEO
{
    public class Service:SeoElement
    {
		[JsonProperty("@type")]
		public override string Type { get; set; } = "Service";

		[JsonProperty("aggregateRating")]
		public AggregateRating AggregateRating { get; set; } = new AggregateRating();

	}
}
