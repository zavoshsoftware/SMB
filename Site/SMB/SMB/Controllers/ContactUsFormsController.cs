using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MarjanKarimi.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class ContactUsFormsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

    [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.ContactUsForms.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

    [Authorize(Roles = "Administrator")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

      
    [Authorize(Roles = "Administrator")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

       
    [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
			contactUsForm.IsDeleted=true;
			contactUsForm.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [AllowAnonymous]
        public ActionResult SubmitContactForm(string fullName, string email, string message)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isEmail)
            {
                ContactUsForm cf = new ContactUsForm();
                cf.Id = Guid.NewGuid();
                cf.Email = email;
                cf.IsDeleted = false;
                cf.Message = message;
                cf.Name = fullName;
                cf.CreationDate = DateTime.Now;
                cf.Ip = Request.UserHostAddress;

                db.ContactUsForms.Add(cf);
                db.SaveChanges();

                Helpers.Message oMessage = new Helpers.Message();
                string msg = CreateEmailMessage(fullName, email, message);
                oMessage.SendEmail("quote@smbcloudsolutions.com.au", "Form Submited in SMBCloudsolutions site", msg);

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("false", JsonRequestBehavior.AllowGet);

        }


        public string CreateEmailMessage(string fullName, string email, string message)
        {
           return @"<html>
                 <head></head>
                <body >
                <h1>Form Submited in SMBCloudsolutions site</h1>
                <p><b>fullname</b> " + fullName + "</p>"+
                "<p><b>email</b> " + email + "</p>"+
               " <p><b>message</b> " + message + "</p>"+
               " <p><b>Submit Date</b> " + DateTime.Now + "</p>"+
                @"
                <h3>https://smbcloudsolutions.com.au</h3>
                </body>
                </html> ";


        }
      
    }
}
