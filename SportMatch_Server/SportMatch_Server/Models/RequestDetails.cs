using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class RequestDetails
    {
        string companyName;
        string logo;
        int replacmentCode;
        string contactName;
        int branchCode;
        string branchName;
        string typeName;
        string classDescription;
        string comments;
        string levelName;
        int maxPrice;
        string lName;
        string pName;
        string publishDateTime;
        string fromHour;
        string toHour;
        string replacementDate;
        bool isHistory;
        int trainerCode;
        string isAprrovedByTrainer;
        string requestStatus;
        string isRated;

        public int ReplacmentCode { get => replacmentCode; set => replacmentCode = value; }
        public string ContactName { get => contactName; set => contactName = value; }
        public int BranchCode { get => branchCode; set => branchCode = value; }
        public string TypeName { get => typeName; set => typeName = value; }
        public string ClassDescription { get => classDescription; set => classDescription = value; }
        public string Comments { get => comments; set => comments = value; }
        public string LevelName { get => levelName; set => levelName = value; }
        public int MaxPrice { get => maxPrice; set => maxPrice = value; }
        public string LName { get => lName; set => lName = value; }
        public string PName { get => pName; set => pName = value; }
        public string PublishDateTime { get => publishDateTime; set => publishDateTime = value; }
        public string FromHour { get => fromHour; set => fromHour = value; }
        public string ToHour { get => toHour; set => toHour = value; }
        public string ReplacementDate { get => replacementDate; set => replacementDate = value; }
        public bool IsHistory { get => isHistory; set => isHistory = value; }
        public int TrainerCode { get => trainerCode; set => trainerCode = value; }
        public string IsAprrovedByTrainer { get => isAprrovedByTrainer; set => isAprrovedByTrainer = value; }
        public string RequestStatus { get => requestStatus; set => requestStatus = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string Logo { get => logo; set => logo = value; }
        public string IsRated { get => isRated; set => isRated = value; }
        public string BranchName { get => branchName; set => branchName = value; }

        public RequestDetails() { }

        public RequestDetails(string company, string logo, int reCode, string con, int branch, string bname, string typeN, string classDesc, string comm, string levelname, int maxp, string lname, string pname, string pubd, string from, string to, string repD, bool his, int trainer, string app, string status)
        {
            CompanyName = company;
            Logo = logo;
            ReplacmentCode = reCode;
            ContactName = con;
            BranchCode = branch;
            BranchName = bname;
            TypeName = typeN;
            ClassDescription = classDesc;
            Comments = comm;
            LevelName = levelname;
            MaxPrice = maxp;
            LName = lname;
            PName = pname;
            PublishDateTime = pubd;
            FromHour = from;
            ToHour = to;
            ReplacementDate = repD;
            IsHistory = his;
            TrainerCode = trainer;
            IsAprrovedByTrainer = app;
            RequestStatus = status;
        }

        public RequestDetails(string company, string logo, int reCode, string con, int branch,string bname, string typeN, string classDesc, string comm, string levelname, int maxp, string lname, string pname, string pubd, string from, string to, string repD, bool his, int trainer, string app, string status, string isRated)
        {
            CompanyName = company;
            Logo = logo;
            ReplacmentCode = reCode;
            ContactName = con;
            BranchCode = branch;
            BranchName = bname;
            TypeName = typeN;
            ClassDescription = classDesc;
            Comments = comm;
            LevelName = levelname;
            MaxPrice = maxp;
            LName = lname;
            PName = pname;
            PublishDateTime = pubd;
            FromHour = from;
            ToHour = to;
            ReplacementDate = repD;
            IsHistory = his;
            TrainerCode = trainer;
            IsAprrovedByTrainer = app;
            RequestStatus = status;
            IsRated = isRated;
        }

        public List<RequestDetails> GetBranchRequests (string branchCode)
        {
            DBservices dbs = new DBservices();
            List<RequestDetails> requests = dbs.GetBranchRequests(branchCode);
            return requests;
        }

        public List<RequestDetails> GetTrainerRequests(string trainerCode)
        {
            DBservices dbs = new DBservices();
            List<RequestDetails> requests = dbs.GetTrainerRequests(trainerCode);
            return requests;
        }
    }  
}