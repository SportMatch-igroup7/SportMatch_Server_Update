using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class TrainerAreaController : ApiController
    {
        // GET: api/TrainerArea
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/TrainerArea/getTrainerArea/{trainerCode}/")]
        public List<TrainerArea> getTrainerArea(string trainerCode)
        {
            TrainerArea ta = new TrainerArea();
            return ta.GetTrainerArea(trainerCode);
        }

        [HttpPut]
        [Route("api/TrainerArea/UpdateTrainerArea")]
        public int UpdateTrainerArea([FromBody] TrainerArea [] t)
        {
            TrainerArea trainer = new TrainerArea();
            return trainer.UpdateTrainerArea(t);
        }

        // GET: api/TrainerArea/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TrainerArea
        public int Post([FromBody]TrainerArea[] area)
        {
            TrainerArea a = new TrainerArea();
            return a.insert(area);
        }

        // PUT: api/TrainerArea/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TrainerLanguage/5
        public void Delete(int id)
        {
        }
    }
}
