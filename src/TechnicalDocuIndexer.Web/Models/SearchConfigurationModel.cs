using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Models
{
    public class SearchConfigurationModel
    {
        public string IndexName { get; set; }
        public string QueryKey { get; set; }
        public string Service { get; set; }
    }
}
