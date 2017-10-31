using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAccom.Models
{
    public class Accomodation
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int ID { set; get; }
        [Required]
        public string Description { set; get; }
        public Byte[] Photo { set; get; }
        public string Comment { set; get; }
        public bool Approved { set; get; }
        public bool Rejected { set; get; }
    }
}