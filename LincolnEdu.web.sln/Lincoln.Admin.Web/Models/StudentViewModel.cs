using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class StudentViewModel
    {
        public int StudentID { get; set; }
        public int LoginID { get; set; }
        public int BatchID { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<SelectListItem> BatchList { get; set; }
        public string Active { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
    }
}