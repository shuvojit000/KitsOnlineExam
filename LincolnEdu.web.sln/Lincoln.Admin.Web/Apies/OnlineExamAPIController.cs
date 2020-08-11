using Lincoln.OnlineExam;
using Lincoln.OnlineExam.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lincoln.Admin.Web.Apies
{
    public partial class OnlineExamAPIController : ApiController
    {
        private readonly IOnlineExam onlineExamService;

        public OnlineExamAPIController(IOnlineExam onlineExamService)
        {
            this.onlineExamService = onlineExamService;
        }
        //[Route("api/OnlineExamAPI/GetName")]
        //[HttpGet]
        //public HttpResponseMessage GetName(string id)
        //{

        //    return Request.CreateResponse(HttpStatusCode.OK, "Shuvojit");
        //}


        [Route("api/OnlineExamAPI/GetStudentExamination")]
        [HttpGet]
        public HttpResponseMessage GetStudentExamination(OnlineTestRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetStudentExamination(request));

        }
        [Route("api/OnlineExamAPI/GetExamQuestionSection")]
        [HttpGet]
        public HttpResponseMessage GetExamQuestionSection(ExamQuestionSectionRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetExamQuestionSection(request));

        }
        [Route("api/OnlineExamAPI/GetQuestionPaper")]
        [HttpGet]
        public HttpResponseMessage GetQuestionPaper(ExamQuestionSectionRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetQuestionPaper(request));

        }
        [Route("api/OnlineExamAPI/SaveExaminationTest")]
        [HttpPost]
        public HttpResponseMessage SaveExaminationTest(ExaminationTestRequestDTO recordAttributer, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveExaminationTest(recordAttributer, Operation));

        }
        [Route("api/OnlineExamAPI/GetExaminationTest")]
        [HttpGet]
        public HttpResponseMessage GetExaminationTest(ExaminationTestRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetExaminationTest(request));


        }
        [Route("api/OnlineExamAPI/SaveExaminationSheet")]
        [HttpPost]
        public HttpResponseMessage SaveExaminationSheet(StudentExaminationSheetResponseDTO recordAttributer, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveExaminationSheet(recordAttributer, Operation));

        }
        [Route("api/OnlineExamAPI/GetAttemptQuestion")]
        [HttpGet]
        public HttpResponseMessage GetAttemptQuestion(ExaminationTestRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAttemptQuestion(request, Operation));

        }
        [Route("api/OnlineExamAPI/SaveTimerTime")]
        [HttpPost]
        public HttpResponseMessage SaveTimerTime(ExaminationTestRequestDTO recordAttributer)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SaveTimerTime(recordAttributer));

        }
    }
}
