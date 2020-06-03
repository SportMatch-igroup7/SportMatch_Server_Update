using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class BranchParameterController : ApiController
    {
        // GET: api/BranchParameter
        [HttpGet]
        [Route("api/BranchParameter/GetBranchParameter/{branchCode}/")]
        public List<BranchParameter> GetBranchParameter(string branchCode)
        {
            int branchCode1 = int.Parse(branchCode);
            BranchParameter bp = new BranchParameter();
            return bp.GetBranchParameterList(branchCode1);
        }

        
        // POST: api/BranchParameter

        //public int PostBranchParameter([FromBody] BranchParameter [] p)
        //{
        //    foreach (BranchParameter item in p)
        //    {
        //        float numOfParameters = item.ParameterWeight;
        //        item.ParameterWeight = 100 / numOfParameters;
               
        //    }
        //    return p.InsertBranchParameter();
        //}
        public int PostBranchParameter([FromBody]BranchParameter[] p)
        {
            BranchParameter t = new BranchParameter();
            return t.InsertBranchParameter(p);
        }
        // PUT: api/BranchParameter/5
        public int Put([FromBody] BranchParameter [] b)
        {
            BranchParameter parameter = new BranchParameter();
            return parameter.UpdateBranchParameter(b);
        }
        // DELETE: api/BranchParameter/5
        public void Delete(int id)
        {
        }
    }
}
