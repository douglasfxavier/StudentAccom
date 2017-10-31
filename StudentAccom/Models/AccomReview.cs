using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAccom.Models
{
    public class AccomReview{
        public int? AccommodationID { set; get; }
        public virtual Accommodation AccommodationRecord { set; get; }
        public string Comment { set; get; }
        public bool Approved { set; get; }
        public bool Rejected { set; get; }
    }
}