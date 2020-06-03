using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using SportMatch_1.Controllers;
using SportMatch_1.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // Insert Functions
    //--------------------------------------------------------------------------------------------------
    private int findBranchCode(Branch b)
    {
        SqlConnection con = null;
        int branchCode = 0;
        try
        {
            con = connect("DB7");
            String selectSTR = "select BranchCode from SM_Branch where Email='" + b.Email + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                branchCode = Convert.ToInt32(dr["BranchCode"]);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return branchCode;
    }


    private int findTrainerCode(Trainer t)
    {
        SqlConnection con = null;
        int trainerCode = 0;
        try
        {
            con = connect("DB7");
            String selectSTR = "select TrainerCode from SM_Trainer where Email='" + t.Email + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                trainerCode = Convert.ToInt32(dr["TrainerCode"]);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return trainerCode;
    }
    public Branch insertBranch(Branch b)
    {
        int numEffected = 0;
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommand(b);      // helper method to build the insert string
                                                  // SqlCommand scalarStr = new SqlCommand("select * from SM_Branch where Email = '" + branch.Email + "'");

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            numEffected = cmd.ExecuteNonQuery(); // execute the command
            //numEffected = Convert.ToInt32(scalarStr.ExecuteScalar());

        }
        catch (Exception ex)
        {
            return null;
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        b.BranchCode = findBranchCode(b);

        return b;
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommand(Branch branch)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}' ,'{4}', '{5}' ,'{6}', '{7}')", branch.Name, branch.Address, branch.PhoneNo, branch.Email, branch.Description, branch.CompanyNo.ToString(), branch.Password, branch.AreaCode.ToString());
        String prefix = "INSERT INTO SM_Branch" + "(BranchName , BranchAddress  , PhoneNo , Email ,BranchDescription,CompanyNo , BranchPassword ,AreaCode ) ";
        command = prefix + sb.ToString();

        return command;
    }
    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandTrainer(Trainer t)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}' ,'{4}', '{5}' ,'{6}', '{7}' ,{8},'{9}','{10}',{11})", t.FirstName, t.LastName, t.Email, t.Phone1, t.Phone2, t.Gender, t.Password, t.AboutMe, t.PricePerHour.ToString(), t.DateOfBirth, t.Image, t.Rate);
        String prefix = "INSERT INTO SM_Trainer" + "(FirstName , LastName , Email , PhoneNo1 ,PhoneNo2 ,Gender ,TrainerPassword , AboutMe , MinPricePerHour , DateOfBirth, Photo ,Rate ) ";
        command = prefix + sb.ToString();

        return command;
    }
    public Trainer insertTrainer(Trainer trainer)
    {
        int numEffected = 0;
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandTrainer(trainer);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            numEffected = cmd.ExecuteNonQuery(); // execute the command

        }
        catch (Exception ex)
        {
            //return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        trainer.TrainerCode = findTrainerCode(trainer);
        return trainer;
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandTrainerQualification(TrainerQualification tQ)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0}, {1} ,{2}, '{3}', '{4}' )", tQ.TrainerCode.ToString(), tQ.QualificationTypeCode.ToString(), tQ.PopulationCode.ToString(), tQ.DocumentPath, tQ.FromDate);
        String prefix = "INSERT INTO SM_TrainerQualification" + "(TrainerCode , QualificationTypeCode ,PopulationCode , DocumentPath, FromDate ) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int insertTrainerQualification(TrainerQualification[] tQ)
    {

        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in tQ)
        {
            String cStr = BuildInsertCommandTrainerQualification(item);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandTrainerArea(TrainerArea area)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' )", area.TrainerCode.ToString(), area.AreaCode.ToString());
        String prefix = "INSERT INTO SM_TrainerArea" + "(TrainerCode , AreaCode) ";
        command = prefix + sb.ToString();

        return command;
    }
    public int insertTrainerArea(TrainerArea[] area)
    {

        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in area)
        {
            String cStr = BuildInsertCommandTrainerArea(item);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }


    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandTrainerLanguage(TrainerLanguage lan)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' )", lan.LCode.ToString(), lan.TrainerCode.ToString());
        String prefix = "INSERT INTO SM_LanguageTrainer" + "(LCode , TrainerCode) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int insertTrainerLanguage(TrainerLanguage[] lan)
    {

        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in lan)
        {
            String cStr = BuildInsertCommandTrainerLanguage(item);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandRequest(RequestForReplacement r)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}' ,'{4}', '{5}' ,'{6}', '{7}' ,'{8}', '{9}' ,'{10}', '{11}' ,'{12}')", r.PublishDateTime, r.ContactName, r.BranchCode, r.ClassTypeCode, r.FromHour, r.ToHour, r.ReplacementDate, r.ClassDescription, r.Comments, r.DifficultyLevelCode, r.MaxPrice, r.LanguageCode, r.PopulationCode);
        String prefix = "INSERT INTO SM_RequestForReplacment" + "(PublishDateTime , ContactName , BranchCode , ClassTypeCode ,FromHour ,ToHour ,ReplacmentDate , ClassDecription ,Comments ,DifficultyLevelCode ,MaxPrice ,LanguageLCode ,PopulationCode )";
        command = prefix + sb.ToString();

        return command;
    }

    private int findRequestCode(RequestForReplacement r)
    {
        SqlConnection con = null;
        int ReplacmentCode = 0;
        try
        {
            con = connect("DB7");
            String selectSTR = "select max(ReplacmentCode) as ReplacmentCode from SM_RequestForReplacment where BranchCode='" + r.BranchCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                ReplacmentCode = Convert.ToInt32(dr["ReplacmentCode"]);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return ReplacmentCode;
    }

    //private String BuildInsertRequsrtTrainerCommandRequest(RequestTrainer r)
    //{
    //    String command;

    //    StringBuilder sb = new StringBuilder();
    //    // use a string builder to create the dynamic string
    //    sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}')", r.RequestCode, r.TrainerCode, r.IsApprovedByTrainer, r.RequestStatus);
    //    String prefix = "INSERT INTO SM_RequestForReplacmentTrainer" + "(RequestCode , TrainerCode , IsApprovedByTrainer , RequestStatus )";
    //    command = prefix + sb.ToString();

    //    return command;
    //}

    private String BuildInsertRequsrtTrainerCommandRequest(RequestTrainer r)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}')", r.RequestCode, r.TrainerCode, r.IsApprovedByTrainer, r.RequestStatus);
        String prefix = "INSERT INTO SM_RequestForReplacmentTrainer" + "(RequestCode , TrainerCode ,IsApprovedByTrainer, RequestStatus )";
        command = prefix + sb.ToString();

        return command;
    }

    public int insertRequestTrainer(RequestTrainer[] r)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;
        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in r)
        {
            String cStr = BuildInsertRequsrtTrainerCommandRequest(item);// helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }
        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }


    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandLinksTrainer(LinksTrainer lt)
    {
        String command;
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0}, {1} ,{2})", lt.LinkCode, lt.TrainerCode, lt.Link);
        String prefix = "INSERT INTO SM_LinksTrainer" + "(LinkCode , TrainerCode ,Link) ";
        command = prefix + sb.ToString();
        return command;
    }
    public int insertTrainerLinks(LinksTrainer[] LT)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;
        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in LT)
        {
            String cStr = BuildInsertCommandLinksTrainer(item);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }
        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }
    public RequestForReplacement insertRequest(RequestForReplacement r)
    {
        // int requestId;
        int numEffected = 0;
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandRequest(r);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            // requestId = (Int32)cmd.ExecuteScalar();
            numEffected = cmd.ExecuteNonQuery(); // execute the command

        }
        catch (Exception ex)
        {
            //return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        r.ReplacementCode = findRequestCode(r);
        // return r.ReplacementCode;
        return r;
    }

    //static public int insertRequest(RequestForReplacement r, string connString)
    //{
    //    Int32 requestId = 0;
    //    string sql =
    //      "INSERT INTO SM_RequestForReplacment" + "(PublishDateTime, ContactName, BranchCode, ClassTypeCode, FromHour, ToHour, ReplacmentDate, ClassDecription, Comments, DifficultyLevelCode, MaxPrice, LanguageLCode, PopulationCode) Values(@r.PublishDateTime, r.ContactName, r.BranchCode, r.ClassTypeCode, r.FromHour, r.ToHour, r.ReplacementDate, r.ClassDescription, r.Comments, r.DifficultyLevelCode, r.MaxPrice, r.LanguageCode, r.PopulationCode)"
    //  + "SELECT CAST(scope_identity() AS int)";
    //    using (SqlConnection conn = new SqlConnection(connString))
    //    {
    //        SqlCommand cmd = new SqlCommand(sql, conn);
    //        cmd.Parameters.Add("@Name", SqlDbType.VarChar);
    //        cmd.Parameters["@name"].Value = newName;
    //        try
    //        {
    //            conn.Open();
    //            requestId = (Int32)cmd.ExecuteScalar();
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //        }
    //    }
    //    return (int)requestId;
    //}

    private String BuildInsertCommandParameter(Parameter p)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}')", p.Pname);
        String prefix = "INSERT INTO SM_Parameters" + "(ParameterName) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int InsertParameter(Parameter parameter)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandParameter(parameter);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }


    private String BuildInsertCommandBranchParameter(BranchParameter bp)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}' , '{1}' , '{2}')", bp.BranchCode.ToString(), bp.ParameterCode.ToString(), bp.ParameterWeight.ToString());
        String prefix = "INSERT INTO SM_ParametersBrnach" + "(BranchCode , ParameterCode , ParameterWeight) ";
        command = prefix + sb.ToString();

        return command;
    }
    public int InsertBranchParameter(BranchParameter[] Bparameter)
    {

        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in Bparameter)
        {
            float weight = 100 / item.ParameterWeight;
            item.ParameterWeight = weight;
            String cStr = BuildInsertCommandBranchParameter(item);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }


    private String BuildUpdateCommandBranchParameter(BranchParameter b)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "UPDATE SM_ParametersBrnach Set ParameterWeight='" + b.ParameterWeight + "' WHERE BranchCode='" + b.BranchCode + "'" + "AND ParameterCode = '" + b.ParameterCode + "'";
        command = prefix + sb.ToString();
        return command;
    }
    public int UpdateBranchParameter(BranchParameter[] b)
    {

        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;
        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in b)
        {
            String cStr = BuildUpdateCommandBranchParameter(item); // helper method to build the insert string
            cmd = CreateCommand(cStr, con);
            try
            {
                numEffected = cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
                // write to log
                throw (ex);
            }
        }
        // create the command 
        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }

    private String BuildInsertCommandLinksTo(LinksTo lt)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}' , '{1}' , '{2}')", lt.BranchCode.ToString(), lt.LinkName, lt.LinkCode.ToString());
        String prefix = "INSERT INTO SM_LinksTo" + "(BranchCode , Link , LinkCode) ";
        command = prefix + sb.ToString();

        return command;
    }

    public List<LinksTrainer> GetTrainerLinks(string trainerCode)
    {
        List<LinksTrainer> LinksTrainerList = new List<LinksTrainer>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT lt.*, l.LinkName FROM SM_LinksTrainer lt inner join SM_Links l on lt.LinkCode = l.LinkCode WHERE lt.TrainerCode='" + trainerCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                LinksTrainer lt = new LinksTrainer();

                lt.LinkCode = Convert.ToInt32(dr["LinkCode"]);
                lt.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                lt.Link = (string)dr["Link"];
                lt.LinkName = (string)dr["LinkName"];


                LinksTrainerList.Add(lt);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return LinksTrainerList;
    }


    public int insertLinksTo(LinksTo[] LT)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;
        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in LT)
        {
            String cStr = BuildInsertCommandLinksTo(item);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }
        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    public List<Area> getAreas()
    {
        List<Area> areasList = new List<Area>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM SM_Area";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Area a = new Area();

                a.AreaCode = Convert.ToInt32(dr["AreaCode"]);
                a.AreaName = (string)dr["AreaName"];

                areasList.Add(a);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return areasList;
    }

    public List<Link> getLinks()
    {
        List<Link> linksList = new List<Link>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM SM_Links";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Link l = new Link();

                l.LinkCode = Convert.ToInt32(dr["LinkCode"]);
                l.LinkName = (string)dr["LinkName"];

                linksList.Add(l);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return linksList;
    }


    public List<Company> getCompany()
    {
        List<Company> companyList = new List<Company>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT * FROM SM_Company";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Company c = new Company();
                c.CompanyNo = Convert.ToInt32(dr["CompanyNo"]);
                c.Name = (string)dr["CompanyName"];
                c.Logo = (string)dr["Logo"];
                companyList.Add(c);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return companyList;
    }
    public User getUsers(string email)
    {
        User u = new User();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select distinct TrainerCode as Code, Email, TrainerPassword as 'Password', 'Trainer' as Type from SM_Trainer where Email = '" + email + "' UNION select distinct BranchCode as Code, Email, BranchPassword as 'Password' , 'Branch' as Type from SM_Branch where Email = '" + email + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                u.Code = Convert.ToInt32(dr["Code"]);
                u.Email = (string)dr["Email"];
                u.Password = (string)dr["Password"];
                u.Type = (string)dr["Type"];

            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return u;
    }

    public List<Qualification> getQualification()
    {
        List<Qualification> qualList = new List<Qualification>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from SM_Qualification";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Qualification q = new Qualification();

                q.TypeCode = Convert.ToInt32(dr["TypeCode"]);
                q.TypeName = (string)dr["TypeName"];
                //q.Description = (string)dr["QualificationDescription"];

                qualList.Add(q);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return qualList;
    }

    public List<Population> getPopulation()
    {
        List<Population> popList = new List<Population>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from SM_Population";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Population p = new Population();

                p.Code = Convert.ToInt32(dr["Code"]);
                p.PName = (string)dr["PName"];

                popList.Add(p);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return popList;
    }

    public List<Language> getLanguage()
    {
        List<Language> langList = new List<Language>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from SM_Language";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Language l = new Language();

                l.LanCode = Convert.ToInt32(dr["LCode"]);
                l.LanName = (string)dr["LName"];

                langList.Add(l);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return langList;
    }

    public List<DifficultyLevel> getDifficultyLevel()
    {
        List<DifficultyLevel> DLList = new List<DifficultyLevel>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from SM_DifficultyLevel";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                DifficultyLevel dl = new DifficultyLevel();

                dl.LevelCode = Convert.ToInt32(dr["LevelCode"]);
                dl.LevelName = (string)dr["LevelName"];

                DLList.Add(dl);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return DLList;
    }

    public DataTable GetParameter()
    {
        SqlConnection con = null;

        try
        {
            con = connect("DB7");
            da = new SqlDataAdapter("select * from SM_Parameters", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return dt;
    }

    public List<Parameter> GetParameterBranchList()
    {
        List<Parameter> ParametersList = new List<Parameter>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM SM_Parameters";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Parameter a = new Parameter();

                a.Pcode = Convert.ToInt32(dr["ParameterCode"]);
                a.Pname = (string)dr["ParameterName"];

                ParametersList.Add(a);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return ParametersList;
    }
    public List<TrainerArea> GetTrainerArea(string trainerCode)
    {
        List<TrainerArea> trainerAreaList = new List<TrainerArea>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT ta.*, a.AreaName FROM SM_TrainerArea ta inner join SM_Area a on ta.AreaCode=a.AreaCode WHERE ta.TrainerCode='" + trainerCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                TrainerArea ta = new TrainerArea();
                ta.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                ta.AreaCode = Convert.ToInt32(dr["AreaCode"]);
                ta.AreaName = (string)dr["AreaName"];
                trainerAreaList.Add(ta);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return trainerAreaList;
    }

    public List<TrainerLanguage> GetTrainerLang(string trainerCode)
    {
        List<TrainerLanguage> trainerLangList = new List<TrainerLanguage>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT lt.*, l.LName FROM SM_LanguageTrainer lt inner join SM_Language l on lt.LCode=l.LCode WHERE lt.TrainerCode='" + trainerCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                TrainerLanguage tl = new TrainerLanguage();
                tl.LCode = Convert.ToInt32(dr["LCode"]);
                tl.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                tl.LName = (string)dr["LName"];
                trainerLangList.Add(tl);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return trainerLangList;
    }

    public List<TrainerQualification> GetTrainerQual(string trainerCode)
    {
        List<TrainerQualification> trainerQualList = new List<TrainerQualification>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select tq.*, q.TypeName, p.PName, DATEDIFF(DAY, convert(date, FromDate, 102),GETDATE())/365 as 'YearsOfExperience' from SM_TrainerQualification tq inner join SM_Qualification q on tq.QualificationTypeCode = q.TypeCode inner join SM_Population p on tq.PopulationCode = p.Code WHERE tq.TrainerCode='" + trainerCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                TrainerQualification tq = new TrainerQualification();
                tq.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                tq.QualificationTypeCode = Convert.ToInt32(dr["QualificationTypeCode"]);
                tq.FromDate = (string)dr["FromDate"];
                tq.PopulationCode = Convert.ToInt32(dr["PopulationCode"]);
                tq.DocumentPath = (string)dr["DocumentPath"];
                tq.TypeName = (string)dr["TypeName"];
                tq.PName = (string)dr["PName"];
                tq.YearsOfExperience = Convert.ToInt32(dr["YearsOfExperience"]);
                trainerQualList.Add(tq);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return trainerQualList;
    }

    public List<Trainer> GetTrainerList()
    {
        List<Trainer> TrainerList = new List<Trainer>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select *,case when datalength(Photo)= 0 or Photo is null then 'https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQsyuHcX3SjjrV1IDflNLxre1r2b-jqFKefrWEQpOnUsbbajv1J&usqp=CAU' else Photo end as 'Image' from SM_Trainer";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Trainer t = new Trainer();
                t.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                t.FirstName = (string)dr["FirstName"];
                t.LastName = (string)dr["LastName"];
                t.Email = (string)dr["Email"];
                t.Phone1 = (string)dr["PhoneNo1"];
                t.Phone2 = (string)dr["PhoneNo2"];
                t.Gender = (string)dr["Gender"];
                t.Password = (string)dr["TrainerPassword"];
                t.AboutMe = (string)dr["AboutMe"];
                t.PricePerHour = Convert.ToInt32(dr["MinPricePerHour"]);
                t.DateOfBirth = (string)dr["DateOfBirth"];
                t.Image = (string)dr["Image"];
                TrainerList.Add(t);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return TrainerList;
    }

    public Trainer GetTrainer(string TrainerCode)
    {
        Trainer t = new Trainer();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select t.*,case when datalength(Photo)= 0 or Photo is null then 'https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQsyuHcX3SjjrV1IDflNLxre1r2b-jqFKefrWEQpOnUsbbajv1J&usqp=CAU' else Photo end as 'Image', DATEDIFF(DAY, convert(date, DateOfBirth, 102),GETDATE())/365 as Age from SM_Trainer t WHERE TrainerCode='" + TrainerCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                t.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                t.FirstName = (string)dr["FirstName"];
                t.LastName = (string)dr["LastName"];
                t.Email = (string)dr["Email"];
                t.Phone1 = (string)dr["PhoneNo1"];
                t.Phone2 = (string)dr["PhoneNo2"];
                t.Gender = (string)dr["Gender"];
                t.Password = (string)dr["TrainerPassword"];
                t.AboutMe = (string)dr["AboutMe"];
                t.PricePerHour = Convert.ToInt32(dr["MinPricePerHour"]);
                t.DateOfBirth = (string)dr["DateOfBirth"];
                t.Image = (string)dr["Image"];
                t.Age = Convert.ToInt32(dr["Age"]);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return t;
    }

    private float RatingMatch(int replacementCode, int TrainerCode, string Gender, int PricePerHour, float Rate, int branchCode, int classTypeCode)
    {
        SqlConnection con = null;
        float matchRating = 0;
        try
        {
            con = connect("DB7");
            String selectSTR = @"select CASE WHEN BP.ParameterCode = 5 THEN((BP.ParameterWeight*0.01)*(select(select re.MaxPrice from SM_RequestForReplacment re where re.ReplacmentCode='" + replacementCode + "' ) - (select t.MinPricePerHour from SM_Trainer t where t.TrainerCode = '" + TrainerCode + "' ))) WHEN BP.ParameterCode = 6 and(BP.ParameterWeight * 0.01) * (DATEDIFF (year, (select TQ.FromDate from SM_TrainerQualification TQ where TQ.TrainerCode = '" + TrainerCode + "'  and TQ.QualificationTypeCode = '" + classTypeCode + "'), (select convert(varchar(10), getDate(), 120)))) <= 0 THEN(BP.ParameterWeight * 0.1) WHEN BP.ParameterCode = 6 and(BP.ParameterWeight * 0.01) * (DATEDIFF(year, (select TQ.FromDate from SM_TrainerQualification TQ where TQ.TrainerCode = '" + TrainerCode + "'  and TQ.QualificationTypeCode = '" + classTypeCode + "'), (select convert(varchar(10), getDate(), 120))) )>= 10 THEN(BP.ParameterWeight * 1) WHEN BP.ParameterCode = 6 and(BP.ParameterWeight * 0.01) * (DATEDIFF (year, (select TQ.FromDate from SM_TrainerQualification TQ where TQ.TrainerCode = '" + TrainerCode + "'  and TQ.QualificationTypeCode = '" + classTypeCode + "'), (select convert(varchar(10), getDate(), 120)))) BETWEEN 0 and 10 THEN(select DATEDIFF(year, (select TQ.FromDate from SM_TrainerQualification TQ where TQ.TrainerCode = '" + TrainerCode + "'  and TQ.QualificationTypeCode = '" + classTypeCode + "'), (select convert(varchar(10), getDate(), 120)))*(BP.ParameterWeight * 0.1) as aa) WHEN BP.ParameterCode = 8 THEN((BP.ParameterWeight * 0.01)*(select T.Rate from SM_Trainer T where t.TrainerCode = '" + TrainerCode + "'  )) WHEN BP.ParameterCode = 7 THEN((BP.ParameterWeight * 0.01) * 100) WHEN BP.ParameterCode = 9 THEN((BP.ParameterWeight * 0.01) * 100) ELSE 1 END AS MatchRate from SM_RequestForReplacment R inner join SM_ParametersBrnach BP on R.BranchCode = BP.BranchCode where R.ReplacmentCode = '" + replacementCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                matchRating += (float)Convert.ToDouble(dr["MatchRate"]);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return matchRating;
    }
    public List<Match> GetMatchTrainerList(int replacementCode, int branchCode, int classTypeCode, int maxPrice, int languageCode, int populationCode)
    {
        List<Match> TrainerList = new List<Match>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = @"select *,case when datalength(Photo)= 0 or Photo is null then 'https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQsyuHcX3SjjrV1IDflNLxre1r2b-jqFKefrWEQpOnUsbbajv1J&usqp=CAU' else Photo end as 'Image',
                                case when datalength(Rate)= 0 or Rate is null then '0' else Rate end as 'Rate' from SM_Trainer T inner join SM_TrainerQualification TQ on T.TrainerCode = TQ.TrainerCode 
                                inner join SM_LanguageTrainer LT on T.TrainerCode = LT.TrainerCode 
                                inner join SM_TrainerArea TA on T.TrainerCode = TA.TrainerCode 
                                where TQ.QualificationTypeCode = '" + classTypeCode + "'  " +
                                "and T.MinPricePerHour <= '" + maxPrice + "'  and LT.LCode = '" + languageCode + "'  " +
                                "and TQ.PopulationCode = '" + populationCode + "' " +
                                "and TA.AreaCode = (select AreaCode from SM_Branch B where b.BranchCode = '" + branchCode + "')";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Match t = new Match();
                t.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                t.FirstName = (string)dr["FirstName"];
                t.LastName = (string)dr["LastName"];
                t.Email = (string)dr["Email"];
                t.Phone1 = (string)dr["PhoneNo1"];
                t.Phone2 = (string)dr["PhoneNo2"];
                t.Gender = (string)dr["Gender"];
                t.Password = (string)dr["TrainerPassword"];
                t.AboutMe = (string)dr["AboutMe"];
                t.PricePerHour = Convert.ToInt32(dr["MinPricePerHour"]);
                t.DateOfBirth = (string)dr["DateOfBirth"];
                t.Rate = (float)Convert.ToDouble(dr["Rate"]);
                t.MatchRating = RatingMatch(replacementCode, t.TrainerCode, t.Gender, t.PricePerHour, t.Rate, branchCode, classTypeCode);
                t.Image = (string)dr["Photo"];
                t.RequestCode = replacementCode;


                TrainerList.Add(t);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return TrainerList;
    }
    public List<BranchParameter> GetBranchParameterList(int BranchCode)
    {
        List<BranchParameter> ParametersBranchList = new List<BranchParameter>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT pb.*,p.ParameterName FROM SM_ParametersBrnach pb inner join SM_Parameters p on pb.ParameterCode = p.ParameterCode WHERE pb.BranchCode='" + BranchCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                BranchParameter a = new BranchParameter();
                a.BranchCode = Convert.ToInt32(dr["BranchCode"]);
                a.ParameterCode = Convert.ToInt32(dr["ParameterCode"]);
                a.ParameterWeight = (float)Convert.ToDouble(dr["ParameterWeight"]);
                a.ParameterName1 = (string)dr["ParameterName"];
                ParametersBranchList.Add(a);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return ParametersBranchList;
    }
    public List<Branch> GetBranchList()
    {
        List<Branch> BranchList = new List<Branch>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select b.*, c.CompanyName, c.Logo, a.AreaName from SM_Branch b inner join SM_Company c on b.CompanyNo = c.CompanyNo inner join SM_Area a on b.AreaCode = a.AreaCode";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Branch b = new Branch();
                b.BranchCode = Convert.ToInt32(dr["BranchCode"]);
                b.Name = (string)dr["BranchName"];
                b.Address = (string)dr["BranchAddress"];
                b.PhoneNo = (string)dr["PhoneNo"];
                b.Email = (string)dr["Email"];
                b.Description = (string)dr["BranchDescription"];
                b.CompanyNo = Convert.ToInt32(dr["CompanyNo"]);
                b.Password = (string)dr["BranchPassword"];
                b.AreaCode = Convert.ToInt32(dr["AreaCode"]);
                b.CompanyName = (string)dr["CompanyName"];
                b.AreaName = (string)dr["AreaName"];
                b.Logo = (string)dr["Logo"];

                BranchList.Add(b);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return BranchList;
    }

    public Branch GetBranch(string branchCode)
    {
        Branch b = new Branch();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select b.*, c.CompanyName, c.Logo, a.AreaName from SM_Branch b inner join SM_Company c on b.CompanyNo = c.CompanyNo inner join SM_Area a on b.AreaCode = a.AreaCode where b.BranchCode= '" + branchCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                b.BranchCode = Convert.ToInt32(dr["BranchCode"]);
                b.Name = (string)dr["BranchName"];
                b.Address = (string)dr["BranchAddress"];
                b.PhoneNo = (string)dr["PhoneNo"];
                b.Email = (string)dr["Email"];
                b.Description = (string)dr["BranchDescription"];
                b.CompanyNo = Convert.ToInt32(dr["CompanyNo"]);
                b.Password = (string)dr["BranchPassword"];
                b.AreaCode = Convert.ToInt32(dr["AreaCode"]);
                b.CompanyName = (string)dr["CompanyName"];
                b.AreaName = (string)dr["AreaName"];
                b.Logo = (string)dr["Logo"];

            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return b;
    }

    public List<LinksTo> GetBranchLinks(string BranchCode)
    {
        List<LinksTo> linksToList = new List<LinksTo>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM SM_LinksTo WHERE BranchCode='" + BranchCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                LinksTo lt = new LinksTo();

                lt.BranchCode = Convert.ToInt32(dr["BranchCode"]);
                lt.LinkName = (string)dr["Link"];
                lt.LinkCode = Convert.ToInt32(dr["LinkCode"]);


                linksToList.Add(lt);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return linksToList;
    }

    public List<Branch> GetBranchesList(int branchCode)
    {
        List<Branch> BranchList = new List<Branch>();
        SqlConnection con = null;

        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM SM_Branch where BranchCode=" + '"' + branchCode + '"';
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Branch b = new Branch();
                b.BranchCode = Convert.ToInt32(dr["BranchCode"]);
                b.Name = (string)dr["BranchName"];
                b.Address = (string)dr["BranchAddress"];
                b.PhoneNo = (string)dr["PhoneNo"];
                b.Email = (string)dr["Email"];
                b.Description = (string)dr["BranchDescription"];
                b.CompanyNo = Convert.ToInt32(dr["CompanyNo"]);
                b.Password = (string)dr["BranchPassword"];
                b.AreaCode = Convert.ToInt32(dr["AreaCode"]);

                BranchList.Add(b);
            }

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
        return BranchList;
    }

    public List<RequestDetails> GetBranchRequests(string branchCode)
    {
        List<RequestDetails> branchRequests = new List<RequestDetails>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            string selectSTR = @"select c.CompanyName,c.Logo, r.ReplacmentCode, r.ContactName, r.BranchCode, q.TypeName, r.ClassDecription, r.Comments, d.LevelName, r.MaxPrice, l.LName,p.PName, r.PublishDateTime, r.FromHour, r.ToHour,r.ReplacmentDate,case when DATEDIFF(DAY,CONVERT(date,ReplacmentDate,102),GETDATE())>0 then 1 else 0 end as 'isHistory', rt.TrainerCode, rt.IsApprovedByTrainer, rt.RequestStatus
                                from SM_Company c inner join SM_Branch b on c.CompanyNo = b.CompanyNo inner join SM_RequestForReplacment r on b.BranchCode = r.BranchCode inner join SM_RequestForReplacmentTrainer rt on r.ReplacmentCode = rt.RequestCode
                                inner join SM_Qualification q on r.ClassTypeCode = q.TypeCode inner join SM_DifficultyLevel d on r.DifficultyLevelCode = d.LevelCode
                                inner join SM_Language l on r.LanguageLCode = l.LCode inner join SM_Population p on r.PopulationCode = p.Code
                                where r.BranchCode='" + branchCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                RequestDetails r = new RequestDetails();
                r.CompanyName = (string)dr["CompanyName"];
                r.Logo = (string)dr["Logo"];
                r.ReplacmentCode = Convert.ToInt32(dr["ReplacmentCode"]);
                r.ContactName = (string)dr["ContactName"];
                r.BranchCode = Convert.ToInt32(dr["BranchCode"]);
                r.TypeName = (string)dr["TypeName"];
                r.ClassDescription = (string)dr["ClassDecription"];
                r.Comments = (string)dr["Comments"];
                r.LevelName = (string)dr["LevelName"];
                r.MaxPrice = Convert.ToInt32(dr["MaxPrice"]);
                r.LName = (string)dr["LName"];
                r.PName = (string)dr["PName"];
                r.PublishDateTime = (string)dr["PublishDateTime"];
                r.FromHour = (string)dr["FromHour"];
                r.ToHour = (string)dr["ToHour"];
                r.ReplacementDate = (string)dr["ReplacmentDate"];
                r.IsHistory = Convert.ToBoolean(dr["isHistory"]);
                r.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                r.IsAprrovedByTrainer = (string)dr["IsApprovedByTrainer"];
                r.RequestStatus = (string)dr["RequestStatus"];

                branchRequests.Add(r);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return branchRequests;
    }

    public List<RequestDetails> GetTrainerRequests(string trainerCode)
    {
        List<RequestDetails> trainerRequests = new List<RequestDetails>();
        SqlConnection con = null;
        try
        {
            con = connect("DB7"); // create a connection to the database using the connection String defined in the web config file
            string selectSTR = @"select c.CompanyName,c.Logo, r.ReplacmentCode, r.ContactName, r.BranchCode, q.TypeName, r.ClassDecription, r.Comments, d.LevelName, r.MaxPrice, l.LName,p.PName, r.PublishDateTime, r.FromHour, r.ToHour,r.ReplacmentDate,case when DATEDIFF(DAY,CONVERT(date,ReplacmentDate,102),GETDATE())>0 then 1 else 0 end as 'isHistory', rt.TrainerCode, rt.IsApprovedByTrainer, rt.RequestStatus
                                from SM_Company c inner join SM_Branch b on c.CompanyNo = b.CompanyNo inner join SM_RequestForReplacment r on b.BranchCode = r.BranchCode inner join SM_RequestForReplacmentTrainer rt on r.ReplacmentCode = rt.RequestCode
                                inner join SM_Qualification q on r.ClassTypeCode = q.TypeCode inner join SM_DifficultyLevel d on r.DifficultyLevelCode = d.LevelCode
                                inner join SM_Language l on r.LanguageLCode = l.LCode inner join SM_Population p on r.PopulationCode = p.Code
                                where rt.TrainerCode='" + trainerCode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                RequestDetails r = new RequestDetails();
                r.CompanyName = (string)dr["CompanyName"];
                r.Logo = (string)dr["Logo"];
                r.ReplacmentCode = Convert.ToInt32(dr["ReplacmentCode"]);
                r.ContactName = (string)dr["ContactName"];
                r.BranchCode = Convert.ToInt32(dr["BranchCode"]);
                r.TypeName = (string)dr["TypeName"];
                r.ClassDescription = (string)dr["ClassDecription"];
                r.Comments = (string)dr["Comments"];
                r.LevelName = (string)dr["LevelName"];
                r.MaxPrice = Convert.ToInt32(dr["MaxPrice"]);
                r.LName = (string)dr["LName"];
                r.PName = (string)dr["PName"];
                r.PublishDateTime = (string)dr["PublishDateTime"];
                r.FromHour = (string)dr["FromHour"];
                r.ToHour = (string)dr["ToHour"];
                r.ReplacementDate = (string)dr["ReplacmentDate"];
                r.IsHistory = Convert.ToBoolean(dr["isHistory"]);
                r.TrainerCode = Convert.ToInt32(dr["TrainerCode"]);
                r.IsAprrovedByTrainer = (string)dr["IsApprovedByTrainer"];
                r.RequestStatus = (string)dr["RequestStatus"];

                trainerRequests.Add(r);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return trainerRequests;
    }

    private String BuildUpdateCommandRequestTrainer(RequestTrainer r)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "UPDATE SM_RequestForReplacmentTrainer Set RequestStatus='" + r.RequestStatus + "' WHERE RequestCode='" + r.RequestCode + "'" + "AND TrainerCode = '" + r.TrainerCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int UpdateRequestTrainer(RequestTrainer r)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;
        int x = 0;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandRequestTrainer(r);     // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            numEffected = cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        if (r.RequestStatus == "approved")
        {
            x = UpdateRequestTrainerDecline(r);
        }
        return numEffected;
    }


    private String BuildUpdateCommandRequestTrainerDecline(RequestTrainer r)
    {
        String command;
        string status = "closed";
        StringBuilder sb = new StringBuilder();
        string prefix = "UPDATE SM_RequestForReplacmentTrainer Set RequestStatus='" + status + "' WHERE RequestCode='" + r.RequestCode + "'" + "AND TrainerCode <> '" + r.TrainerCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int UpdateRequestTrainerDecline(RequestTrainer r)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandRequestTrainerDecline(r);     // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private String BuildUpdateCommandIsApprovedTrainer(RequestTrainer r)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "UPDATE SM_RequestForReplacmentTrainer Set IsApprovedByTrainer='" + r.IsApprovedByTrainer + "' WHERE RequestCode='" + r.RequestCode + "'" + "AND TrainerCode = '" + r.TrainerCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int UpdateIsApprovedTrainer(RequestTrainer r)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandIsApprovedTrainer(r);     // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private String BuildUpdateCommandIsApprovedTrainerFalse(RequestTrainer r)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "UPDATE SM_RequestForReplacmentTrainer Set IsApprovedByTrainer='" + r.IsApprovedByTrainer + "', RequestStatus='" + r.RequestStatus + "' WHERE RequestCode='" + r.RequestCode + "'" + "AND TrainerCode = '" + r.TrainerCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int UpdateIsApprovedTrainerFalse(RequestTrainer r)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandIsApprovedTrainerFalse(r);     // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }


    private String BuildDeleteCommandDeleteRequest(RequestTrainer r)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "DELETE from SM_RequestForReplacmentTrainer where RequestCode='" + r.RequestCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int DeleteRequest(RequestTrainer r)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;
        int x = 0;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildDeleteCommandDeleteRequest(r);     // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            numEffected = cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        x = DeleteFullRequest(r);

        return numEffected;
    }

    private String BuildDeleteCommandDeleteFullRequest(RequestTrainer r)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "DELETE from SM_RequestForReplacment where ReplacmentCode='" + r.RequestCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int DeleteFullRequest(RequestTrainer r)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildDeleteCommandDeleteFullRequest(r);     // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }


    private String BuildUpdateCommandUpdateTrainerQualifications(TrainerQualification tq)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "UPDATE SM_TrainerQualification Set PopulationCode='" + tq.PopulationCode + "', DocumentPath='" + tq.DocumentPath + "' , FromDate='" + tq.FromDate + "' WHERE TrainerCode='" + tq.TrainerCode + "'" + "AND QualificationTypeCode = '" + tq.QualificationTypeCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int updateTrainerQualifications(TrainerQualification[] tQ)
    {

        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        foreach (var item in tQ)
        {
            String cStr = BuildUpdateCommandUpdateTrainerQualifications(item);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                numEffected += cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                con.Close();
                // write to log
                throw (ex);
            }
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }

    private String BuildDeleteCommandDeleteTrainerQualifications(TrainerQualification tq)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        string prefix = "DELETE from SM_TrainerQualification where TrainerCode='" + tq.TrainerCode + "'" + "AND QualificationTypeCode = '" + tq.QualificationTypeCode + "'";
        command = prefix + sb.ToString();
        return command;
    }

    public int DeleteTrainerQualifcations(TrainerQualification tq)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB7"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildDeleteCommandDeleteTrainerQualifications(tq);     // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
}



