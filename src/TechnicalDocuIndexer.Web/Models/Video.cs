using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalDocuIndexer.Web.Models
{
    public class Video
    {
        public string webSearchUrl { get; set; }

        public string Name { get; set; } = "";
        public List<VideoEntity> value { get; set; }
    }
}
