using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class Company
    {
        int companyNo;
        string name;
        string logo;

        public int CompanyNo { get => companyNo; set => companyNo = value; }
        public string Name { get => name; set => name = value; }
        public string Logo { get => logo; set => logo = value; }

        public Company() { }

        public Company(int comNo, string name, string logo)
        {
            CompanyNo = comNo;
            Name = name;
            Logo = logo;
        }

        public List<Company> getCompany()
        {
            DBservices dbs = new DBservices();
            List<Company> arrCompany = dbs.getCompany();
            return arrCompany;
        }

    }


}