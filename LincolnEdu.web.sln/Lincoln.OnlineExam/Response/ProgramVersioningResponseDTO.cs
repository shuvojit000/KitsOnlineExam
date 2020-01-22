using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Response
{
   public class ProgramVersioningResponseDTO
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
        public string Status { get; set; }
    }
}
