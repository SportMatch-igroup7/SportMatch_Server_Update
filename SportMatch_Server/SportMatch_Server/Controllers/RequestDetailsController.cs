using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class RequestDetailsController : ApiController
    {
        [HttpGet]
        [Route("api/RequestDetails/getBranchRequests/{branchCode}/")]
        public List<RequestDetails> getBranchRequests(string branchCode)
        {
            RequestDetails r = new RequestDetails();
            return r.GetBranchRequests(branchCode);
        }

        [HttpGet]
        [Route("api/RequestDetails/getTrainerRequests/{trainerCode}/")]
        public List<RequestDetails> getTrainerRequests(string trainerCode)
        {
            RequestDetails r = new RequestDetails();
            return r.GetTrainerRequests(trainerCode);
        }
    }
}