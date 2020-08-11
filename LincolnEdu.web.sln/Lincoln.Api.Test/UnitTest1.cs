using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lincoln.OnlineExam;
using Lincoln.Admin.Web.Apies;
using Lincoln.OnlineExam.Response;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Http.Hosting;
using System.Web.Http;

namespace Lincoln.Api.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IOnlineExam onlineExamService;
        public UnitTest1()
        {
            this.onlineExamService = new OnlineExamService();
        }

        [TestMethod]
        public void GetAllAcademicLevel()
        {
            
            var controller = new OnlineExamAPIController(onlineExamService);
            controller.Request = new System.Net.Http.HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            var result = controller.GetAllAcademicLevel();
            Assert.IsNotNull(result);
        }
    }
}
