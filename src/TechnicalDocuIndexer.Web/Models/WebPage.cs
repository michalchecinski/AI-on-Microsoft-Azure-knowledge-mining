using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalDocuIndexer.Web.Models
{
    public class WebPage
    {
        public string webSearchUrl { get; set; }

        public string Name { get; set; } = "";
        public List<WebPageEntity> value { get; set; }
    }
}
