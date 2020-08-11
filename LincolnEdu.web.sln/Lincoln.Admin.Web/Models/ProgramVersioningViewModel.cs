using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Models
{
    public class ProgramVersioningViewModel
    {
        public int ProgramVersioningID { get; set; }
        public int DepartmentCode { get; set; }
        public int ProgramCode { get; set; }
        public string DepartmentName { get; set; }
        public string ProgramName { get; set; }
        public string Version { get; set; }
        public string PlaceHolder { get; set; }
        public string Credit { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Active { get; set; }
        public List<SelectListItem> ProgramList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public int? AcademicID { get; set; }
        public string AcademicName { get; set; }
        public List<SelectListItem> AcademicList { get; set; }
    }
}