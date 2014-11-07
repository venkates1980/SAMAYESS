using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Company
/// </summary>
public class Company : MasterBase 
{
	
	  public Int64 CompanyID;
    public String CompanyName;
    public String CompanyAddress = "";
    public String CompanyEMail = "";
    public String CompanyWebsite = "";
    public String SMSTemplate = "";
    public String SMSBalanceTemplate = "";
    public String SMSUserName = "";
    public string SMSPassword = "";
    public string SMSLocalPwd = "";
    public string SenderID = "";
    public string DefaultBackupDrive = "";
    public string BackupPath = "";
    public DateTime DownLoadScheduleTime = System.DateTime.Now;
    public string OPS = "";
    public Double AC01;
    public Double AC10;
    public Double Difference;
    public Double PFCutOffAmount;
    public Double AC02;
    public Double AC21;
    public Double AC22;
    public Boolean PFRoundOff = false;
    public Double ESIEmployee;
    public Double ESIEmployer;
    public Double ESIcutOffAmount;
    public Boolean ESIRoundOff;

    public Boolean EnableSSL = false;
    public String LeaveTemplateHTML = "";
    public String SMTPServer = "";
    public Int32 SMTPPort = 0;
    public String SourceEmail = "";
    public String EmailPwd = "";

    public Char SpliterChar;
    public Int32 AbsentAlertDays;
    public String CompanyPFNo;
    public String PFOffice;
    public String PFOfficeAddress;
    public String PFBankName;
    public String PFBankBranchName;
    public String PFAuthorisedName;
    public String PFAuthorisedDesignation;
    public String PFAuthorisedAddress;
    public String PFAuthorisedPhonoNo;
    public String CompanyESINo;
    public String ESIOffice;
    public String ESIAuthorisedName;
    public String ESIAuthorisedDesignation;
    public String ESIAuthorisedAddress;
    public Boolean SalaryBasedOnLop;

    public Boolean PTIsOnBasic;
    public Boolean PTIsOnAllowance;
    public Boolean PTIsOnOT;
    public Boolean PTIsOnIncentives;
    public Boolean PTIsOnArrears;
    public Boolean PTIsOnOthers;

   // public PictureBox CompanyLogo = new PictureBox();

    public Boolean WHBasedFILO = false;
    public String EmployeeCodePrefixStr = "";
    public Int64 DefaultEmployeeID = 0;

    public string EmailUserName = "";
    public Boolean IsDemoCompany = false;
    public Company()
    {
      LoadHeaders();
    }

    public Company(Int64 ID)
    {
      CompanyID = ID;
      LoadAttributes(ID);
      LoadHeaders();
    }
    ~Company()
    {

      GC.SuppressFinalize(this);

    }
    public void LoadHeaders()
    {
      EntryHeader = "Company";
      EntryDescription = "";
     
    }

    public override void LoadAttributes(long ID)
    {
      try
      {
          SqlConn = GlobalFunctions.GetConnection();
          if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
          //venkat
       //StrSql = "SELECT * FROM SAMAY..TK_COMPANY WHERE COMPANYID = " + ID;
          StrSql = "SELECT * from SamayGPL..TK_Company WHERE COMPANYID = " + ID;
        SqlDataReader DR;
        SqlCmd = new SqlCommand(StrSql,SqlConn );

          DR = SqlCmd.ExecuteReader();
        if (DR.Read())
        {
          CompanyID = Convert.ToInt64(DR["COMPANYID"]);
          CompanyName = Convert.ToString(DR["COMPANYNAME"]);
          CompanyAddress = Convert.ToString(DR["COMPANYADDRESS"]);
          CompanyEMail = Convert.ToString(DR["COMPANYEMAIL"]);
          CompanyWebsite = Convert.ToString(DR["COMPANYWEBSITE"]);
          SpliterChar = Convert.ToChar(DR["SPLITERCHAR"]);
          SMSTemplate = Convert.ToString(DR["SMSTEMPLATE"]);
          SMSBalanceTemplate = Convert.ToString(DR["BALANCETEMPLATE"]);
          SMSUserName = Convert.ToString(DR["SMSUSERNAME"]);
          SMSPassword = Convert.ToString(DR["SMSPASSWORD"]);
          SenderID = Convert.ToString(DR["SENDERID"]);
          if (DR["DOWNLOADSCHEDULETIME"] != DBNull.Value)
          {
            DownLoadScheduleTime = Convert.ToDateTime(DR["DOWNLOADSCHEDULETIME"]);
          }
          AC01 = Convert.ToDouble(DR["AC01"]);
          AC10 = Convert.ToDouble(DR["AC10"]);
          Difference = Convert.ToDouble(DR["DIFFERENCE"]);
          PFCutOffAmount = Convert.ToDouble(DR["PFCUTOFFAMOUNT"]);
          AC02 = Convert.ToDouble(DR["AC02"]);
          AC21 = Convert.ToDouble(DR["AC21"]);
          AC22 = Convert.ToDouble(DR["AC22"]);
          PFRoundOff = Convert.ToBoolean(DR["PFROUNDOFF"]);
          ESIEmployee = Convert.ToDouble(DR["ESIEMPLOYEE"]);
          ESIEmployer = Convert.ToDouble(DR["ESIEMPLOYER"]);
          ESIcutOffAmount = Convert.ToDouble(DR["ESICUTOFFAMOUNT"]);
          ESIRoundOff = Convert.ToBoolean(DR["ESIROUNDOFF"]);
          AbsentAlertDays = Convert.ToInt32(DR["ABSENTALERTDAYS"]);
          CompanyPFNo = Convert.ToString(DR["COMPANYPFNO"]);
          PFOffice = Convert.ToString(DR["PFOFFICE"]);
          PFOfficeAddress = Convert.ToString(DR["PFOFFICEADDRESS"]);
          PFBankName = Convert.ToString(DR["PFBANKNAME"]);
          PFBankBranchName = Convert.ToString(DR["PFBANKBRANCHNAME"]);
          PFAuthorisedName = Convert.ToString(DR["PFAUTHORISEDNAME"]);
          PFAuthorisedDesignation = Convert.ToString(DR["PFAUTHORISEDDESIGNATION"]);
          PFAuthorisedAddress = Convert.ToString(DR["PFAUTHORISEDADDRESS"]);
          PFAuthorisedPhonoNo = Convert.ToString(DR["PFAUTHORISEDPHONENO"]);
          CompanyESINo = Convert.ToString(DR["COMPANYESINO"]);
          ESIOffice = Convert.ToString(DR["ESIOFFICE"]);
          ESIAuthorisedName = Convert.ToString(DR["ESIAUTHORISEDNAME"]);
          ESIAuthorisedDesignation = Convert.ToString(DR["ESIAUTHORISEDDESIGNATION"]);
          ESIAuthorisedAddress = Convert.ToString(DR["ESIAUTHORISEDADDRESS"]);
          SalaryBasedOnLop = Convert.ToBoolean(DR["SALARYBASEDONLOP"]);
          PTIsOnBasic = Convert.ToBoolean(DR["PTISONBASIC"]);
          PTIsOnAllowance = Convert.ToBoolean(DR["PTISONALLOWANCE"]);
          PTIsOnOT  = Convert.ToBoolean(DR["PTISONOT"]);
          PTIsOnIncentives = Convert.ToBoolean(DR["PTISONINCENTIVES"]);
          PTIsOnArrears = Convert.ToBoolean(DR["PTISONARREARS"]);
          PTIsOnOthers = Convert.ToBoolean(DR["PTISONOTHERS"]);
          WHBasedFILO = Convert.ToBoolean(DR["WHBASEDFILO"]);
          EmployeeCodePrefixStr = Convert.ToString(DR ["EMPLOYEECODEPREFIXSTR"]);
          DefaultEmployeeID  = Convert.ToInt64(DR["DEFAULTEMPLOYEEID"]);
          IsDemoCompany = Convert.ToBoolean(DR["ISDEMOCOMPANY"]);
          EnableSSL = Convert.ToBoolean (DR["ENABLESSL"]);
          SMTPPort = Convert.ToInt32(DR["SMTPPORT"]);
          SMTPServer = Convert.ToString(DR["SMTPSERVER"]);
          SourceEmail = Convert.ToString(DR["SOURCEEMAIL"]);
          EmailPwd = Convert.ToString(DR["EMAILPWD"]);
          LeaveTemplateHTML = Convert.ToString(DR["LEAVETEMPLATEHTML"]);
          EmailUserName = Convert.ToString(DR["EMAILUSERNAME"]);
          //if (!Convert.IsDBNull(DR["COMPANYLOGO"])) { GlobalFunctions.LoadImageWithByte((byte[])DR["COMPANYLOGO"], CompanyLogo); }
        }
        DR.Close();
      }
      catch (Exception E)
      {
       // SamayMsgBox.Show(E.Message);
      }
    }
	}

