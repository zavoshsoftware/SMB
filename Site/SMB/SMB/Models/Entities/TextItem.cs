using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class TextItem : BaseEntity
    {
        [Display(Name="عنوان")]
        public string Title { get; set; }

        [NotMapped]
        [Display(Name = "عنوان")]
        [DataType(DataType.MultilineText)]
        public string TitleWithBr
        {
            get
            {
                return Title.Replace(System.Environment.NewLine, "<br />");
            }
        }
        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [NotMapped]
        public string SummeryWithBr
        {
            get
            {
                return Summery.Replace(System.Environment.NewLine, "<br />");
            }
        }


        public string Name { get; set; }

        [Display(Name="تصویر")]
        public string ImageUrl { get; set; }

        [Display(Name="متن کوتاه")]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }
     

        [Display(Name = "متن")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }


        [Display(Name="آدرس لینک")]
        public string LinkUrl { get; set; }
        [Display(Name="متن لینک")]
        public string LinkTitle { get; set; }

        public Guid? TextItemTypeId { get; set; }
        public virtual  TextItemType  TextItemType { get; set; }
    }
}