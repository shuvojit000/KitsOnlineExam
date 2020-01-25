using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class AcademicViewModel
    {
        public int AcademicID { get; set; }
        public string AcademicCode { get; set; }
        [Required(ErrorMessage ="Academic level is required")]
        public string AcademicName { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}