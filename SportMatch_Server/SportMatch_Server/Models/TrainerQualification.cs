using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class TrainerQualification
    {
        int trainerCode;
        int qualificationTypeCode;
        string fromDate;
        int populationCode;
        string documentPath;
        string typeName;
        string pName;
        int yearsOfExperience;

        public int TrainerCode { get => trainerCode; set => trainerCode = value; }
        public int QualificationTypeCode { get => qualificationTypeCode; set => qualificationTypeCode = value; }
        public string FromDate { get => fromDate; set => fromDate = value; }
        public int PopulationCode { get => populationCode; set => populationCode = value; }
        public string DocumentPath { get => documentPath; set => documentPath = value; }
        public string TypeName { get => typeName; set => typeName = value; }
        public string PName { get => pName; set => pName = value; }
        public int YearsOfExperience { get => yearsOfExperience; set => yearsOfExperience = value; }

        public TrainerQualification()
        { }

        public TrainerQualification(int trainerCode, int qualificationTypeCode, string fromDate, int populationCode, string doc)
        {
            this.TrainerCode = trainerCode;
            this.QualificationTypeCode = qualificationTypeCode;
            this.FromDate = fromDate;
            this.PopulationCode = populationCode;
            this.DocumentPath = doc;
        }

        public TrainerQualification(int trainerCode, int qualificationTypeCode)
        {
            this.TrainerCode = trainerCode;
            this.QualificationTypeCode = qualificationTypeCode;
        }

        public TrainerQualification(int trainerCode, int qualificationTypeCode, string fromDate, int populationCode, string doc, string qualName, string popName, int exp)
        {
            this.TrainerCode = trainerCode;
            this.QualificationTypeCode = qualificationTypeCode;
            this.FromDate = fromDate;
            this.PopulationCode = populationCode;
            this.DocumentPath = doc;
            TypeName = qualName;
            PName = popName;
            YearsOfExperience = exp;
        }
        public int insert(TrainerQualification[] tQ)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertTrainerQualification(tQ);
            return numAffected;
        }

        public int updateTrainerQualifications(TrainerQualification[] tQ)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.updateTrainerQualifications(tQ);
            return numAffected;
        }

        public int deleteTrainerQualifications(TrainerQualification tQ)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.DeleteTrainerQualifcations(tQ);
            return numAffected;
        }

        public List<TrainerQualification> GetTrainerQual(string trainerCode)
        {
            DBservices dbs = new DBservices();
            List<TrainerQualification> arrQual = dbs.GetTrainerQual(trainerCode);
            return arrQual;
        }


    }
}