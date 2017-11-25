using StudentAccom.DAL;
using StudentAccom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;

namespace StudentAccom.Controllers
{
    public class AdvertisementsController : Controller
    {
        private StudentAccomContext DBContext;
        private ApplicationDbContext IdentityContext;


        //Home page, public for anonymous (not logged in) users
        [Route("Advertisements/Show")]
        [HttpGet]
        public ActionResult Show(string search, string status,  string sortby, int? page) {
            DBContext = new StudentAccomContext();
            AccomLandlord viewModel = new AccomLandlord();

            IEnumerable<Accommodation> accommodations = DBContext.AccommodationsDB;
            accommodations = accommodations.Where(a => a.Status.Equals(Status.Approved));

            if (!String.IsNullOrEmpty(search)) {
                accommodations = accommodations.Where(a => a.Title.ToLower().Contains(search.ToLower()) || 
                    a.Description.ToLower().Contains(search.ToLower()) || 
                    a.Location.ToLower().Contains(search.ToLower()));
                ViewBag.Search = search;
            }


            //sorting the results
            switch (sortby) {
                case "price_lowest":
                    accommodations = accommodations.OrderBy(a => a.Price);
                    break;
                case "price_highest":
                    accommodations = accommodations.OrderByDescending(a => a.Price);
                    break;
                default:
                    break;
            }

            //Paging the results
            const int pageItems = 9;
            int currentPage = (page ?? 1);
            viewModel.Accommodations = accommodations.ToPagedList(currentPage, pageItems);
            viewModel.Search = search;
            viewModel.SortBy = sortby;

            return View(viewModel);
        }


        

        //Private page for users logged in (Admin,Landlord or AccommodationOfficer
        [Authorize(Roles = "Admin, AccommodationOfficer, Landlord")]
        [Route("Advertisements/List")]
        [HttpGet]
        public ActionResult List(string search, string status, string sortby, int? page) {
            DBContext = new StudentAccomContext();
            AccomLandlord viewModel = new AccomLandlord();

            IEnumerable<Accommodation> accommodations =  DBContext.AccommodationsDB.ToArray();
            

            if (!String.IsNullOrEmpty(search)) {
                accommodations = accommodations.Where(a => a.Title.ToLower().Contains(search.ToLower()) || 
                    a.Description.ToLower().Contains(search.ToLower()) || 
                    a.Location.ToLower().Contains(search.ToLower()));
                ViewBag.Search = search;
            }


            //Filgering by advertisement status
            var enumStatus = accommodations.OrderBy(a => a.Status).Select(a => a.Status).Distinct();

            if (!String.IsNullOrEmpty(status)) {
                switch (status) {
                    case "UnderReview":
                        accommodations = accommodations.Where(a => a.Status.Equals(Status.UnderReview));
                        break;
                    case "Approved":
                        accommodations = accommodations.Where(a => a.Status.Equals(Status.Approved));
                        break;
                    case "Rejected":
                        accommodations = accommodations.Where(a => a.Status.Equals(Status.Rejected));
                        break;
                    default:
                        break;
                }
            }
                        
            ViewBag.Status = new SelectList(enumStatus);



            //sorting the results
            switch (sortby) {
                case "price_lowest":
                    accommodations = accommodations.OrderBy(a => a.Price);
                    break;
                case "price_highest":
                    accommodations = accommodations.OrderByDescending(a => a.Price);
                    break;
                default:
                    break;
            }


            //Avoinding Landlord users to have unauthorized access to the other landlords ads
            if (User.IsInRole("Landlord")) {
                var userId = User.Identity.GetUserId();                
                accommodations = accommodations.Where(a => a.LandlordID.Equals(userId));
            }



            //Getting the name (or campany name) from users and create a map wich key is the ID. The aim is to avoid accecces to the password column
            IdentityContext = new ApplicationDbContext();
            var landlords = IdentityContext.Users;
            var landlordsMap = new Dictionary<string, string>();

            foreach (var l in landlords) {
                if (l.Company != null && l.Company.Length > 0) {
                    landlordsMap.Add(l.Id, l.Company);
                } else {
                    landlordsMap.Add(l.Id, string.Format("{0} {1}", l.FirstName, l.LastName));
                }
                
            }


            //Paging the results
            const int pageItems = 10;
            int currentPage = (page ?? 1);
            viewModel.Accommodations = accommodations.ToPagedList(currentPage, pageItems);
            viewModel.Landlords = landlordsMap;
            viewModel.Status = status;
            viewModel.Search = search;
            viewModel.SortBy = sortby;

            return View(viewModel);
        }


    }
}