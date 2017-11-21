using StudentAccom.DAL;
using StudentAccom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}