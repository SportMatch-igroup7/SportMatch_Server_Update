using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class RequestTrainerController : ApiController
    {
        // GET: api/RequestTrainer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPut]
        [Route("api/RequestTrainer/PutIsApprovedTrainer")]
        public int PutIsApprovedTrainer([FromBody] RequestTrainer r)
        {
            RequestTrainer rt = new RequestTrainer();
            return rt.UpdateIsApprovedTrainer(r);
        }

        [HttpPut]
        [Route("api/RequestTrainer/PutIsApprovedTrainerFalse")]
        public int PutIsApprovedTrainerFalse([FromBody] RequestTrainer r)
        {
            RequestTrainer rt = new RequestTrainer();
            return rt.UpdateIsApprovedTrainerFalse(r);
        }

        [HttpPut]
        [Route("api/RequestTrainer/PutRequestTrainer")]
        public int PutRequestTrainer([FromBody] RequestTrainer r)
        {
            RequestTrainer rt = new RequestTrainer();
            return rt.UpdateRequestTrainer(r);
        }

        [HttpPut]
        [Route("api/RequestTrainer/ReopenRequest")]
        public int ReopenRequest([FromBody] RequestTrainer r)
        {
            RequestTrainer rt = new RequestTrainer();
            return rt.ReopenRequest(r);
        }


        [HttpDelete]
        [Route("api/RequestTrainer/DeleteRequest")]
        public int DeleteRequest([FromBody] RequestTrainer r)
        {
            RequestTrainer rt = new RequestTrainer();
            return rt.DeleteRequest(r);
        }

        [HttpPut]
        [Route("api/RequestTrainer/PutRequestTrainerRate")]
        public int PutRequestTrainerRate([FromBody] RequestTrainer r)
        {
            RequestTrainer rt = new RequestTrainer();
            return rt.UpdateRequestTrainerRate(r);
        }


        // GET: api/RequestTrainer/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/RequestTrainer

        public int Post([FromBody] RequestTrainer [] rt)
        {
            RequestTrainer r = new RequestTrainer();
            return r.Insert(rt);
        }
        //// PUT: api/RequestTrainer/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/RequestTrainer/5
        //public void Delete(int id)
        //{
        //}
    }
}
