﻿using System;
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
    [Authorize(Roles = "Administrator")]
    public class BlogCommentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: BlogComments
        public ActionResult Index()
        {
            var blogComments = db.BlogComments.Include(b => b.Blog).Where(b=>b.IsDeleted==false).OrderByDescending(b=>b.CreationDate);
            return View(blogComments.ToList());
        }

        // GET: BlogComments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // GET: BlogComments/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title");
            return View();
        }

        // POST: BlogComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Message,Response,BlogId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
				blogComment.IsDeleted=false;
				blogComment.CreationDate= DateTime.Now; 
                blogComment.Id = Guid.NewGuid();
                db.BlogComments.Add(blogComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title", blogComment.BlogId);
            return View(blogComment);
        }

        // GET: BlogComments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title", blogComment.BlogId);
            return View(blogComment);
        }

        // POST: BlogComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Message,Response,BlogId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
				blogComment.IsDeleted = false;
				blogComment.LastModifiedDate = DateTime.Now;
                db.Entry(blogComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Title", blogComment.BlogId);
            return View(blogComment);
        }

        // GET: BlogComments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // POST: BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BlogComment blogComment = db.BlogComments.Find(id);
			blogComment.IsDeleted=true;
			blogComment.DeletionDate=DateTime.Now;
 
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
        public ActionResult SubmitComments(string fullName, string email, string comment, string urlParam)
        {
            Blog blog = db.Blogs.FirstOrDefault(c => c.UrlParam == urlParam);

            if (blog == null)
                return Json("false", JsonRequestBehavior.AllowGet);

            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isEmail)
            {
                BlogComment cf = new BlogComment();
                cf.Id = Guid.NewGuid();
                cf.Email = email;
                cf.IsDeleted = false;
                cf.Message = comment;
                cf.Name = fullName;
                cf.CreationDate = DateTime.Now;
                cf.BlogId = blog.Id;

                db.BlogComments.Add(cf);
                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("false", JsonRequestBehavior.AllowGet);

        }
    }
}
