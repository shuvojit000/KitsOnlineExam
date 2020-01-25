﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class ExaminationNameViewModel
    {
        public int ExaminationNameID { get; set; }
        public string ExaminationName { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EndDate { get; set; }
      
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}