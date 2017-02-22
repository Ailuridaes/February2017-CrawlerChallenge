using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace CrawlerFunction {

    internal static class HelperFunctions {

        //--- Class Fields ---
        private static Regex aTagRegex = new Regex("(<a.*?>.*?</a>)", RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex hrefRegex = new Regex("href=\"(.*?)\"", RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex htmlTag = new Regex("<[^>]+>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex wordCountRegex = new Regex("(\\S+)", RegexOptions.Singleline | RegexOptions.Compiled);

        //--- Class Methods ---
        public static IEnumerable<string> FindLinks(string html, int maxLinks) {
            var list = new List<string>();
            var m1 = aTagRegex.Matches(html);
            foreach(Match m in m1) {
                var url = m.Groups[0].Value;
                var m2 = hrefRegex.Match(url);
                if (m2.Success) {
                    var href = m2.Groups[1].Value;
                    list.Add(href);
                }
            }
            return list.Take(maxLinks);
        }

        public static int CountWords(string html) {
            var stripped = htmlTag.Replace(html, string.Empty);
            return wordCountRegex.Matches(html).Count;
        }
    }
}