using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Models
{
    public class FileModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte[] Data { get; set; }

        public FileModel(string id, string name, string fileType, string extension, string description, string uploadedBy, DateTime? createdOn, byte[] data)
        {
            Id = id;
            Name = name;
            FileType = fileType;
            Extension = extension;
            Description = description;
            UploadedBy = uploadedBy;
            CreatedOn = createdOn;
            Data = data;
        }
    }
}
