using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAccom.Models
{
    public class AccomReview {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int? AccommodationID { set; get; }
        public virtual Accommodation AccommodationRecord { set; get; }
        public string Comment { set; get; }
        public bool Approved { set; get; }
        public bool Rejected { set; get; }

    }
}