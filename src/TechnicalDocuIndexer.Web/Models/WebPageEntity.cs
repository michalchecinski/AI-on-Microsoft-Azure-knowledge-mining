using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalDocuIndexer.Web.Models
{
    public class WebPageEntity
    {
        public string snippet { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public List<WebPageEntity> deepLinks { get; set; }
    }
}
