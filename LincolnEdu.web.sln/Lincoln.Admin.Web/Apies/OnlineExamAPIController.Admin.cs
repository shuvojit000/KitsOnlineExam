using Lincoln.OnlineExam.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Lincoln.Admin.Web.Apies
{
    public partial class OnlineExamAPIController
    {
        [Route("api/OnlineExamAPI/GetAllFacultyCourse")]
        [HttpGet]
        public HttpResponseMessage GetAllFacultyCourse(FacultyDashboardRequestDTO request)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllFacultyCourse(request));

        }
        [Route("api/OnlineExamAPI/ValidateUser")]
        [HttpGet]
        public HttpResponseMessage ValidateUser(LogInRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.ValidateUser(request, Operation));

        }
        [Route("api/OnlineExamAPI/UpdateStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateStatus(UpdateStatusReuestDTO request)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.UpdateStatus(request));

        }
        [Route("api/OnlineExamAPI/GetDropdownData")]
        [HttpGet]
        public HttpResponseMessage GetDropdownData(string type)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetDropdownData(type));

        }
        [Route("api/OnlineExamAPI/SaveCourse")]
        [HttpPost]
        public HttpResponseMessage SaveCourse(CourseRequestDTO request, string Operation)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveCourse(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllCourse")]
        [HttpGet]
        public HttpResponseMessage GetAllCourse()
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllCourse());

        }
        [Route("api/OnlineExamAPI/SelectCourse")]
        [HttpGet]
        public HttpResponseMessage SelectCourse(CourseRequestDTO request)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectCourse(request));

        }
        [Route("api/OnlineExamAPI/SaveStudent")]
        [HttpPost]
        public HttpResponseMessage SaveStudent(StudentRequestDTO request, string Operation)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveStudent(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllStudent")]
        [HttpGet]
        public HttpResponseMessage GetAllStudent()
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllStudent());

        }
        [Route("api/OnlineExamAPI/SelectStudent")]
        [HttpGet]
        public HttpResponseMessage SelectStudent(StudentRequestDTO request)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectStudent(request));

        }

        [Route("api/OnlineExamAPI/SaveAcademicLevel")]
        [HttpPost]
        public HttpResponseMessage SaveAcademicLevel(AcademicLevelRequestDTO request, string Operation)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveAcademicLevel(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllAcademicLevel")]
        [HttpGet]
        public HttpResponseMessage GetAllAcademicLevel()
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllAcademicLevel());

        }
        [Route("api/OnlineExamAPI/SelectAcademicLevel")]
        [HttpGet]
        public HttpResponseMessage SelectAcademicLevel(AcademicLevelRequestDTO request)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectAcademicLevel(request));

        }
        [Route("api/OnlineExamAPI/SaveDepartment")]
        [HttpPost]
        public HttpResponseMessage SaveDepartment(DepartmentRequestDTO request, string Operation)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveDepartment(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllDepartment")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartment()
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllDepartment());

        }
        [Route("api/OnlineExamAPI/SelectDepartment")]
        [HttpGet]
        public HttpResponseMessage SelectDepartment(DepartmentRequestDTO request)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectDepartment(request));

        }
        [Route("api/OnlineExamAPI/SaveProgramme")]
        [HttpPost]
        public HttpResponseMessage SaveProgramme(ProgrammeRequestDTO request, string Operation)
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveProgramme(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllProgramme")]
        [HttpGet]
        public HttpResponseMessage GetAllProgramme()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllProgramme());

        }
        [Route("api/OnlineExamAPI/GetAllProgrammeWithVersion")]
        [HttpGet]
        public HttpResponseMessage GetAllProgrammeWithVersion()
        {

            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllProgrammeWithVersion());

        }
        [Route("api/OnlineExamAPI/SelectProgramme")]
        [HttpGet]
        public HttpResponseMessage SelectProgramme(ProgrammeRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectProgramme(request));

        }
        [Route("api/OnlineExamAPI/SaveProgramVersioning")]
        [HttpPost]
        public HttpResponseMessage SaveProgramVersioning(ProgramVersioningRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveProgramVersioning(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllProgramVersioning")]
        [HttpGet]
        public HttpResponseMessage GetAllProgramVersioning()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllProgramVersioning());

        }
        [Route("api/OnlineExamAPI/SelectProgramVersioning")]
        [HttpGet]
        public HttpResponseMessage SelectProgramVersioning(ProgramVersioningRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectProgramVersioning(request));

        }
        [Route("api/OnlineExamAPI/SaveProgrammeSemester")]
        [HttpPost]
        public HttpResponseMessage SaveProgrammeSemester(ProgrammeSemesterRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveProgrammeSemester(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllProgrammeSemester")]
        [HttpGet]
        public HttpResponseMessage GetAllProgrammeSemester()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllProgrammeSemester());

        }
        [Route("api/OnlineExamAPI/SelectProgrammeSemester")]
        [HttpGet]
        public HttpResponseMessage SelectProgrammeSemester(ProgrammeSemesterRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectProgrammeSemester(request));

        }
        [Route("api/OnlineExamAPI/SaveExaminationName")]
        [HttpPost]
        public HttpResponseMessage SaveExaminationName(ExaminationNameRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveExaminationName(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllExaminationName")]
        [HttpGet]
        public HttpResponseMessage GetAllExaminationName()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllExaminationName());

        }
        [Route("api/OnlineExamAPI/SelectExaminationName")]
        [HttpGet]
        public HttpResponseMessage SelectExaminationName(ExaminationNameRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectExaminationName(request));

        }
        [Route("api/OnlineExamAPI/SaveExaminationSection")]
        [HttpPost]
        public HttpResponseMessage SaveExaminationSection(ExaminationSectionRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveExaminationSection(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllExaminationSection")]
        [HttpGet]
        public HttpResponseMessage GetAllExaminationSection()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllExaminationSection());

        }
        [Route("api/OnlineExamAPI/SelectExaminationSection")]
        [HttpGet]
        public HttpResponseMessage SelectExaminationSection(ExaminationSectionRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectExaminationSection(request));

        }
        [Route("api/OnlineExamAPI/SaveAssessment")]
        [HttpPost]
        public HttpResponseMessage SaveAssessment(AssessmentRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveAssessment(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllAssessment")]
        [HttpGet]
        public HttpResponseMessage GetAllAssessment()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllAssessment());

        }
        [Route("api/OnlineExamAPI/SelectAssessment")]
        [HttpGet]
        public HttpResponseMessage SelectAssessment(AssessmentRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectAssessment(request));

        }
        [Route("api/OnlineExamAPI/SaveSubjectAssessment")]
        [HttpPost]
        public HttpResponseMessage SaveSubjectAssessment(SubjectAssessmentRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveSubjectAssessment(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllSubjectAssessment")]
        [HttpGet]
        public HttpResponseMessage GetAllSubjectAssessment()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllSubjectAssessment());

        }
        [Route("api/OnlineExamAPI/SelectSubjectAssessment")]
        [HttpGet]
        public HttpResponseMessage SelectSubjectAssessment(SubjectAssessmentRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectSubjectAssessment(request));

        }
        [Route("api/OnlineExamAPI/SaveEmployee")]
        [HttpPost]
        public HttpResponseMessage SaveEmployee(EmployeeRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveEmployee(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllEmployee")]
        [HttpGet]
        public HttpResponseMessage GetAllEmployee()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllEmployee());

        }
        [Route("api/OnlineExamAPI/SelectEmployee")]
        [HttpGet]
        public HttpResponseMessage SelectEmployee(EmployeeRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectEmployee(request));

        }
        [Route("api/OnlineExamAPI/SaveSubjectAllocation")]
        [HttpPost]
        public HttpResponseMessage SaveSubjectAllocation(SubjectAllocationRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveSubjectAllocation(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllSubjectAllocation")]
        [HttpGet]
        public HttpResponseMessage GetAllSubjectAllocation()
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllSubjectAllocation());

        }
        [Route("api/OnlineExamAPI/SelectSubjectAllocation")]
        [HttpGet]
        public HttpResponseMessage SelectSubjectAllocation(SubjectAllocationRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectSubjectAllocation(request));

        }
        [Route("api/OnlineExamAPI/GetAllOnlineExamApp")]
        [HttpGet]
        public HttpResponseMessage GetAllOnlineExamApp(AdminOnlineExamRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllOnlineExamApp(request));
          
        }
        [Route("api/OnlineExamAPI/GetAllOnlineExamEvaluation")]
        [HttpGet]
        public HttpResponseMessage GetAllOnlineExamEvaluation(AdminOnlineExamRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllOnlineExamEvaluation(request));
           
        }
        [Route("api/OnlineExamAPI/GetAllOnlineExamSchedule")]
        [HttpGet]
        public HttpResponseMessage GetAllOnlineExamSchedule(AdminOnlineExamRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllOnlineExamSchedule(request));
           
        }
        [Route("api/OnlineExamAPI/GetAllOnlineExamResult")]
        [HttpGet]
        public HttpResponseMessage GetAllOnlineExamResult(AdminOnlineExamRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllOnlineExamResult(request));
           
        }
        [Route("api/OnlineExamAPI/GetAnserReview")]
        [HttpGet]
        public HttpResponseMessage GetAnserReview(AdminOnlineExamRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAnserReview(request));
           
        }
        [Route("api/OnlineExamAPI/SaveExaminationConfiguration")]
        [HttpPost]
        public HttpResponseMessage SaveExaminationConfiguration(AdminOnlineExamRequestDTO recordAttributer, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveExaminationConfiguration(recordAttributer, Operation));
          
        }
        [Route("api/OnlineExamAPI/SaveResultApproval")]
        [HttpPost]
        public HttpResponseMessage SaveResultApproval(AdminOnlineExamRequestDTO recordAttributer)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveResultApproval(recordAttributer));
           
        }
    }
}