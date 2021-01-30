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
        [Required]
        public int Order { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [NotMapped]
        [DataType(DataType.MultilineText)]
        public string TitleWithBr
        {
            get
            {
                return Title.Replace(System.Environment.NewLine, "<br />");
            }
        }

        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [DataType(DataType.MultilineText)]
        [NotMapped]
        public string SummeryWithBr
        {
            get
            {
                return Summery.Replace(System.Environment.NewLine, "<br />");
            }
        }

        public string LinkTitle { get; set; }

        public string LandingPage { get; set; }
    }
}