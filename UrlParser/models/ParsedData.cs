using System.Collections.Generic;

namespace HttpParser
{
    public class ParsedData
    {
        public List<string> Headers { get; set; }
        public List<string> Links { get; set; }
    }
}