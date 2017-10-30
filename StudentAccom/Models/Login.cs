using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class Login {
        public User user;
        public string login { set; get; }
        public string password { set; get; }
    }
}