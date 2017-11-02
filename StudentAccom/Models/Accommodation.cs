using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAccom.Models
{
    public class Accommodation
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int ID { set; get; }
        [Required]
        public string Title { set; get; }
        public float Price { set; get; }
        public string Description { set; get; }
        public Byte[] Photo { set; get; }
        //How many rooms left in the house
        public int Availability { set; get; }
        // ForeignKey to User(Landlord)
        [HiddenInput(DisplayValue = false)]
        [ForeignKey("User")]
        public int? LandlordID { set; get; }
        public virtual User User { set; get; }
    }
}