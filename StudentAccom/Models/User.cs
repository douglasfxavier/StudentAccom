using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudentAccom.Models {
    public class User {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int? RoleID { get; set; }
        public virtual Role UserRole { get; set; }
    }
}