using Lincoln.OnlineExam.Request;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Lincoln.Admin.Web.Apies
{
    public partial class OnlineExamAPIController
    {
        [Route("api/OnlineExamAPI/SavePaper")]
        [HttpPost]
        public HttpResponseMessage SavePaper(PaperRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SavePaper(request, Operation));

        }
        [Route("api/OnlineExamAPI/SelectPaper")]
        [HttpGet]
        public HttpResponseMessage SelectPaper(PaperRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectPaper(request));

        }
        [Route("api/OnlineExamAPI/SavePaperDetails")]
        [HttpPost]
        public HttpResponseMessage SavePaperDetails(PaperDetailsRequestDTO request, string Operation)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SavePaperDetails(request, Operation));

        }
        [Route("api/OnlineExamAPI/GetAllPaperDetails")]
        [HttpGet]
        public HttpResponseMessage GetAllPaperDetails(PaperDetailsRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.GetAllPaperDetails(request));

        }
        [Route("api/OnlineExamAPI/SelectAllPaperDetails")]
        [HttpGet]
        public HttpResponseMessage SelectAllPaperDetails(PaperDetailsRequestDTO request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, onlineExamService.SelectAllPaperDetails(request));

        }
    }
}