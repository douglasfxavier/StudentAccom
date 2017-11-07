using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class Image {
        [Key]
        public virtual Accommodation Accommodation { set; get; }
        [Key]
        public int ImageID { set; get; }
        public byte[] ImageData { set; get; }
        public string MimeType { set; get; }
    }
}