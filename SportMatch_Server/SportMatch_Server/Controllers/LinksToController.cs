﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;

namespace SportMatch_1.Controllers
{
    public class LinksToController : ApiController
    {
        // GET: api/LinksTo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/LinksTo/getLinks/{branchCode}/")]
        public List<LinksTo> getLinks(string branchCode)
        {
            LinksTo lt = new LinksTo();
            return lt.GetLinks(branchCode);
        }

        [HttpPut]
        [Route("api/LinksTo/UpdateLinksBranch")]
        public int UpdateLinksBranch([FromBody] LinksTo[] t)
        {
            LinksTo lt = new LinksTo();
            return lt.UpdateLinksBranch(t);
        }

        // GET: api/LinksTo/5
        public string Get(int id)
        {
            return "value";
        }


        // POST: api/LinksTo

        public int Post([FromBody] LinksTo[] LT)
        {
            LinksTo l = new LinksTo();
            return l.InsertLinksTo(LT);
        }



        // PUT: api/LinksTo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LinksTo/5
        public void Delete(int id)
        {
        }
    }
}
