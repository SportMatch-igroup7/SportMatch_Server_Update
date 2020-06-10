using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class Branch
    {
        int branchCode;
        string name;
        string address;
        string phoneNo;
        string email;
        string description;
        int companyNo;
        string password;
        int areaCode;
        string areaName;
        string logo;
        string companyName;

        public int BranchCode { get => branchCode; set => branchCode = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNo { get => phoneNo; set => phoneNo = value; }
        public string Email { get => email; set => email = value; }
        public string Description { get => description; set => description = value; }
        public int CompanyNo { get => companyNo; set => companyNo = value; }
        public string Password { get => password; set => password = value; }
        public int AreaCode { get => areaCode; set => areaCode = value; }
        public string AreaName { get => areaName; set => areaName = value; }
        public string Logo { get => logo; set => logo = value; }
        public string CompanyName { get => companyName; set => companyName = value; }

        public Branch(string n, string ad, string ph, string em, string des, int com, string pas, int ar)
        {
            Name = n;
            Address = ad;
            PhoneNo = ph;
            Email = em;
            Description = des;
            CompanyNo = com;
            Password = pas;
            AreaCode = ar;
        }

        public Branch(string n, string ad, string ph, string em, string des, int com, string pas, int ar, string compName, string arName, string log)
        {
            Name = n;
            Address = ad;
            PhoneNo = ph;
            Email = em;
            Description = des;
            companyNo = com;
            Password = pas;
            AreaCode = ar;
            CompanyName = compName;
            AreaName = arName;
            Logo = log;
        }

        public Branch(int bc, string n, string ad, string ph, string em, string des, int com, string pas, int ar, string compName, string arName, string log)
        {
            BranchCode = bc;
            Name = n;
            Address = ad;
            PhoneNo = ph;
            Email = em;
            Description = des;
            companyNo = com;
            Password = pas;
            AreaCode = ar;
            CompanyName = compName;
            AreaName = arName;
            Logo = log;
        }

        public Branch()
        {

        }

        public Branch(int code1)
        {
            branchCode = code1;
        }
        public Branch insert()
        {
            DBservices dbs = new DBservices();
            //int numAffected = dbs.insertBranch(this);
            return dbs.insertBranch(this);
            //return numAffected;
        }

        public List<Branch> GetBranchList()
        {
            DBservices dbs = new DBservices();
            List<Branch> arrBranches = dbs.GetBranchList();
            return arrBranches;
        }

        public Branch GetBranch(string branchCode)
        {
            DBservices dbs = new DBservices();
            Branch b = dbs.GetBranch(branchCode);
            return b;
        }

        public List<Branch> GetBranchesList(int branchCode)
        {
            DBservices dbs = new DBservices();
            List<Branch> arrBranchs = dbs.GetBranchesList(branchCode);
            return arrBranchs;
        }

        public int updetBranch(Branch b)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdetBranch(b);
        }

    }
}