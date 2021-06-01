using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hst.Model.ViewModels
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Please enter current password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "The password format is invalid.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password does not match with password")]
        [Required(ErrorMessage = "Please enter confrim password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "The password format is invalid.")]
        public string ConfirmPassword { get; set; }

        public int? UserId { get; set; }
        [Required(ErrorMessage = "Please enter user name")]
        public string UserName { get; set; }
        public string Code { get; set; }
        public string EmailTemplatePath { get; set; }
        public string Url { get; set; }
        public bool? IsCurrentPasswordValid { get; set; }
        public bool? Success { get; set; }
    }
}
