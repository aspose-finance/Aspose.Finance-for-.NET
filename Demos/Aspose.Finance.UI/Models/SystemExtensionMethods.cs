using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace Aspose.Finance.UI.Models
{
    public static class SystemExtensionMethods
    {
        public static Route WithDataToken(this Route self, string key, object value)
        {
            if (self.DataTokens == null)
                self.DataTokens = new RouteValueDictionary();

            self.DataTokens.Add(key, value);
            return self;
        }

        public static bool IsNullOrEmpty(this IEnumerable value)
        {
            return value == null || value.Cast<object>().Count() == 0;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || value.Count() == 0;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static B GetValueOrDefault<T, B>(IDictionary<T, B> dict, T key, B defaultValue = default(B))
        {
            if (dict.TryGetValue(key, out B value))
                return value;
            return defaultValue;
        }
    }
}