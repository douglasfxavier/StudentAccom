using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class Accomodation {
        public int id { set; get; }
        public string description { set; get; }
        public Byte[] photo { set; get; }
        public string comment { set; get; }
        public bool approved { set; get; }
        public bool rejected { set; get; }
    }
}