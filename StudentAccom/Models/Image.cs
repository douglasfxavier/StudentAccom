using System.ComponentModel.DataAnnotations;

namespace StudentAccom.Models {
    public class Image {
        [Key]
        public int ImageID { set; get; }
        public byte[] ImageData { set; get; }
        public string MimeType { set; get; }
        public virtual Accommodation Accommodation { set; get; }
    }
}