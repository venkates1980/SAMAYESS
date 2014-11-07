using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

	
		public class Employee : MasterBase
    {
        public Int64 EmployeeID;
        public String EmployeeCode;
        public String EmployeeFirstName;
        public String EmployeeLastName;
        public Boolean Gender = false ;
        public Int64 SectionID;
        public Int64 DesignationID; 
        public Int64 CategoryID;
        public Int64 GradeID;
      public DateTime DOJ = Convert.ToDateTime("01/01/0001");
      public DateTime DOR = Convert.ToDateTime("01/01/0001");
      public DateTime DOC = Convert.ToDateTime("01/01/0001");
      public DateTime DOB = Convert.ToDateTime("01/01/0001");
      public DateTime DOL = Convert.ToDateTime("01/01/0001");

      public DateTime DOResignation = Convert.ToDateTime("01/01/0001");
      public DateTime DOOnRoll = Convert.ToDateTime("01/01/0001");
      public DateTime DOProbation = Convert.ToDateTime("01/01/0001");
      public DateTime PFEligibilityDate = Convert.ToDateTime("01/01/0001");

      public Boolean PFCutOffApplicable = false;
        public String EmployeeRFID = "";
      //  public String 
        public Int64  EmployeeTypeID;
        public String FatherName = "" ;
        public String MotherName = "";
        public String ResidentialAddress = "";
        public String PermanentAddress = "";
        public String ContactNo = "";
        public String EmailID = "";
        public String MobileNo = "";
        public String PlaceOfBirth = "";
        public String Nomenee1 = "";
        public String Nomenee2 = "";
        public String  BloodGroup= "";
        public String WorkPlace = "";
        public String ExtnNo= "";
        public Boolean MaritalStatus = false ;
        public String Nationality = "";
        public String PANCARDNO = "";
        public Int16 NoOfDependant = 0;

        public String EmrgContactName1 = "", EmrgContactName2="" ;
        public String EmrgContactRelation1="", EmrgContactRelation2="";
        public String EmrgContactNo1="", EmrgContactNo2="";

        public String DependantName1 = "", DependantName2 = "", DependantName3 = "", DependantName4 = "", DependantName5 = "", DependantName6 = "", DependantName7 = "", DependantName8 = "";
        public String DependantRelation1 = "", DependantRelation2 = "", DependantRelation3 = "", DependantRelation4 = "", DependantRelation5 = "", DependantRelation6 = "", DependantRelation7 = "", DependantRelation8 = "";

        public Int64 ReportingEmployeeID1=0, ReportingEmployeeID2=0, ReportingEmployeeID3=0, ReportingEmployeeID4=0;

      


        public DataTable dtReaderInfo=new DataTable ();

   
        public Int64 AttendancePolicyID;
        public Int64 OverTimePolicyID;
        public Int64 LatePolicyID;
        public String FingerPrint1 = ""; 
        public String FingerPrint2 = "";
        public Int64 LocationID;
      public Int64 ShiftGroupID;

      public DayOfWeek WeeklyOff1;

      public Int64 HolidayGroupID;
      public Int64 LeaveGroupID;
        public Boolean Inactive = false;

        public Int64 ShiftID;

        public Int64 TeamID;

      //Payroll Related

      public Int32 PaymentType = 1;
      public String BankAccountNo = "";
      public Int64 BankID;
      public String PFNumber = "";
      public String ESICNumber = "";
      public Int32 PaySlipLanguageID = 1;
      public Int64  PayGroupID;

        public Double Basic=0;
      public Boolean PFRequired=false ;

      public Boolean ESIRequired=false ;

      public Boolean WFRequired=false ;
      public Double WFAmount=0;
      public Double WFPercentage=0;
      public Int64  WFFormulaID;
      public Int64 NormalOTHRSFormulaID;

      public Boolean IncludeWOInPayableDays = false;

      public String RelievedReason = "";
      public Boolean IsRootEmployee = false;
      public Boolean IsPrepaid = false;
      public double  NetBalance = 0;
      public Int64 BaseEmployeeID;
      public string Kpwd = "";
      public string HODDeptIDStr = "";
      public Int32 PerDaySalCalcDays = 0;
//        BioComponent ObjBioComponent = new BioComponent();
      public String CardNumber = "";
      public Boolean PTRequired = true;

      public Double VPFPercentage = 0;
      public Double VPFAmount = 0;

      public Int64 RecruitmentModeID;


      public string PassportNo = "";
      public string VisaInfo = "";
      public Boolean ShiftAllowance = false;
      public Boolean ShiftIncentive = false;

      public Boolean Reporting1IsFinal = false;
      public Boolean Reporting2IsFinal = false;

      public Employee(Int64 ID)
      {
          EmployeeID = ID;
          LoadAttributes(ID);
       
      }
      public override void LoadAttributes(long ID)
      {
          try
          {
              SqlConn = GlobalFunctions.GetConnection();
              if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
              StrSql = "SELECT * FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + ID;
              SqlDataReader DR;
              SqlCmd = new SqlCommand(StrSql,SqlConn );
              DR = SqlCmd.ExecuteReader();
              if (DR.Read())
              {
                  EmployeeID = Convert.ToInt64(DR["EMPLOYEEID"]);
                  EmployeeCode = Convert.ToString(DR["EMPLOYEECODE"]);
                  EmployeeFirstName = Convert.ToString(DR["EMPLOYEEFIRSTNAME"]);
                  EmployeeLastName = Convert.ToString(DR["EMPLOYEELASTNAME"]);
                  Gender = Convert.ToBoolean(DR["GENDER"]);
                  CompanyID = Convert.ToInt64(DR["COMPANYID"]);
                  SectionID = Convert.ToInt64(DR["SECTIONID"]);
                  DesignationID = Convert.ToInt64(DR["DESIGNATIONID"]);
                  CategoryID = Convert.ToInt64(DR["CATEGORYID"]);
                  GradeID = Convert.ToInt64(DR["GRADEID"]);
                  PTRequired = Convert.ToBoolean(DR["PTREQUIRED"]);
                  if (DR["TEAMID"] != DBNull.Value)
                  {
                      TeamID = Convert.ToInt64(DR["TEAMID"]);
                  }

                  if (DR["DOJ"] != DBNull.Value)
                  {
                      DOJ = Convert.ToDateTime(DR["DOJ"]);
                  }
                  if (DR["DOR"] != DBNull.Value)
                  {
                      DOR = Convert.ToDateTime(DR["DOR"]);
                  }

                  if (DR["DOC"] != DBNull.Value)
                  {
                      DOC = Convert.ToDateTime(DR["DOC"]);
                  }

                  if (DR["DOB"] != DBNull.Value)
                  {
                      DOB = Convert.ToDateTime(DR["DOB"]);
                  }

                  if (DR["DORESIGNATION"] != DBNull.Value)
                  {
                      DOResignation = Convert.ToDateTime(DR["DORESIGNATION"]);
                  }

                  if (DR["DOONROLL"] != DBNull.Value)
                  {
                      DOOnRoll = Convert.ToDateTime(DR["DOONROLL"]);
                  }
                  if (DR["DOPROBATION"] != DBNull.Value)
                  {
                      DOProbation = Convert.ToDateTime(DR["DOPROBATION"]);
                  }
                  if (DR["PFELIGIBILITYDATE"] != DBNull.Value)
                  {
                      PFEligibilityDate = Convert.ToDateTime(DR["PFELIGIBILITYDATE"]);
                  }
                  DOL = Convert.ToDateTime(DR["DOL"]);

                 // if (!Convert.IsDBNull(DR["PHOTO"])) { GlobalFunctions.LoadImageWithByte((byte[])DR["PHOTO"], Photo); }
                  if (DR["REPORTINGEMPLOYEEID1"] != DBNull.Value)
                      ReportingEmployeeID1 = Convert.ToInt64(DR["REPORTINGEMPLOYEEID1"]);

                  if (DR["REPORTINGEMPLOYEEID2"] != DBNull.Value)
                      ReportingEmployeeID2 = Convert.ToInt64(DR["REPORTINGEMPLOYEEID2"]);

                  Reporting1IsFinal = Convert.ToBoolean(DR["REPORTING1ISFINAL"]);
                  Reporting2IsFinal = Convert.ToBoolean(DR["REPORTING2ISFINAL"]);
                  LocationID = Convert.ToInt64(DR["LOCATIONID"]);
                  HolidayGroupID = Convert.ToInt64(DR["HOLIDAYGROUPID"]);
                  LeaveGroupID = Convert.ToInt64(DR["LEAVEGROUPID"]);
                  ShiftGroupID = Convert.ToInt64(DR["SHIFTGROUPID"]);
                  EmployeeRFID = Convert.ToString(DR["EMPLOYEERFID"]);
                  EmployeeTypeID = Convert.ToInt64(DR["EMPLOYEETYPEID"]);
                  FatherName = Convert.ToString(DR["FATHERNAME"]);
                  MotherName = Convert.ToString(DR["MOTHERNAME"]);
                  ResidentialAddress = Convert.ToString(DR["RESIDENTIALADDRESS"]);
                  PermanentAddress = Convert.ToString(DR["PERMANENTADDRESS"]);
                  ContactNo = Convert.ToString(DR["CONTACTNO"]);
                  EmailID = Convert.ToString(DR["EMAILID"]);
                  MobileNo = Convert.ToString(DR["MOBILENO"]);
                  PlaceOfBirth = Convert.ToString(DR["PLACEOFBIRTH"]);
                  Nomenee1 = Convert.ToString(DR["NOMENEE1"]);
                  Nomenee2 = Convert.ToString(DR["NOMENEE2"]);
                  BloodGroup = Convert.ToString(DR["BLOODGROUP"]);
                  WorkPlace = Convert.ToString(DR["WORKPLACE"]);
                  ExtnNo = Convert.ToString(DR["EXTNNO"]);
                  MaritalStatus = Convert.ToBoolean(DR["MARITALSTATUS"]);
                  Nationality = Convert.ToString(DR["NATIONALITY"]);
                  PANCARDNO = Convert.ToString(DR["PANCARDNO"]);
                  NoOfDependant = Convert.ToInt16(DR["NOOFDEPENDENT"]);
                  CompanyID = Convert.ToInt64(DR["COMPANYID"]);
                  PlaceOfBirth = Convert.ToString(DR["PLACEOFBIRTH"]);
                  EmrgContactName1 = Convert.ToString(DR["EMRGCONTACTNAME1"]);
                  EmrgContactName2 = Convert.ToString(DR["EMRGCONTACTNAME2"]);
                  EmrgContactRelation1 = Convert.ToString(DR["EMRGCONTACTRELATION1"]);
                  EmrgContactRelation2 = Convert.ToString(DR["EMRGCONTACTRELATION2"]);
                  EmrgContactNo1 = Convert.ToString(DR["EMRGCONTACTNO1"]);
                  EmrgContactNo2 = Convert.ToString(DR["EMRGCONTACTNO2"]);
                  DependantName1 = Convert.ToString(DR["DEPENDANTNAME1"]);
                  DependantName2 = Convert.ToString(DR["DEPENDANTNAME2"]);
                  DependantName3 = Convert.ToString(DR["DEPENDANTNAME3"]);
                  DependantName4 = Convert.ToString(DR["DEPENDANTNAME4"]);
                  DependantName5 = Convert.ToString(DR["DEPENDANTNAME5"]);
                  DependantName6 = Convert.ToString(DR["DEPENDANTNAME6"]);
                  DependantName7 = Convert.ToString(DR["DEPENDANTNAME7"]);
                  DependantName8 = Convert.ToString(DR["DEPENDANTNAME8"]);
                  DependantRelation1 = Convert.ToString(DR["DEPENDANTRELATION1"]);
                  DependantRelation2 = Convert.ToString(DR["DEPENDANTRELATION2"]);
                  DependantRelation3 = Convert.ToString(DR["DEPENDANTRELATION3"]);
                  DependantRelation4 = Convert.ToString(DR["DEPENDANTRELATION4"]);
                  DependantRelation5 = Convert.ToString(DR["DEPENDANTRELATION5"]);
                  DependantRelation6 = Convert.ToString(DR["DEPENDANTRELATION6"]);
                  DependantRelation7 = Convert.ToString(DR["DEPENDANTRELATION7"]);
                  DependantRelation8 = Convert.ToString(DR["DEPENDANTRELATION8"]);
                  ReportingEmployeeID1 = Convert.ToInt64(DR["REPORTINGEMPLOYEEID1"]);
                  ReportingEmployeeID2 = Convert.ToInt64(DR["REPORTINGEMPLOYEEID2"]);
                  ReportingEmployeeID3 = Convert.ToInt64(DR["REPORTINGEMPLOYEEID3"]);
                  ReportingEmployeeID4 = Convert.ToInt64(DR["REPORTINGEMPLOYEEID4"]);
                  AttendancePolicyID = Convert.ToInt64(DR["ATTENDANCEPOLICYID"]);
                  OverTimePolicyID = Convert.ToInt64(DR["OVERTIMEPOLICYID"]);
                  LatePolicyID = Convert.ToInt64(DR["LATEPOLICYID"]);
                  FingerPrint1 = Convert.ToString(DR["FINGERPRINT1"]);
                  FingerPrint2 = Convert.ToString(DR["FINGERPRINT2"]);
                  Inactive = Convert.ToBoolean(DR["INACTIVE"]);
                  RelievedReason = Convert.ToString(DR["RELIEVEDREASON"]);

                  PaymentType = Convert.ToInt32(DR["PAYMENTTYPE"]);
                  BankAccountNo = Convert.ToString(DR["BANKACCOUNTNO"]);

                  if (DR["BANKID"] != DBNull.Value)
                  {
                      BankID = Convert.ToInt64(DR["BANKID"]);
                  }

                  PFNumber = Convert.ToString(DR["PFNUMBER"]);
                  ESICNumber = Convert.ToString(DR["ESICNUMBER"]);
                  PaySlipLanguageID = Convert.ToInt32(DR["PAYSLIPLANGUAGEID"]);
                  if (DR["PAYGROUPID"] != DBNull.Value)
                  {
                      PayGroupID = Convert.ToInt64(DR["PAYGROUPID"]);
                  }
                  Basic = Convert.ToDouble(DR["BASIC"]);
                  PFRequired = Convert.ToBoolean(DR["PFREQUIRED"]);

                  PFCutOffApplicable = Convert.ToBoolean(DR["PFCUTOFFAPPLICABLE"]);

                  ESIRequired = Convert.ToBoolean(DR["ESIREQUIRED"]);

                  WFRequired = Convert.ToBoolean(DR["WFREQUIRED"]);
                  WFAmount = Convert.ToDouble(DR["WFAMOUNT"]);
                  WFPercentage = Convert.ToDouble(DR["WFPERCENTAGE"]);
                  if (DR["WFFORMULAID"] != DBNull.Value)
                  {
                      WFFormulaID = Convert.ToInt64(DR["WFFORMULAID"]);
                  }
                  if (DR["NORMALOTHRSFORMULAID"] != DBNull.Value)
                  {
                      NormalOTHRSFormulaID = Convert.ToInt64(DR["NORMALOTHRSFORMULAID"]);
                  }


                  IncludeWOInPayableDays = Convert.ToBoolean(DR["INCLUDEWOINPAYABLEDAYS"]);

                  BaseEmployeeID = Convert.ToInt64(DR["BASEEMPLOYEEID"]);
                  IsRootEmployee = Convert.ToBoolean(DR["ISROOTEMPLOYEE"]);
                  IsPrepaid = Convert.ToBoolean(DR["ISPREPAID"]);
                  NetBalance = Convert.ToDouble(DR["NETBALANCE"]);
                  PerDaySalCalcDays = Convert.ToInt32(DR["PERDAYSALCALCDAYS"]);
                  HODDeptIDStr = Convert.ToString(DR["HODDEPTIDSTR"]);
                  Kpwd = Convert.ToString(DR["KPWD"]);
                  CardNumber = Convert.ToString(DR["CARDNUMBER"]);

                  VPFAmount = Convert.ToDouble(DR["VPFAMOUNT"]);
                  VPFPercentage = Convert.ToDouble(DR["VPFPERCENTAGE"]);

                  RecruitmentModeID = Convert.ToInt64(DR["RECRUITMENTMODEID"]);

                  ShiftAllowance = Convert.ToBoolean(DR["SHIFTALLOWANCE"]);
                  ShiftIncentive = Convert.ToBoolean(DR["SHIFTINCENTIVE"]);
                  PassportNo = Convert.ToString(DR["PASSPORTNO"]);
                  VisaInfo = Convert.ToString(DR["VISAINFO"]);
              }
              DR.Close();
          }
          catch (Exception E)
          {
             // SamayMsgBox.Show(E.Message + "/Proc : Employee.LoadAttributes ");
          }
      }
	}

