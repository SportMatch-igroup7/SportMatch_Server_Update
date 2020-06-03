using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class BranchParameter
    {
        int branchCode;
        int parameterCode;
        float parameterWeight;
        string ParameterName;

        public int BranchCode { get => branchCode; set => branchCode = value; }
        public int ParameterCode { get => parameterCode; set => parameterCode = value; }
        public float ParameterWeight { get => parameterWeight; set => parameterWeight = value; }
        public string ParameterName1 { get => ParameterName; set => ParameterName = value; }

        public BranchParameter()
        { }

        public BranchParameter(int bCode, int pCode, float pWeight)
        {
            this.BranchCode = bCode;
            this.ParameterCode = pCode;
            this.parameterWeight = pWeight;
        }
        public void insertParameters()
        {
        }
        public BranchParameter(int bCode, int pCode, float pWeight, string name)
        {
            this.BranchCode = bCode;
            this.ParameterCode = pCode;
            this.parameterWeight = pWeight;
            ParameterName1 = name;
        }
        public int InsertBranchParameter(BranchParameter[] p)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.InsertBranchParameter(p);
            return numAffected;
        }
        public int UpdateBranchParameter(BranchParameter[] b)
        {

            DBservices dbs = new DBservices();

            return dbs.UpdateBranchParameter(b);

        }
        public List<BranchParameter> GetBranchParameterList(int branchCode)
        {
            DBservices dbs = new DBservices();
            List<BranchParameter> arrBranchParameters = dbs.GetBranchParameterList(branchCode);
            return arrBranchParameters;
        }
    }
}