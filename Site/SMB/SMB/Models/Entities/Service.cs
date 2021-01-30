using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Service : BaseEntity
    {
        public Service()
        {
            ServiceComments = new List<ServiceComment>();
        }
        public string Title { get; set; }

        public string UrlParam { get; set; }

        public string ImageUrl { get; set; }


        public string ThumbImageUrl { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        public int? Order { get; set; }

        public Guid? ServiceGroupId { get; set; }
        public virtual ServiceGroup ServiceGroup { get; set; }
        public virtual ICollection<ServiceComment> ServiceComments { get; set; }

    }
}