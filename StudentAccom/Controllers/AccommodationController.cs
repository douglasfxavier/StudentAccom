using System;
using System.Web;
using System.Web.Mvc;
using StudentAccom.Models;
using StudentAccom.DAL;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Net;
using System.IO;

namespace StudentAccom.Controllers {

    //[Authorize(Roles = "Admin")]
    public class AccommodationController : Controller {

        private StudentAccomContext DBContext;
        private ApplicationDbContext IdentityContext;



        [Authorize(Roles = "Admin, Landlord")]
        [Route("Accommodation/Create")]
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
                var userId = User.Identity.GetUserId();
                a.LandlordID = userId;
                a.Status = Status.UnderReview;
                DBContext.AccommodationsDB.Add(a);

                //Persistence of images into the database
                if (SelectedImages[0] != null ) {
                    foreach (var file in SelectedImages) {
                        Image img = new Image {
                            Accommodation = a,
                            ImageData = new byte[file.ContentLength],
                            MimeType = file.ContentType
                        };
                        file.InputStream.Read(img.ImageData, 0, img.ImageData.Length);
                        DBContext.ImagesDB.Add(img);
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

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DBContext = new StudentAccomContext();
            Accommodation accom = DBContext.AccommodationsDB.Find(id);

            //In case the accommodation record does not exist, so the system cannot find it by the ID
            if (accom == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //In case the accommodation exists, but the user is not logged in (possible customer/student)
            //The business rule says that students can see only the advertisments once approved

            if (!Request.IsAuthenticated && !accom.Status.Equals(Status.Approved)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }


            IdentityContext = new ApplicationDbContext();
            ApplicationUser landlord = IdentityContext.Users.Find(accom.LandlordID);

            ViewData.Add("currentUserId", User.Identity.GetUserId());
            ViewData.Add("accom", accom);
            var landlordFullName = string.Format("{0} {1}", landlord.FirstName, landlord.LastName);
            ViewData.Add("landlordFullName", landlordFullName);
            ViewData.Add("landlordCompany", landlord.Company);
            ViewData.Add("landlordEmail", landlord.Email);
            ViewData.Add("landolordPhoneNumber", landlord.PhoneNumber);

            return View();
        }



        [Authorize(Roles = "Admin, Landlord")]
        [Route("Accommodation/Edit/{id:int}")]
        [HttpGet]
        //This method loads the view with the form to edit an 
        //accommodation advertisement by a giver ID
        public ActionResult Edit(int id) {
            
            DBContext = new StudentAccomContext();
            Accommodation accom = DBContext.AccommodationsDB.Find(id);

            if (Request.IsAuthenticated && !accom.LandlordID.Equals(User.Identity.GetUserId()) 
                && !User.IsInRole("Admin")){
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(accom);
        }



        //This method sends the POST message with the changes in the given accommodation
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int id, Accommodation a, HttpPostedFileBase[] SelectedImages) {
            if (ModelState.IsValid) {

                DBContext = new StudentAccomContext();
                a.Status = Status.UnderReview;
                DBContext.Entry(a).State = EntityState.Modified;
                DBContext.SaveChanges();

                //If new images uploaded, remove old images and persist new images into the database
                if (SelectedImages[0] != null ) {
                    DBContext = new StudentAccomContext();
                    Accommodation persistedAccom = DBContext.AccommodationsDB.Find(id);
                    if (persistedAccom.Images.Count > 0) { 
                        DBContext.ImagesDB.RemoveRange(persistedAccom.Images);
                    }

                    foreach (var file in SelectedImages) {
                        Image img = new Image {
                            Accommodation = persistedAccom,
                            ImageData = new byte[file.ContentLength],
                            MimeType = file.ContentType
                        };
                        file.InputStream.Read(img.ImageData, 0, img.ImageData.Length);
                        DBContext.ImagesDB.Add(img);
                    }

                    DBContext.SaveChanges();
                }
                
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
            Accommodation accom = DBContext.AccommodationsDB.Find(id);

            if (Request.IsAuthenticated && !accom.LandlordID.Equals(User.Identity.GetUserId())
                && !User.IsInRole("Admin")) {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }


            return View(accom);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id) {
            DBContext = new StudentAccomContext();
            Accommodation a = DBContext.AccommodationsDB.Find(id);

            DBContext.ImagesDB.RemoveRange(a.Images);
            var result = DBContext.AccommodationsDB.Remove(a);
            DBContext.SaveChanges();

            if (result == null) {
                return View();
            }
            //Redirect to the list of accommodations from the Landlord logged in
            return RedirectToAction("../Advertisements/List");
        }



        //Method which execute approval or recjection of one accommodation by the officer
        [Authorize(Roles = "Admin, AccommodationOfficer")]
        [Route("Accommodation/Review")]
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

            return RedirectToAction("Details/" + id);
        }

    }

}