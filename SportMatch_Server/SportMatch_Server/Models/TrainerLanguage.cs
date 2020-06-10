using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class TrainerLanguage
    {
        int lCode;
        int trainerCode;
        string lName;

        public int LCode { get => lCode; set => lCode = value; }
        public int TrainerCode { get => trainerCode; set => trainerCode = value; }
        public string LName { get => lName; set => lName = value; }

        public TrainerLanguage()
        { }
        public TrainerLanguage(int lCode, int trainerCode)
        {
            this.LCode = lCode;
            this.TrainerCode = trainerCode;
        }

        public TrainerLanguage(int lCode, int trainerCode, string name)
        {
            this.LCode = lCode;
            this.TrainerCode = trainerCode;
            this.LName = name;
        }
        public int insert(TrainerLanguage[] lan)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertTrainerLanguage(lan);
            return numAffected;
        }

        public List<TrainerLanguage> GetTrainerLang(string trainerCode)
        {
            DBservices dbs = new DBservices();
            List<TrainerLanguage> arrLang = dbs.GetTrainerLang(trainerCode);
            return arrLang;
        }

        public int UpdateTrainerLang(TrainerLanguage[] t)
        {

            DBservices dbs = new DBservices();

            return dbs.DeleteTrainerLang(t);

        }
    }
}