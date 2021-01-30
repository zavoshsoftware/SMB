using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class BlogGroup : BaseEntity
    {
        [Required]
        public string Title { get; set; }
      
        [Required]
        public string Summery { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string UrlParam { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }

    }
}