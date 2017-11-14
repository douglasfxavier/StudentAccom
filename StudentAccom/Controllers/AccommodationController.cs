﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentAccom.Models;
using StudentAccom.DAL;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Net;

namespace StudentAccom.Controllers {

    //[Authorize(Roles = "Admin")]
    public class AccommodationController : Controller {
        private StudentAccomContext Context;
        private DbSet<Accommodation> AccommodationsDB;
        private DbSet<Image> ImagesDB;
        //private Accommodation[] Accommodations;
        //private Image[] Images;

        [Authorize(Roles = "Admin, Landlord")]
        [HttpGet]
        //This method load the view with the form to create a new Accommodation advertisement
        public ViewResult Create() {
            return View();
        }

        [Authorize(Roles = "Admin, Landlord")]
        [Route("Accommodation/Create")]
        [HttpPost]
        //This method does the validation checks and post the values from and, when there's no erros, persist the data into the database
        public ActionResult Create(Accommodation a, HttpPostedFileBase[] SelectedImages) {
            if (ModelState.IsValid) {                
                Context = new StudentAccomContext();
                AccommodationsDB = Context.AccommodationsDB;
                ImagesDB = Context.ImagesDB;
                var userId = User.Identity.GetUserId();
                a.LandlordID = userId;
                AccommodationsDB.Add(a);

                //Persistence of images into the database
                if (SelectedImages != null) {
                    foreach (var file in SelectedImages) {
                        Image img = new Image {
                            Accommodation = a,
                            ImageData = new byte[file.ContentLength],
                            MimeType = file.ContentType
                        };
                        file.InputStream.Read(img.ImageData, 0, img.ImageData.Length);
                        ImagesDB.Add(img);

                    }

                }

                Context.SaveChanges();

                //This statement is the redirect action, as part of PRG (Post-Redirect-Get)
                return RedirectToAction("CreateSuccess", a);
            } else {
                return View();
            }
        }

        //This method loads the view with a successful message 
        [Authorize(Roles = "Admin, Landlord")]
        [Route("Accommodation/Success")]
        public ViewResult CreateSuccess(Accommodation a) {
            return View(a);
        }

        //This method loads the Details page of a Accommodation related to a given ID
        [AllowAnonymous]
        [Route("Accommodation/Details/{id:int}")]
        public ActionResult Details(int? id) {

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Context = new StudentAccomContext();
            AccommodationsDB = Context.AccommodationsDB;
            Accommodation accom = AccommodationsDB.Find(id);

            if (accom == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(accom);
        }

        [Authorize(Roles = "Admin, Landlord")]
        [Route("Accommodation/Edit/{id:int}")]
        [HttpGet]
        //This method load the view with the form to create a new Accommodation advertisement
        public ActionResult Edit(int ID) {
            Context = new StudentAccomContext();
            AccommodationsDB = Context.AccommodationsDB;
            Accommodation a = AccommodationsDB.Find(ID);
            return View(a);
        }
    }

}