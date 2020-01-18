using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
    public class SubjectResponseDTO
    {
        public int SubjectID { get; set; }
        public int CourseID { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Status { get; set; }
      
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
