using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class CompanyController : ApiController
    {
        public List<Company> Get()
        {
            Company c = new Company();
            return c.getCompany();
        }
        public int Post([FromBody]Company q)
        {
            return q.InsertCompany();
        }

        // PUT: api/Paraץmeter/5
        public int Put([FromBody] Company Pu)
        {
            Company company = new Company();
            return company.UpdateCompany(Pu);
        }

        //// DELETE: api/Parameter/5
        [HttpDelete]
        [Route("api/Company/{id}")]
        public int Delete(int id)
        {
            Company de = new Company();
            return de.Delete(id);
        }
    }
}

