using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Testimonial : BaseEntity
    {
        public int Order { get; set; }
        public string FullName { get; set; }
        public string Post { get; set; }
        public string ImageUrl { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
       
    }
}