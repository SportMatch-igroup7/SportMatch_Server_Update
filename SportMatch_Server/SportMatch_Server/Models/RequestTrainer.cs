using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class RequestTrainer
    {
        int requestCode;
        int trainerCode;
        string isApprovedByTrainer;
        string requestStatus;

        public int RequestCode { get => requestCode; set => requestCode = value; }
        public int TrainerCode { get => trainerCode; set => trainerCode = value; }
        public string IsApprovedByTrainer { get => isApprovedByTrainer; set => isApprovedByTrainer = value; }
        public string RequestStatus { get => requestStatus; set => requestStatus = value; }

        public RequestTrainer() { }

        public RequestTrainer(int re)
        {
            this.RequestCode = re;
        }

        public RequestTrainer(int re, int tr,string status)
        {
            this.RequestCode = re;
            this.TrainerCode = tr;
            this.RequestStatus = status;
        }

        public RequestTrainer(int re, int tr,string app, string status)
        {
            this.RequestCode = re;
            this.TrainerCode = tr;
            this.IsApprovedByTrainer = app;
            this.RequestStatus = status;
        }


        public int Insert(RequestTrainer [] r)
        {

            DBservices dbs = new DBservices();
            return dbs.insertRequestTrainer(r);
        }

        public int UpdateRequestTrainer(RequestTrainer r)
        {

            DBservices dbs = new DBservices();

            return dbs.UpdateRequestTrainer(r);

        }

        public int UpdateIsApprovedTrainer(RequestTrainer r)
        {

            DBservices dbs = new DBservices();

            return dbs.UpdateIsApprovedTrainer(r);

        }

        public int UpdateIsApprovedTrainerFalse(RequestTrainer r)
        {

            DBservices dbs = new DBservices();

            return dbs.UpdateIsApprovedTrainerFalse(r);

        }

        public int DeleteRequest(RequestTrainer r)
        {

            DBservices dbs = new DBservices();

            return dbs.DeleteRequest(r);

        }

    }
}