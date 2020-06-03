using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class BranchController : ApiController
    {
        // GET: api/Branch
        public List<Branch> Get()
        {
            Branch b = new Branch();
            return b.GetBranchList();
        }

        [HttpGet]
        [Route("api/Branch/getBranch/{branchCode}/")]
        public Branch getBranch(string branchCode)
        {
            Branch b = new Branch();
            return b.GetBranch(branchCode);
        }

        [HttpGet]
        [Route("api/Branch/getBranchesList/{branchCode}/")]
        public List<Branch> Get(int branchCode)
        {
            Branch b = new Branch();
            return b.GetBranchesList(branchCode);
        }

        // POST: api/Branch
        public Branch Post([FromBody]Branch b)
        {
            return b.insert();
        }

        // PUT: api/Branch/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Branch/5
        public void Delete(int id)
        {
        }

    }
}
