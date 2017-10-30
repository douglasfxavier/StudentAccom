using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class Login {
        public User User;
        public string Login { set; get; }
        public string Password { set; get; }
    }
}