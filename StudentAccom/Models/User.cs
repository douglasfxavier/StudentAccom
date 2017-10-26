using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class User {
        public int îd { get; set; }
        public string name { get; set; }
        public Role role { get; set; }
    }
}