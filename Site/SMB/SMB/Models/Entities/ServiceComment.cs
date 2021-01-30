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
    public class ServiceComment : BaseEntity
    {
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Response { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }

        internal class Configuration : EntityTypeConfiguration<ServiceComment>
        {
            public Configuration()
            {
                HasRequired(p => p.Service)
                    .WithMany(j => j.ServiceComments)
                    .HasForeignKey(p => p.ServiceId);
            }
        }
    }
}