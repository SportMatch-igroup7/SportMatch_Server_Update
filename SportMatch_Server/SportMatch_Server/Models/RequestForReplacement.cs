using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class RequestForReplacement
    {
        int replacementCode;
        string publishDateTime;
        string contactName;
        int branchCode;
        int classTypeCode;
        string fromHour;
        string toHour;
        string replacementDate;
        string classDescription;
        string comments;
        int difficultyLevelCode;
        int maxPrice;
        int languageCode;
        int populationCode;

        public int ReplacementCode { get => replacementCode; set => replacementCode = value; }
        public string PublishDateTime { get => publishDateTime; set => publishDateTime = value; }
        public string ContactName { get => contactName; set => contactName = value; }
        public int BranchCode { get => branchCode; set => branchCode = value; }
        public int ClassTypeCode { get => classTypeCode; set => classTypeCode = value; }
        public string FromHour { get => fromHour; set => fromHour = value; }
        public string ToHour { get => toHour; set => toHour = value; }
        public string ReplacementDate { get => replacementDate; set => replacementDate = value; }
        public string ClassDescription { get => classDescription; set => classDescription = value; }
        public string Comments { get => comments; set => comments = value; }
        public int DifficultyLevelCode { get => difficultyLevelCode; set => difficultyLevelCode = value; }
        public int MaxPrice { get => maxPrice; set => maxPrice = value; }
        public int LanguageCode { get => languageCode; set => languageCode = value; }
        public int PopulationCode { get => populationCode; set => populationCode = value; }

      
    public RequestForReplacement() { }

        public RequestForReplacement(int replacementCode, string contactName, int branchCode, int classTypeCode, string classDescription, string comments, int difficultyLevelCode, int maxPrice, int languageCode, int populationCode, string publishDateTime,   string fromHour, string toHour, string replacementDate )
        {
            this.ReplacementCode= replacementCode;
            this.ContactName = contactName;
            this.BranchCode = branchCode;
            this.ClassTypeCode = classTypeCode;
            this.ClassDescription = classDescription;
            this.Comments = comments;
            this.DifficultyLevelCode = difficultyLevelCode;
            this.MaxPrice = maxPrice;
            this.LanguageCode = languageCode;
            this.PopulationCode = populationCode;
            this.PublishDateTime= publishDateTime;
            this.FromHour= fromHour;
            this.ToHour= toHour;
            this.ReplacementDate= replacementDate;
           
        }

       
        public RequestForReplacement(int code1)
        {
            ReplacementCode = code1;
        }

        public RequestForReplacement insert()
        {
            DBservices dbs = new DBservices();
            return dbs.insertRequest(this);
            //RequestForReplacement numAffected = dbs.insertRequest(this);
            //return numAffected;
        }


        public int GetBranchAreaCode(int branchCode)
        {
            //function to get areaCode by branchCode for the element
            int areaCode = 0;
            DBservices dbs = new DBservices();
            List<Branch> arrBranchs = dbs.GetBranchesList(branchCode);
            foreach (Branch item in arrBranchs)
            {
                areaCode= item.AreaCode;
            }
            return areaCode;
        }
    }  
}