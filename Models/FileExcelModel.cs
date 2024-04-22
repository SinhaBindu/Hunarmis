using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class FileExcelModel
    {
        public FileExcelModel() { Id = 0; } 
        public int Id { get; set; }
        [DisplayName("Date")]
        public DateTime? UploadDate { get; set; }
        [DisplayName("File Name")]
        public string FileName { get; set; }
        [DisplayName("File Upload")]
        public HttpPostedFileBase FileUpload { get; set; }
        [DisplayName("File Upload")]
        public string FilePath { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}