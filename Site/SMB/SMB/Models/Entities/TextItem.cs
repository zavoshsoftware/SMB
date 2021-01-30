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
        [NotMapped]
        public string SummeryWithBr
        {
            get
            {
                return Summery.Replace(System.Environment.NewLine, "<br />");
            }
        }


        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }
     

        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }


        public string LinkUrl { get; set; }
        public string LinkTitle { get; set; }

        public Guid? TextItemTypeId { get; set; }
        public virtual  TextItemType  TextItemType { get; set; }
    }
}