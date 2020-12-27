using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Client : BaseEntity
    {
        public string Order { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}