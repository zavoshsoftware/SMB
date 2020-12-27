using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace SMB.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("")]
        public ActionResult Index()
        {
            HomeViewModel result=new HomeViewModel()
            {
                Sliders = db.Sliders.Where(c=>c.IsDeleted==false&&c.IsActive).OrderBy(c=>c.Order).ToList(),
                Section2Master = GetSingleTextItem("section2master"),
                Section2Details = GetTextItemList("section2items"),
                Section3Features = GetTextItemList("section3features").OrderBy(c=>c.CreationDate).ToList(),
                Section3SkillMaster = GetSingleTextItem("section3skillmaster"),
                Section3SkillDetails = GetTextItemList("section3skillitems"),
                Testimonials = db.Testimonials.Where(c=>c.IsDeleted==false&&c.IsActive).Take(5).ToList(),
                Teams = db.Team.Where(c => c.IsDeleted == false && c.IsActive).Take(6).ToList(),
                Blogs = db.Blogs.Where(c => c.IsDeleted == false && c.IsActive).OrderByDescending(c=>c.CreationDate).Take(6).ToList(),
            };
            return View(result);
        }

        public TextItem GetSingleTextItem(string name)
        {
            return db.TextItems.FirstOrDefault(c => c.Name == name);
        }
        public List<TextItem> GetTextItemList(string typeName)
        {
            return db.TextItems.Where(c => c.TextItemType.Name == typeName).ToList();
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult BlogList()
        {
            return View();
        }
        public ActionResult BlogDetail()
        {
            return View();
        }
        [Route("about")]
        public ActionResult About()
        {
            AboutViewModel result = new AboutViewModel()
            {
                Clients = db.Clients.Where(c => c.IsDeleted == false && c.IsActive).OrderBy(c => c.Order).ToList(),
                Section2Master = GetSingleTextItem("section2master"),
                Section2Details = GetTextItemList("section2items"),
                Section3Features = GetTextItemList("section3features").OrderBy(c => c.CreationDate).ToList(),
                Section3SkillMaster = GetSingleTextItem("section3skillmaster"),
                Section3SkillDetails = GetTextItemList("section3skillitems"),
                Testimonials = db.Testimonials.Where(c => c.IsDeleted == false && c.IsActive).Take(5).ToList(),
                Teams = db.Team.Where(c => c.IsDeleted == false && c.IsActive).Take(6).ToList(),
            };
            return View(result);
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            ContactViewModel result=new ContactViewModel();
            return View(result);
        }
    }
}