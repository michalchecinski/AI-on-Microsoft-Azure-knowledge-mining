using System;
using System.Collections.Generic;
using System.Text;

namespace BingSearch
{
    class Image
    {
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
        public Provider[] Provider { get; set; }
        public string HostPageUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
