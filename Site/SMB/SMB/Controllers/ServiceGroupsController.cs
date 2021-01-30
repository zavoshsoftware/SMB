using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace SMB.Controllers
{
    public class ServiceGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceGroups
        public ActionResult Index()
        {
            return View(db.ServiceGroups.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: ServiceGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceGroup serviceGroup = db.ServiceGroups.Find(id);
            if (serviceGroup == null)
            {
                return HttpNotFound();
            }
            return View(serviceGroup);
        }

        // GET: ServiceGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceGroup serviceGroup, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    serviceGroup.ImageUrl = newFilenameUrl;
                }
                #endregion
                serviceGroup.IsDeleted=false;
				serviceGroup.CreationDate= DateTime.Now; 
					
                serviceGroup.Id = Guid.NewGuid();
                db.ServiceGroups.Add(serviceGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceGroup);
        }

        // GET: ServiceGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceGroup serviceGroup = db.ServiceGroups.Find(id);
            if (serviceGroup == null)
            {
                return HttpNotFound();
            }
            return View(serviceGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceGroup serviceGroup, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    serviceGroup.ImageUrl = newFilenameUrl;
                }
                #endregion
                serviceGroup.IsDeleted=false;
					serviceGroup.LastModifiedDate=DateTime.Now;
                db.Entry(serviceGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceGroup);
        }

        // GET: ServiceGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceGroup serviceGroup = db.ServiceGroups.Find(id);
            if (serviceGroup == null)
            {
                return HttpNotFound();
            }
            return View(serviceGroup);
        }

        // POST: ServiceGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServiceGroup serviceGroup = db.ServiceGroups.Find(id);
			serviceGroup.IsDeleted=true;
			serviceGroup.DeletionDate=DateTime.Now;
 
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
    }
}
