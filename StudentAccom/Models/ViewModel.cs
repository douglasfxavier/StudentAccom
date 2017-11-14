﻿//Code from the book ASP.NET MVC with Entity Framework and CSS, by Lee Naylor

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAccom.Models {
    public class ViewModel {
        public string Id { get; set; }
        [Required(AllowEmptyStrings=false)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}