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

namespace SMB.Controllers
{
    public class ServiceCommentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceComments
        public ActionResult Index()
        {
            var serviceComments = db.ServiceComments.Include(s => s.Service).Where(s=>s.IsDeleted==false).OrderByDescending(s=>s.CreationDate);
            return View(serviceComments.ToList());
        }

        // GET: ServiceComments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceComment serviceComment = db.ServiceComments.Find(id);
            if (serviceComment == null)
            {
                return HttpNotFound();
            }
            return View(serviceComment);
        }

        // GET: ServiceComments/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title");
            return View();
        }

        // POST: ServiceComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Message,Response,ServiceId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] ServiceComment serviceComment)
        {
            if (ModelState.IsValid)
            {
				serviceComment.IsDeleted=false;
				serviceComment.CreationDate= DateTime.Now; 
					
                serviceComment.Id = Guid.NewGuid();
                db.ServiceComments.Add(serviceComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", serviceComment.ServiceId);
            return View(serviceComment);
        }

        // GET: ServiceComments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceComment serviceComment = db.ServiceComments.Find(id);
            if (serviceComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", serviceComment.ServiceId);
            return View(serviceComment);
        }

        // POST: ServiceComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Message,Response,ServiceId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] ServiceComment serviceComment)
        {
            if (ModelState.IsValid)
            {
				serviceComment.IsDeleted=false;
					serviceComment.LastModifiedDate=DateTime.Now;
                db.Entry(serviceComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", serviceComment.ServiceId);
            return View(serviceComment);
        }

        // GET: ServiceComments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceComment serviceComment = db.ServiceComments.Find(id);
            if (serviceComment == null)
            {
                return HttpNotFound();
            }
            return View(serviceComment);
        }

        // POST: ServiceComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServiceComment serviceComment = db.ServiceComments.Find(id);
			serviceComment.IsDeleted=true;
			serviceComment.DeletionDate=DateTime.Now;
 
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
        public ActionResult SubmitComments(string fullName, string email, string comment,string urlParam)
        {
            Service service = db.Services.FirstOrDefault(c => c.UrlParam == urlParam);
            if(service==null)
                return Json("false", JsonRequestBehavior.AllowGet);

            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isEmail)
            {
                ServiceComment cf = new ServiceComment();
                cf.Id = Guid.NewGuid();
                cf.Email = email;
                cf.IsDeleted = false;
                cf.Message = comment;
                cf.Name = fullName;
                cf.CreationDate = DateTime.Now;
                cf.ServiceId = service.Id;
               

                db.ServiceComments.Add(cf);
                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("false", JsonRequestBehavior.AllowGet);

        }
    }
}
