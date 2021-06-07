using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.Voter.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string MobileNo { get; set; }

        public string Otp { get; set; }
    }
}
