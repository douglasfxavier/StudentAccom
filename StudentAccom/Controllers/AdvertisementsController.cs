using StudentAccom.DAL;
using StudentAccom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StudentAccom.Controllers
{
    public class AdvertisementsController : Controller
    {
        private StudentAccomContext DBContext;

        // GET: Main
        [Route("Advertisements/Show")]
        [HttpGet]
        public ActionResult Show(){
            DBContext = new StudentAccomContext();
            var accomFilter = DBContext.AccommodationsDB.Where(a => a.Status == Status.Approved);
            IEnumerable<Accommodation> accomList = accomFilter.ToArray();

            return View(accomList);
        }


        [Authorize(Roles = "Admin, AccommodationOfficer, Landlord")]
        [Route("Advertisements/List")]
        [HttpGet]
        public ActionResult List() {
            DBContext = new StudentAccomContext();
            IEnumerable<Accommodation> accommodations;
            if (User.IsInRole("Landlord")) {
                var userId = User.Identity.GetUserId();
                //Filter by Landlord User based on its ID
                var accomFilter = DBContext.AccommodationsDB.Where(a => a.LandlordID.Equals(userId));
                accommodations = accomFilter.ToArray();
            } else {
                //All accommodation records
                accommodations = DBContext.AccommodationsDB.ToArray();
            }
            return View(accommodations);
        }


    }
}