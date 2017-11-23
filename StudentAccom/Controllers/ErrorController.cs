using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAccom.Controllers
{
    public class ErrorController : Controller {

        public ActionResult FileUploadLimitExceeded() {
            return View();
        }

        public ActionResult BadRequest() {
            Response.StatusCode = 400;

            return View();
        }

        public ActionResult PageUnauthorized() {
            Response.StatusCode = 403;

            return View();
        }

        public ActionResult PageNotFound() {
            Response.StatusCode = 404;
            return View();
        }
        
        public ActionResult InternalServerError() {
            Response.StatusCode = 500;

            return View();
         }
    }
}