using System.Collections.Generic;

namespace TechnicalDocuIndexer.Functions.Model
{
    class Video
    {
        public string webSearchUrl { get; set; }

        public string Name { get; set; } = "";
        public List<VideoEntity> value { get; set; }
    }
}
