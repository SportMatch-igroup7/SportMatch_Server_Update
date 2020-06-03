using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class LinksTrainer
    {
        int linkCode;
        int trainerCode;
        string link;
        string linkName;

        public int LinkCode { get => linkCode; set => linkCode = value; }
        public int TrainerCode { get => trainerCode; set => trainerCode = value; }
        public string Link { get => link; set => link = value; }
        public string LinkName { get => linkName; set => linkName = value; }

        public LinksTrainer()
        {

        }
        public LinksTrainer(int lcode, int tcode, string lin)
        {
            LinkCode = lcode;
            TrainerCode = tcode;
            Link = lin; 
        }

        public LinksTrainer(int lcode, int tcode, string lin, string name)
        {
            LinkCode = lcode;
            TrainerCode = tcode;
            Link = lin;
            LinkName = name;
        }

        public int insert(LinksTrainer [] LT)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertTrainerLinks(LT);
            return numAffected;
        }

        public List<LinksTrainer> GetLinksTrainer(string trainerCode)
        {
            DBservices dbs = new DBservices();
            List<LinksTrainer> arrLinks = dbs.GetTrainerLinks(trainerCode);
            return arrLinks;
        }

        public int UpdateLinksTrainer(LinksTrainer[] t)
        {

            DBservices dbs = new DBservices();

            return dbs.UpdateLinksTrainer(t);

        }

    }
}