using System.Collections.Generic;

namespace TechnicalDocuIndexer.Functions.Model
{
    class WebPage
    {
        public string webSearchUrl { get; set; }

        public string Name { get; set; } = "";
        public List<WebPageEntity> value { get; set; }
    }
}
