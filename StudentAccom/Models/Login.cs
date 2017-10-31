using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class Login {
        public User CurrentUser;
        public string UserLogin { set; get; }
        public string Password { set; get; }
    }
}