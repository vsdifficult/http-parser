using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HttpParser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Parse([FromQuery]string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("URL not be null");
            }

            try
            {
                var html = await GetHtmlAsync(url);
                var parsedData = ParseHtml(html);

                return Ok(parsedData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<string> GetHtmlAsync(string url)
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetStringAsync(url);
        }

        private ParsedData ParseHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var parsedData = new ParsedData
            {
                Headers = new List<string>(),
                Links = new List<string>()
            };

            // Извлечение заголовков
            foreach (var header in doc.DocumentNode.SelectNodes("//h1|//h2|//h3|//h4|//h5|//h6"))
            {
                parsedData.Headers.Add(header.InnerText.Trim());
            }

            // Извлечение ссылок
            foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                parsedData.Links.Add(link.GetAttributeValue("href", string.Empty));
            }

            return parsedData;
        }
    }
}
