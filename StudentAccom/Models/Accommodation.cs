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
        [Required]
        public string Description { set; get; }
        [Required]
        public TypeAccom TypeAccom { set; get; }
        [Required]
        public string Location { set; get; }
        [Required]
        public float Price { set; get; }
        [Required]
        public TypeRent TypeRent { set; get; }
        public int RoomsNumber { set; get; }
        public float CleaningFee { set; get; }
        public float SecurityDeposit { set; get; }
        public Byte[] Photo { set; get; }

        //Relationship with the class/table User. It record the user Landlord who creates the Accommodation        //Relationship with the class/table User. It record the user Landlord who creates the Accommodation advertisement 

        public virtual User LandLord { set; get; }

        //Sequence of features available OR not (boolean properties)
        public bool Internet { set; get; }
        public bool Doorman { set; get; }
        public bool Wifi { set; get; }
        public bool Kitchen { set; get; }
        public bool IndividualBathroom { set; get; }
        public bool SharedBathroom { set; get; }
        public bool Heating { set; get; }
        public bool AirConditioning { set; get; }
        public bool CableTV { set; get; }
        public bool TV { set; get; }
        public bool WasherMachine { set; get; }
        public bool DryerMachine { set; get; }
        public bool Pool { set; get; }
        public bool Gym { set; get; }

        //Sequence of permitted OR not permitted items/behaviors
        public bool Smoking { set; get; }
        public bool Pets { set; get; }
        public bool Parties { set; get; }
        public bool CommercialActivities { set; get; }
    }

}