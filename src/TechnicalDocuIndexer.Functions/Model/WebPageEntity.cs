using System.Collections.Generic;

namespace TechnicalDocuIndexer.Functions.Model
{
    class WebPageEntity
    {
        public string snippet { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public List<WebPageEntity> deepLinks { get; set; }
    }
}
