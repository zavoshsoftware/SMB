using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Models
{
    public class ServiceGroup:BaseEntity
    {
        public ServiceGroup()
        {
            Services=new List<Service>();
        }
        public string Title { get; set; }

        public string UrlParameter { get; set; }

        [Display(Name="Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [Required]
        public string Body { get; set; }


        public virtual ICollection<Service> Services { get; set; }
    }
}