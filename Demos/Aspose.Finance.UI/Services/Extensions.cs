using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aspose.Finance.UI.Services
{
	public static class StringExtensions
	{
		public static string TitleCase(this string value)
		{
			return new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(value);
		}

	}
}
