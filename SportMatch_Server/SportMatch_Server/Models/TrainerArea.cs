using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class TrainerArea
    {
        int trainerCode;
        int areaCode;
        string areaName;

        public int TrainerCode { get => trainerCode; set => trainerCode = value; }
        public int AreaCode { get => areaCode; set => areaCode = value; }
        public string AreaName { get => areaName; set => areaName = value; }

        public TrainerArea()
        { }
        public TrainerArea(int trainerCode, int acode)
        {
            this.TrainerCode = trainerCode;
            this.AreaCode = acode;
        }

        public TrainerArea(int trainerCode, int acode, string name)
        {
            this.TrainerCode = trainerCode;
            this.AreaCode = acode;
            this.AreaName = name;
        }

        public int insert(TrainerArea[] area)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertTrainerArea(area);
            return numAffected;
        }

        public List<TrainerArea> GetTrainerArea(string trainerCode)
        {
            DBservices dbs = new DBservices();
            List<TrainerArea> arrArea = dbs.GetTrainerArea(trainerCode);
            return arrArea;
        }

        public int UpdateTrainerArea(TrainerArea [] t)
        {

            DBservices dbs = new DBservices();

            return dbs.DeleteTrainerArea(t);

        }
    }
}