using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Newsletter : BaseEntity
    { 

        [MaxLength(300)]
        public string Email { get; set; }

        [Display(Name = "IP")]
        [MaxLength(50)]
        public string Ip { get; set; }
    }
}