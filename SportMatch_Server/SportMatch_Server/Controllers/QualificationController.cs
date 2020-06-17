using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class QualificationController : ApiController
    {
        // GET: Qualification
        public List<Qualification> Get()
        {
            Qualification q = new Qualification();
            return q.getQualifications();
        }

        public int Post([FromBody]Qualification Q)
        {
            return Q.InsertQualification();
        }

        // PUT: api/Parameter/5
        public int Put([FromBody] Qualification P)
        {
            Qualification qualification = new Qualification();
            return qualification.UpdateQualification(P);
        }
        // DELETE: api/Parameter/5
        [HttpDelete]
        [Route("api/Qualification/{id}")]
        public int Delete(int id)
        {
            Qualification d = new Qualification();
            return d.Delete(id);
        }
    }
}