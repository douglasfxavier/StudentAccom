using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentAccom.Models;
using StudentAccom.DAL;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;


namespace StudentAccom.Controllers {

    //[Authorize(Roles = "Admin")]
    public class AccommodationController : Controller {
        private StudentAccomContext DBContext;
        private ApplicationDbContext IdentityContext;
        private DbSet<Accommodation> AccommodationsDB;
        private DbSet<Image> ImagesDB;
        


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
                DBContext = new StudentAccomContext();
                AccommodationsDB = DBContext.AccommodationsDB;
                ImagesDB = DBContext.ImagesDB;
                var userId = User.Identity.GetUserId();
                a.LandlordID = userId;
                a.Status = Status.UnderReview;
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

                DBContext.SaveChanges();

                //This statement is the redirect action, as part of PRG (Post-Redirect-Get)
                return RedirectToAction("Details/" + a.ID);
            } else {
                return View();
            }
        }



        //This method loads the Details page of a Accommodation related to a given ID
        [AllowAnonymous]
        [Route("Accommodation/Details/{id:int}")]
        public ActionResult Details(int? id) {

            //Add validation: it can be shown anonymously (seen by students) only if it was approved by the AccommodationOfficer
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DBContext = new StudentAccomContext();
            AccommodationsDB = DBContext.AccommodationsDB;
            Accommodation accom = AccommodationsDB.Find(id);       
          
            if (accom == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IdentityContext = new ApplicationDbContext();
            ApplicationUser landlord = IdentityContext.Users.Find(accom.LandlordID);

            ImagesDB = DBContext.ImagesDB;

            ViewData.Add("accom", accom);
            ViewData.Add("landlord", landlord);

            return View();
        }



        [Authorize(Roles = "Admin, Landlord")]
        [Route("Accommodation/Edit/{id:int}")]
        [HttpGet]
        //This method load the view with the form to create a new Accommodation advertisement
        public ActionResult Edit(int id) {
            DBContext = new StudentAccomContext();
            AccommodationsDB = DBContext.AccommodationsDB;
            Accommodation a = AccommodationsDB.Find(id);
            return View(a);
        }



        //This method sends the POST message to the server
        [Route("Home/Edit/{pid:int}")]
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int id, Accommodation a) {
            if (ModelState.IsValid) {

                DBContext = new StudentAccomContext();
                DBContext.Entry(a).State = EntityState.Modified;
                DBContext.SaveChanges();

                return RedirectToAction("Details/"+id);
            } else {
                //When validation fails, redisplay the form
                return View();
            }
        }



        [Authorize(Roles = "Admin, Landlord")]
        [HttpGet]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DBContext = new StudentAccomContext();
            AccommodationsDB = DBContext.AccommodationsDB;
            Accommodation a = AccommodationsDB.Find(id);

            return View(a);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id) {
            DBContext = new StudentAccomContext();
            AccommodationsDB = DBContext.AccommodationsDB;
            ImagesDB = DBContext.ImagesDB;
            Accommodation a = AccommodationsDB.Find(id);

            ImagesDB.RemoveRange(a.Images);
            var result = AccommodationsDB.Remove(a);
            DBContext.SaveChanges();

            if (result != null) {
                return View();
            }
            //Redirect to the list of accommodations from the Landlord logged in
            return RedirectToAction("");
        }



        //Method which execute approval or recjection of one accommodation by the officer
        [Route("Accommodation/Review")]
        [Authorize(Roles = "Admin, AccommodationOfficer")]
        [HttpPost]
        public ActionResult Review(String textAreaComment, String btnReview, String Accommodation_ID) {
            int id = int.Parse(Accommodation_ID);
            DBContext = new StudentAccomContext();
            Accommodation a = DBContext.AccommodationsDB.Find(id);
            a.Comment = textAreaComment;

            if (btnReview.Equals("Approve")) {
                a.Status = Status.Approved;
            } else if (btnReview.Equals("Reject")) {
                a.Status = Status.Rejected;
            }

            DBContext.Entry(a).State = EntityState.Modified;
            DBContext.SaveChanges();
           
            return View(a);
        }
    }

}