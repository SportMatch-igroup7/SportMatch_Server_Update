using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class LinksTrainerController : ApiController
    {
        // GET: api/LinksTrainer

        [HttpGet]
        [Route("api/LinksTrainer/getLinksTrainer/{trainerCode}/")]
        public List<LinksTrainer> getLinksTrainer(string trainerCode)
        {
            LinksTrainer lt = new LinksTrainer();
            return lt.GetLinksTrainer(trainerCode);
        }

        [HttpPost]
        [Route("api/LinksTrainer/postLinksTrainer")]
        public int postLinksTrainer([FromBody]LinksTrainer[] LT)
        {
            LinksTrainer lt = new LinksTrainer();
            return lt.insert(LT);
        }

        [HttpPut]
        [Route("api/LinksTrainer/UpdateLinksTrainer")]
        public int UpdateLinksTrainer([FromBody] LinksTrainer[] t)
        {
            LinksTrainer trainer = new LinksTrainer();
            return trainer.UpdateLinksTrainer(t);
        }

        // GET: api/LinksTrainer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LinksTrainer
        public Trainer Post([FromBody]Trainer t)
        {
            return t.insert();
        }

        // PUT: api/LinksTrainer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LinksTrainer/5
        public void Delete(int id)
        {
        }
    }
}