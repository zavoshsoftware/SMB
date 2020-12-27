using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Team : BaseEntity
    {
        public string FullName { get; set; }
        public string Post { get; set; }
        public string ImageUrl { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }
    }
}