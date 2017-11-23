﻿using StudentAccom.DAL;
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
        private ApplicationDbContext IdentityContext;

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
        public ActionResult List(string search, string status) {
            DBContext = new StudentAccomContext();
            IdentityContext = new ApplicationDbContext();

            IEnumerable<Accommodation> accommodations = DBContext.AccommodationsDB;

            if (!String.IsNullOrEmpty(search)) {
                accommodations = accommodations.Where(a => a.Title.Contains(search) || a.Description.Contains(search) || a.Location.Contains(search));
                ViewBag.Search = search;
            }

            var enumStatus = accommodations.OrderBy(a => a.Status).Select(a => a.Status).Distinct();

            if (!String.IsNullOrEmpty(status)) {
                if (status.Equals("Approved")) {
                    accommodations = accommodations.Where(a => a.Status.Equals(Status.Approved));
                }else if (status.Equals("Rejected")){ 
                    accommodations = accommodations.Where(a => a.Status.Equals(Status.Rejected));
                }else if ((status.Equals("UnderReview"))) {
                    accommodations = accommodations.Where(a => a.Status.Equals(Status.UnderReview));
                }
            }
                        
            ViewBag.Status = new SelectList(enumStatus);

            if (User.IsInRole("Landlord")) {
                var userId = User.Identity.GetUserId();
                //Filter by Landlord User based on its ID
                accommodations = accommodations.Where(a => a.LandlordID.Equals(userId));
            } 

            //Get the name (or campany name) from users and create a map wich key is the ID. The aim is to avoid accecces to the password column
            var landlords = IdentityContext.Users;
            var map = new Dictionary<string, string>();

            foreach (var l in landlords) {
                if (l.Company != null && l.Company.Length > 0) {
                    map.Add(l.Id, l.Company);
                } else {
                    map.Add(l.Id, string.Format("{0} {1}", l.FirstName, l.LastName));
                }
                
            }
            ViewData.Add("Landlords", map);
            ViewData.Add("Accommodations", accommodations.ToArray());

            return View();
        }


    }
}