using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lincoln.Admin.Web.Models
{
    public class UpdateStatusViewModel
    {
        public string ID { get; set; }
        public string Table { get; set; }
        public string Active { get; set; }
        public int CreatedBy { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
    }
}