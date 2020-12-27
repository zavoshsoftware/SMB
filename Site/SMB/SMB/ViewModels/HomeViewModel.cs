using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HomeViewModel:_BaseViewModel
    {
        public List<Slider> Sliders { get; set; }
        public TextItem Section2Master { get; set; }
        public List<TextItem> Section2Details { get; set; }
        public List<TextItem> Section3Features { get; set; }

        public TextItem Section3SkillMaster { get; set; }
        public List<TextItem> Section3SkillDetails { get; set; }
        public List<Team> Teams { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}