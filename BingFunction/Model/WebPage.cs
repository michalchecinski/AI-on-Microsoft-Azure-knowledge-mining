using System;
using System.Collections.Generic;
using System.Text;

namespace BingSearch
{
    class WebPage
    {
        public string webSearchUrl { get; set; }

        public string Name { get; set; } = "";
        public List<WebPageEntity> value { get; set; }
    }
}
