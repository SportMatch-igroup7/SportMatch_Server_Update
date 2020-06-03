using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class TrainerLanguageController : ApiController
    {
        // GET: api/TrainerLanguage
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/TrainerLanguage/getTrainerLang/{trainerCode}/")]
        public List<TrainerLanguage> getTrainerLang(string trainerCode)
        {
            TrainerLanguage t = new TrainerLanguage();
            return t.GetTrainerLang(trainerCode);
        }

        [HttpPut]
        [Route("api/TrainerArea/UpdateTrainerLang")]
        public int UpdateTrainerLang([FromBody] TrainerLanguage[] t)
        {
            TrainerLanguage trainer = new TrainerLanguage();
            return trainer.UpdateTrainerLang(t);
        }

        // GET: api/TrainerLanguage/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TrainerLanguage
        public int Post([FromBody]TrainerLanguage[] lan)
        {
            TrainerLanguage l = new TrainerLanguage();
            return l.insert(lan);
        }

        // PUT: api/TrainerLanguage/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TrainerLanguage/5
        public void Delete(int id)
        {
        }
    }
}
