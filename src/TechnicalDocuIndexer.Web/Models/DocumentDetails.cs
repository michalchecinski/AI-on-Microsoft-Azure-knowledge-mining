using Azure.Search.Documents.Indexes;
using System.Collections.Generic;

namespace TechnicalDocuIndexer.Web.Models
{
    public class DocumentDetails
    {
        [SimpleField(IsKey = true)]
        public string metadata_storage_path { get; set; }
        public string metadata_storage_name { get; set; }
        
        public string StorageUrl { get; set; }
        
        public string metadata_storage_file_extension { get; set; }
        public List<string> keyphrases { get; set; }
        public List<string> organizations { get; set; }
        public List<string> text { get; set; }
        public List<string> foundServices { get; set; }
        public List<string> imageTags { get; set; }
        public List<string> layoutText { get; set; }
        public int wordCount { get; set; }
        public double timeToRead { get; set; }
        public string merged_content { get; set; }
        public string bing_entities { get; set; }
        public List<Entity> organizationsEntities { get; set; }
    }
}
