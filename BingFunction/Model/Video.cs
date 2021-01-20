using System;
using System.Collections.Generic;
using System.Text;

namespace BingSearch
{
    class Video
    {
        public string webSearchUrl { get; set; }

        public string Name { get; set; } = "";
        public List<VideoEntity> value { get; set; }
    }
}
