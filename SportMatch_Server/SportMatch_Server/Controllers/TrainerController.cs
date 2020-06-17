using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class TrainerController : ApiController
    {
        // GET: api/Trainer
        public List<Trainer> Get()
        {
            Trainer t = new Trainer();
            return t.GetTrainerList();
        }

        [HttpGet]
        [Route("api/Trainer/getTrainer/{trainerCode}/")]
        public Trainer getTrainer(string trainerCode)
        {
            Trainer t = new Trainer();
            return t.GetTrainer(trainerCode);
        }

        // GET: api/Trainer
        [HttpPost]
        [Route("api/Trainer/GetTrainerMatchRequest")] ///{r}/
        public List<Match> GetTrainerMatchRequest([FromBody] RequestForReplacement r)
        {
            int replacementCode=r.ReplacementCode;
            int branchCode=r.BranchCode;
            int classTypeCode=r.ClassTypeCode;
            int maxPrice=r.MaxPrice;
            int languageCode=r.LanguageCode;
            int populationCode=r.PopulationCode;

            Trainer t = new Trainer();
            return t.GetMatchTrainerList(replacementCode, branchCode,classTypeCode, maxPrice, languageCode, populationCode);
        }

        [HttpPut]
        [Route("api/Trainer/UpdateTrainerPersonalDetails")]
        public int UpdateTrainerPersonalDetails([FromBody] Trainer t)
        {
            Trainer trainer = new Trainer();
            return trainer.UpdateTrainerPersonalDetails(t);
        }


        // GET: api/Trainer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Trainer
        public Trainer Post([FromBody]Trainer t)
        {
            return t.insert();
        }

        // PUT: api/Trainer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Trainer/5
        public void Delete(int id)
        {
        }

        [HttpPut]
        [Route("api/RequestTrainer/PutTrainer")]
        public void PutTrainer(int id, [FromBody]string value)
        {
        }
        [HttpPut]
        [Route("api/RequestTrainer/PutTrainerRate")]
        public int PutTrainerRate([FromBody] Trainer r)
        {
            Trainer rt = new Trainer();
            return rt.UpdateTrainerRate(r);
        }
    }
}

