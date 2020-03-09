using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage ="Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public string UserType { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
    }
    public class imagesviewmodel
    {
        public string Url { get; set; }
    }
}