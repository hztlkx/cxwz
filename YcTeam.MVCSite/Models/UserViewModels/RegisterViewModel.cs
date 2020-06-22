using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.SysUserViewModels.UserViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 6)]
        [DataType(dataType: DataType.Password)]
        public string  Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string  Confirm { get; set; }
    }
}