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
using ViewModels;

//using ViewModels;

namespace SMB.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServicesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Services.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups.Where(c=>c.IsDeleted==false), "Id", "Title");

            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase fileUpload, HttpPostedFileBase thumbFileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    service.ImageUrl = newFilenameUrl;
                }

                if (thumbFileUpload != null)
                {
                    string filename = Path.GetFileName(thumbFileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    thumbFileUpload.SaveAs(physicalFilename);

                    service.ThumbImageUrl = newFilenameUrl;
                }
                #endregion
                service.IsDeleted=false;
				service.CreationDate= DateTime.Now; 
                service.Id = Guid.NewGuid();
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups.Where(c => c.IsDeleted == false), "Id", "Title");

            return View(service);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups.Where(c => c.IsDeleted == false), "Id", "Title",service.ServiceGroupId);

            return View(service);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase fileUpload, HttpPostedFileBase thumbFileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    service.ImageUrl = newFilenameUrl;
                }

                if (thumbFileUpload != null)
                {
                    string filename = Path.GetFileName(thumbFileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    thumbFileUpload.SaveAs(physicalFilename);

                    service.ThumbImageUrl = newFilenameUrl;
                }
                #endregion
                service.IsDeleted = false;
				service.LastModifiedDate = DateTime.Now;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups.Where(c => c.IsDeleted == false), "Id", "Title", service.ServiceGroupId);

            return View(service);
        }

 
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Service service = db.Services.Find(id);
			service.IsDeleted=true;
			service.DeletionDate=DateTime.Now;
 
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
        [Route("service")]
        public ActionResult List()
        {
            ServiceListViewModel result = new ServiceListViewModel()
            {
              //  Services = db.Services.Where(c => c.IsDeleted == false && c.IsActive).OrderBy(c => c.Order).ToList()
            };
            return View(result);
        }

        [AllowAnonymous]
        [Route("service/{urlParam}")]
        public ActionResult Details(string urlParam)
        {
            Service service = db.Services.FirstOrDefault(c => c.UrlParam == urlParam && c.IsDeleted == false);
            if (service == null)
            {
                return RedirectToAction("List");
            }

            ServiceDetailViewModel result = new ServiceDetailViewModel()
            {
                Service = service,
                RelatedServices = db.Services.Where(c => c.IsDeleted == false && c.Id != service.Id && c.IsActive).OrderBy(c => c.Order).ToList(),
                ServiceComments = db.ServiceComments.Where(c=>c.ServiceId==service.Id&&c.IsActive&&c.IsDeleted==false).OrderByDescending(c=>c.CreationDate).ToList()
            };

            return View(result);
        }

        //public List<Blog> GetSidebarBlogBySericeId(Guid serviceId)
        //{
        //    List<ServiceBlog> serviceBlogs =
        //        db.ServiceBlogs.Where(c => c.ServiceId == serviceId && c.IsDeleted == false).Take(5).ToList();

        //    List<Blog> blogs=new List<Blog>();

        //    foreach (var serviceBlog in serviceBlogs)
        //    {
        //        blogs.Add(serviceBlog.Blog);
        //    }

        //    if (!blogs.Any())
        //        blogs = db.Blogs.Where(c => c.IsDeleted == false && c.IsActive).OrderByDescending(c => c.CreationDate)
        //            .Take(5).ToList();

        //    return blogs;
        //}
    }
}

