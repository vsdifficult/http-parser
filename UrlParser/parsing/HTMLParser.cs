using HtmlAgilityPack;
using System.Collections.Generic;

namespace HttpParser
{
    public class HtmlParser
    {
        public ParsedData Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var parsedData = new ParsedData
            {
                Headers = new List<string>(),
                Links = new List<string>()
            };

          
            foreach (var header in doc.DocumentNode.SelectNodes("//h1|//h2|//h3|//h4|//h5|//h6"))
            {
                parsedData.Headers.Add(header.InnerText.Trim());
            }

          
            foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                parsedData.Links.Add(link.GetAttributeValue("href", string.Empty));
            }

            return parsedData;
        }
    }
}
