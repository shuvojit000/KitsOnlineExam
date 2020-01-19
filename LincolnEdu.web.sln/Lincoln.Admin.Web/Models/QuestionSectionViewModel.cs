using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class QuestionSectionViewModel
    {
        public int QuestionSectionID { get; set; }
        public string QuestionSectionName { get; set; }
        public string QuestionSectionDesc { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Active { get; set; }
    }
}