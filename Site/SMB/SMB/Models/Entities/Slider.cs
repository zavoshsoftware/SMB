using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Slider : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Order { get; set; }

        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string ImageUrl { get; set; }

        [Display(Name = "عنوان")]
        [DataType(DataType.MultilineText)]
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
        public string Summery { get; set; }

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

        [Display(Name = "متن دکمه")]
        public string LinkTitle { get; set; }

        [Display(Name = "صفحه هدف دکمه")]
        public string LandingPage { get; set; }
    }
}