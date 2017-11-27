using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;



namespace StudentAccom.Models {
    public class Accommodation {

        [HiddenInput(DisplayValue = false)]
        [Key]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Title { set; get; }

        [Required]
        [MaxLength(500)]
        public string Description { set; get; }
        [Required]

        [Display(Name = "Type of Accommodation")]
        [EnumDataType(typeof(TypeAccom))]
        public TypeAccom TypeAccom { set; get; }

        [Required]
        [MaxLength(100)]
        public string Location { set; get; }

        [Display(Name = "Number of Rooms")]
        [Required]
        [Range(1,int.MaxValue)]
        public int RoomsNumber { set; get; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { set; get; }

        [Display(Name = "Type of Rent")]
        [Required]
        public TypeRent TypeRent { set; get; }


        //Not required fields
        [Display(Name = "Cleaning Fee")]
        [Range(0.01, double.MaxValue)]
        public decimal CleaningFee { set; get; }

        [Display(Name = "Security Deposit")]
        [Range(0.01, double.MaxValue)]
        public decimal SecurityDeposit { set; get; }


        //Relationship with the class/table User. It record the user Landlord who creates the Accommodation        //Relationship with the class/table User. It record the user Landlord who creates the Accommodation advertisement 
        [HiddenInput(DisplayValue = false)]
        public string LandlordID { set; get; }


        //Sequence of features available OR not (boolean properties)
        public bool Internet { set; get; }

        [Display(Name = "Wi-Fi")]
        public bool Wifi { set; get; }

        [Display(Name = "Cable TV")]
        public bool CableTV { set; get; }

        [Display(Name = "TV")]
        public bool TV { set; get; }

        public bool Doorman { set; get; }

        public bool Kitchen { set; get; }

        [Display(Name = "Individual Bathroom")]
        public bool IndividualBathroom { set; get; }

        [Display(Name = "Shared Bathroom")]
        public bool SharedBathroom { set; get; }

        public bool Heating { set; get; }

        [Display(Name = "Air-Conditioning")]
        public bool AirConditioning { set; get; }

        [Display(Name = "Washing Machine")]
        public bool WashingMachine { set; get; }

        [Display(Name = "Drying Machine")]
        public bool DryingMachine { set; get; }

        public bool Pool { set; get; }

        public bool Gym { set; get; }

        //Sequence of permitted OR not permitted items/activities
        public bool Smoking { set; get; }

        public bool Pets { set; get; }

        public bool Parties { set; get; }

        [Display(Name = "Commercial Activities")]
        public bool CommercialActivities { set; get; }

        //Fields filed by the AccommodationOfficer
        public string Comment { set; get; }

        public Status Status { set; get; }

        //Collection of Images    
        public virtual ICollection<Image> Images { set; get; }
       
    }
}