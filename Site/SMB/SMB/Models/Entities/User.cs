
namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;

    public class User : BaseEntity
    {

        [StringLength(150)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string CellNum { get; set; }

        [Required]
        [StringLength(250)]
        public string FullName { get; set; }

        public int? Code { get; set; }

      

        public string AvatarImageUrl { get; set; }

        [StringLength(256)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        public Guid RoleId { get; set; }

      
        public virtual Role Role { get; set; }

    }
}

