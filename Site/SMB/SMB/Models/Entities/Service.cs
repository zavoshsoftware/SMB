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
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "پارامتر صفحه")]
        public string UrlParam { get; set; }

        [Display(Name = "تصویر داخلی")]
        public string ImageUrl { get; set; }


        [Display(Name = "تصویر خارجی")]
        public string ThumbImageUrl { get; set; }
        [Display(Name = "خلاصه")]
        public string Summery { get; set; }
        [Display(Name = "متن صفحه")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }
        public virtual ICollection<ServiceComment> ServiceComments { get; set; }

    }
}