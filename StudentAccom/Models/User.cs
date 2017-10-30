using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class User {
        public int ID { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}