using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default2 : System.Web.UI.Page
{
    GlobalInformation Ginf;
    LeaveTrans Obj = new LeaveTrans();
    MasterBase objmstbase = new MasterBase();
    Int64 EmployeeID;
    DataTable dt;
    string STRBODY;
    private void RefreshData()
    {
        //SqlDataSource sd = new SqlDataSource(ConfigurationSettings.AppSettings["ConnString"],"");
        String StrSql = "Select Leavetypeid, leavetypename FROM TK_LEAVETYPE WHERE COMPANYID =  " + Ginf.CompanyID.ToString();

        ddlLeavetype.DataSource = GlobalFill.FillDataTable(StrSql);
        ddlLeavetype.DataValueField = "leavetypeid";
        ddlLeavetype.DataTextField = "leavetypename";
        ddlLeavetype.DataBind();

        lblAppliedOn.Text = System.DateTime.Now.ToString("ddMMMyyyy");
        //Code Added by kt
        Ginf = new GlobalInformation(this);
        EmployeeID = Convert.ToInt16(Ginf.EmployeeId);
        string StrQry = "select * from TK_CalendarYear where CalendarYearId=" + Convert.ToInt64(Ginf.CalendarYearID);
        DataTable DT = GlobalFill.FillDataTable(StrQry);
        if ((Request.QueryString["Mode"] == "Approve") || (Request.QueryString["Mode"] == "Reject")) { 
        
        }
        else
        {
            if (DT.Rows.Count != 0)
            {
                DateTime dtFromDate = Convert.ToDateTime(DT.Rows[0]["StartingDate"].ToString());
                DateTime dtToDate = Convert.ToDateTime(DT.Rows[0]["EndingDate"].ToString());
                dt = GetEmployeeLeaves(EmployeeID, dtFromDate, dtToDate, "", Convert.ToInt64(Ginf.CalendarYearID));
                gvledger.DataSource = dt;
                gvledger.DataBind();
                Session["LeaveDetails"] = dt;
            }
        }
    }

//    public DataTable GetEmployeeLeaves(Int64 EmployeeId, DateTime FrDate, DateTime ToDate, string FilterStr, Int64 pCalendarYearId)
//    {

//        if (Convert.ToInt64(Ginf.EmployeeId) == 0)
//        {
//            Response.Redirect("~/login.aspx");
//        }
//        string StrQry = "";

//        string stDate = "'" + FrDate.ToString("ddMMMyyyy") + "'";
//        string endDate = "'" + ToDate.ToString("ddMMMyyyy") + "'";


//        StrQry = "SELECT STARTINGDATE FROM TK_CALENDARYEAR WHERE CALENDARYEARID = " + pCalendarYearId;
//        DateTime StartingDate = Convert.ToDateTime(GlobalFill.GetQueryValue(StrQry));
//        StrQry = "SELECT ENDINGDATE FROM TK_CALENDARYEAR WHERE CALENDARYEARID = " + pCalendarYearId;
//        DateTime EndingDate = Convert.ToDateTime(GlobalFill.GetQueryValue(StrQry));


//        StrQry = "DECLARE @FROMDATE DATETIME " +
//                                  "\n " + "DECLARE @TODATE DATETIME " +
//                                  "\n " + " DECLARE @CALENDARYEARSTARTDATE DATETIME " +
//                                  "\n " + " SET @FROMDATE = " + stDate +
//                                 "\n " + " SET @TODATE = " + endDate +
//            //"\n " + " SET @CALENDARYEARSTARTDATE = '" + ObjCal.StartingDate.ToString("ddMMMyyyy") + "'" +
//                                "\n " + " SET @CALENDARYEARSTARTDATE = " + stDate +
//                                "  CREATE TABLE #TMP (EMPLOYEEID BIGINT, LEAVETYPEID INT, PREVOPBAL FLOAT, CURROPBAL FLOAT, ADJ FLOAT, ENCASHED FLOAT,EARNED FLOAT, PREVTAKEN FLOAT, CURRTAKEN FLOAT, CLBAL FLOAT)  " +
//                                "\n " + " INSERT INTO #TMP  " +
//                                "\n " + "   SELECT OP.EMPLOYEEID , LT.LEAVETYPEID, OPBALANCE AS PREVOPBAL, 0,0,0,0,0,0 ,0 " +
//                                "\n " + "  FROM     TK_LEAVEOPBAL OP " +
//                                "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
//                                "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
//                                "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;

//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += " AND FRDATE  < @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID + " AND ISADJUSTED = 0 AND IsEarned=0 AND IsEncashed=0 " +

//                    "\n " + " INSERT INTO #TMP  " +
//                                   "\n " + " SELECT OP.EMPLOYEEID , LT.LEAVETYPEID, OPBALANCE AS CURROPBAL, 0,0,0,0 ,0 ,0,0 " +
//                                   "\n " + "  FROM     TK_LEAVEOPBAL OP " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                     "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";


//        if (EmployeeId > 0)
//            StrQry += "\n " + "  AND OP.EMPLOYEEID = " + EmployeeId;
//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;



//        StrQry += "\n " + " AND FRDATE BETWEEN @FROMDATE AND @TODATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +

//                                "\n " + " AND ISADJUSTED = 0   AND IsEarned=0 AND IsEncashed=0  " +


//                                   "\n " + " INSERT INTO #TMP  " +
//                                   "\n " + " SELECT EM.EMPLOYEEID , LT.LEAVETYPEID, 0, 0,OPBALANCE,0,0 ,0 ,0,0 " +
//                                 "\n " + "  FROM     TK_LEAVEOPBAL OP " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;
//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += "\n " + " AND FRDATE <=  @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +
//                            "\n " + "  AND ISADJUSTED = 1 " +





//                                ///////////////////////////////////////////////////////

//                                   "\n " + " INSERT INTO #TMP  " +
//                                   "\n " + " SELECT EM.EMPLOYEEID , LT.LEAVETYPEID, 0, 0,0,OPBALANCE,0 ,0 ,0,0 " +
//                                 "\n " + "  FROM     TK_LEAVEOPBAL OP " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;

//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += "\n " + " AND FRDATE <=  @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +
//                                "\n " + "  AND IsEncashed = 1 " +

//// for include wo
//                "\n " + " INSERT INTO #TMP  " +
//                                   "\n " + " SELECT EM.EMPLOYEEID , LT.LEAVETYPEID, 0, 0,0,0,OPBALANCE,0 ,0,0 " +
//                                 "\n " + "  FROM     TK_LEAVEOPBAL OP " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";
//        //over
//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;

//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += "\n " + "  AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +
//                                 "\n " + "  AND IsEarned = 1 " +

//                                 //over
//            // for include holiday
//                "\n " + " INSERT INTO #TMP " +
//                                "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0,ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA    " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
//                                "\n " + " WHERE TA.WEEKLYOFF = 0 AND TA.HOLIDAY = 0 AND  ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";

//        //
//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;

//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += " AND ISONLEAVE = 1 " +
//                            "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID  " +


//                            // CURRENT LEAVES

//                             "\n " + " INSERT INTO #TMP " +
//                                "\n " + "   SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA    " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID AND EM.COMPANYID =  " + Ginf.CompanyID +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
//                                "\n " + " WHERE TA.WEEKLYOFF = 1 AND LT.INCLUDEWEEKLYOFF = 1 AND  ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";
//        //
//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += " AND ISONLEAVE = 1 " +
//                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

//// FOR INCLUDE WO
//         "\n " + " INSERT INTO #TMP " +
//                                "\n " + "  SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA  " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID AND EM.COMPANYID =  " + Ginf.CompanyID +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
//                                "\n " + " WHERE TA.holiday = 1 AND LT.INCLUDEHOLIDAY = 1 AND  TA.WEEKLYOFF = 0 AND LT.INCLUDEWEEKLYOFF = 0 AND ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";

//        //
//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += " AND ISONLEAVE = 1 " +
//                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

//// FOR INCLUDE HOLIAY
//   "\n " + " INSERT INTO #TMP " +
//                                "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0, 0,0,ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0 FROM TK_ATTENDANCE TA   " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
//                                 "\n " + " WHERE TA.WEEKLYOFF = 0 AND TA.HOLIDAY = 0 AND   ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;
//        //
//        StrQry += " AND ISONLEAVE = 1 " +
//                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

//// FOR INCLUDE WO
//           "\n " + " INSERT INTO #TMP " +
//                                "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0), 0 FROM TK_ATTENDANCE TA   " +
//                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
//                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
//                                 "\n " + " WHERE TA.WEEKLYOFF = 1 AND LT.INCLUDEWEEKLYOFF = 1 AND   ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
//        //
//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += " AND ISONLEAVE = 1 " +
//                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

//// FOR INCLUDE HOLIAY
//"\n " + " INSERT INTO #TMP " +
//                             "\n " + "  SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0), 0 FROM TK_ATTENDANCE TA  " +
//                              "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
//                              "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
//                              "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
//                              "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
//                              "\n " + " WHERE TA.HOLIDAY = 1 AND LT.INCLUDEHOLIDAY = 1 AND TA.WEEKLYOFF = 0 AND LT.INCLUDEWEEKLYOFF = 0 AND  ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
//        if (EmployeeId > 0)
//            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
//        if (FilterStr != "")
//            StrQry += "\n " + FilterStr;

//        StrQry += " AND ISONLEAVE = 1 " +
//                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

//                             "\n " + " SELECT EMPLOYEEID,LEAVETYPEID , SUM(PREVOPBAL) AS PREVOPBAL , SUM(CURROPBAL) AS CURROPBAL, SUM(ADJ) AS ADJ , sum(ENCASHED) as  ENCASHED,sum(EARNED) as EARNED , SUM(PREVTAKEN) AS PREVTAKEN , SUM(CURRTAKEN) AS CURRTAKEN , SUM(CLBAL)  AS CLBAL  " +
//                             "\n " + "  INTO #TMP1 " +
//                             "\n " + " FROM #TMP GROUP BY EMPLOYEEID ,LEAVETYPEID " +
//                             "\n " + " UPDATE #TMP1 SET  CLBAL = PREVOPBAL + CURROPBAL + ADJ- PREVTAKEN - CURRTAKEN  - ENCASHED + EARNED  " +
//                             "\n " + " SELECT  LT.LEAVETYPENAME AS LEAVETYPE, PREVOPBAL+ CURROPBAL AS OPBAL , T.ADJ AS ADJUSTMENTS,  ENCASHED as  ENCASHED, EARNED as EARNED, PREVTAKEN + CURRTAKEN AS TAKEN , CLBAL   " +
//                             "\n " + " FROM #TMP1 T " +
//                             "\n " + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = T.EMPLOYEEID AND EM.COMPANYID =  " + Ginf.CompanyID +
//                             "\n " + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = T.LEAVETYPEID  " +
//                             "\n " + " ORDER BY T.EMPLOYEEID " +
//                             "\n " + " DROP TABLE #TMP " +
//                             "\n " + " DROP TABLE #TMP1 ";

//        DataTable DT = GlobalFill.FillDataTable(StrQry);
//        return DT;
//    }


    public DataTable GetEmployeeLeaves(Int64 EmployeeId, DateTime FrDate, DateTime ToDate, string FilterStr, Int64 pCalendarYearId)
    {

        if (Convert.ToInt64(Ginf.EmployeeId) == 0)
        {
            Response.Redirect("~/login.aspx");
        }
        string StrQry = "";

        string stDate = "'" + FrDate.ToString("ddMMMyyyy") + "'";
        string endDate = "'" + ToDate.ToString("ddMMMyyyy") + "'";


        StrQry = "SELECT STARTINGDATE FROM TK_CALENDARYEAR WHERE CALENDARYEARID = " + pCalendarYearId;
        DateTime StartingDate = Convert.ToDateTime(GlobalFill.GetQueryValue(StrQry));
        StrQry = "SELECT ENDINGDATE FROM TK_CALENDARYEAR WHERE CALENDARYEARID = " + pCalendarYearId;
        DateTime EndingDate = Convert.ToDateTime(GlobalFill.GetQueryValue(StrQry));


        StrQry = "DECLARE @FROMDATE DATETIME " +
                                  "\n " + "DECLARE @TODATE DATETIME " +
                                  "\n " + " DECLARE @CALENDARYEARSTARTDATE DATETIME " +
                                  "\n " + " SET @FROMDATE = " + stDate +
                                 "\n " + " SET @TODATE = " + endDate +
            //"\n " + " SET @CALENDARYEARSTARTDATE = '" + ObjCal.StartingDate.ToString("ddMMMyyyy") + "'" +
                                "\n " + " SET @CALENDARYEARSTARTDATE = " + stDate +
                                "  CREATE TABLE #TMP (EMPLOYEEID BIGINT, LEAVETYPEID INT, PREVOPBAL FLOAT, CURROPBAL FLOAT, ADJ FLOAT, ENCASHED FLOAT,EARNED FLOAT, PREVTAKEN FLOAT, CURRTAKEN FLOAT, CLBAL FLOAT)  " +
                                "\n " + " INSERT INTO #TMP  " +
                                "\n " + "   SELECT OP.EMPLOYEEID , LT.LEAVETYPEID, OPBALANCE AS PREVOPBAL, 0,0,0,0,0,0 ,0 " +
                                "\n " + "  FROM     TK_LEAVEOPBAL OP " +
                                "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
                                "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
                                "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

        if (EmployeeId > 0)
            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;

        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND FRDATE  < @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID + " AND ISADJUSTED = 0 AND IsEarned=0 AND IsEncashed=0 " +

                    "\n " + " INSERT INTO #TMP  " +
                                   "\n " + " SELECT OP.EMPLOYEEID , LT.LEAVETYPEID, OPBALANCE AS CURROPBAL, 0,0,0,0 ,0 ,0,0 " +
                                   "\n " + "  FROM     TK_LEAVEOPBAL OP " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                     "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";


        if (EmployeeId > 0)
            StrQry += "\n " + "  AND OP.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;



        StrQry += "\n " + " AND FRDATE BETWEEN @FROMDATE AND @TODATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +

                                "\n " + " AND ISADJUSTED = 0   AND IsEarned=0 AND IsEncashed=0  " +


                                   "\n " + " INSERT INTO #TMP  " +
                                   "\n " + " SELECT EM.EMPLOYEEID , LT.LEAVETYPEID, 0, 0,OPBALANCE,0,0 ,0 ,0,0 " +
                                 "\n " + "  FROM     TK_LEAVEOPBAL OP " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

        if (EmployeeId > 0)
            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += "\n " + " AND FRDATE <=  @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +
                            "\n " + "  AND ISADJUSTED = 1 " +





                                ///////////////////////////////////////////////////////

                                   "\n " + " INSERT INTO #TMP  " +
                                   "\n " + " SELECT EM.EMPLOYEEID , LT.LEAVETYPEID, 0, 0,0,OPBALANCE,0 ,0 ,0,0 " +
                                 "\n " + "  FROM     TK_LEAVEOPBAL OP " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

        if (EmployeeId > 0)
            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;

        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += "\n " + " AND FRDATE <=  @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +
                                "\n " + "  AND IsEncashed = 1 " +

// for include wo
                "\n " + " INSERT INTO #TMP  " +
                                   "\n " + " SELECT EM.EMPLOYEEID , LT.LEAVETYPEID, 0, 0,0,0,OPBALANCE,0 ,0,0 " +
                                 "\n " + "  FROM     TK_LEAVEOPBAL OP " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = OP.LEAVETYPEID  " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";
        //over
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;

        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += "\n " + "  AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID +
                                 "\n " + "  AND IsEarned = 1 " +

                                 //over
            // for include holiday
                "\n " + " INSERT INTO #TMP " +
                                "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0,ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA    " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                                "\n " + " WHERE TA.WEEKLYOFF = 0 AND TA.HOLIDAY = 0 AND  ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";

        //
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;

        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                            "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID  " +


                            // CURRENT LEAVES

                             "\n " + " INSERT INTO #TMP " +
                                "\n " + "   SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA    " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID AND EM.COMPANYID =  " + Ginf.CompanyID +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                                "\n " + " WHERE TA.WEEKLYOFF = 1 AND LT.INCLUDEWEEKLYOFF = 1 AND  ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";
        //
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

// FOR INCLUDE WO
         "\n " + " INSERT INTO #TMP " +
                                "\n " + "  SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA  " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID AND EM.COMPANYID =  " + Ginf.CompanyID +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                                "\n " + " WHERE TA.holiday = 1 AND LT.INCLUDEHOLIDAY = 1 AND  TA.WEEKLYOFF = 0 AND LT.INCLUDEWEEKLYOFF = 0 AND ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";

        //
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

// FOR INCLUDE HOLIAY
   "\n " + " INSERT INTO #TMP " +
                                "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0, 0,0,ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0 FROM TK_ATTENDANCE TA   " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                                 "\n " + " WHERE TA.WEEKLYOFF = 0 AND TA.HOLIDAY = 0 AND   ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;
        //
        StrQry += " AND ISONLEAVE = 1 " +
                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

// FOR INCLUDE WO
           "\n " + " INSERT INTO #TMP " +
                                "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0), 0 FROM TK_ATTENDANCE TA   " +
                                 "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                                 "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                                 "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                                 "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                                 "\n " + " WHERE TA.WEEKLYOFF = 1 AND LT.INCLUDEWEEKLYOFF = 1 AND   ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
        //
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

// FOR INCLUDE HOLIAY
"\n " + " INSERT INTO #TMP " +
                             "\n " + "  SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0), 0 FROM TK_ATTENDANCE TA  " +
                              "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                              "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                              "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                              "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                              "\n " + " WHERE TA.HOLIDAY = 1 AND LT.INCLUDEHOLIDAY = 1 AND TA.WEEKLYOFF = 0 AND LT.INCLUDEWEEKLYOFF = 0 AND  ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

                             "\n " + " SELECT EMPLOYEEID,LEAVETYPEID , SUM(PREVOPBAL) AS PREVOPBAL , SUM(CURROPBAL) AS CURROPBAL, SUM(ADJ) AS ADJ , sum(ENCASHED) as  ENCASHED,sum(EARNED) as EARNED , SUM(PREVTAKEN) AS PREVTAKEN , SUM(CURRTAKEN) AS CURRTAKEN , SUM(CLBAL)  AS CLBAL  " +
                             "\n " + "  INTO #TMP1 " +
                             "\n " + " FROM #TMP GROUP BY EMPLOYEEID ,LEAVETYPEID " +
                             "\n " + " UPDATE #TMP1 SET  CLBAL = PREVOPBAL + CURROPBAL + ADJ- PREVTAKEN - CURRTAKEN  - ENCASHED + EARNED  " +
                             //"\n " + " SELECT  LT.LEAVETYPENAME AS LEAVETYPE, PREVOPBAL+ CURROPBAL AS OPBAL , T.ADJ AS ADJUSTMENTS,  ENCASHED as  ENCASHED, EARNED as EARNED, PREVTAKEN + CURRTAKEN AS TAKEN , CLBAL   " +
                               //"\n " + " SELECT  LT.LEAVETYPENAME AS LEAVETYPE, PREVOPBAL+ CURROPBAL AS OPBAL , T.ADJ AS ADJUSTMENTS, CLBAL   " +
                               "\n " + " SELECT  LT.LEAVETYPENAME AS LEAVETYPE, PREVOPBAL+ CURROPBAL AS OPBAL , PREVTAKEN + CURRTAKEN AS TAKEN , CLBAL   " +
                             "\n " + " FROM #TMP1 T " +
                             "\n " + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = T.EMPLOYEEID AND EM.COMPANYID =  " + Ginf.CompanyID +
                             "\n " + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = T.LEAVETYPEID  " +
                             "\n " + " ORDER BY T.EMPLOYEEID " +
                             "\n " + " DROP TABLE #TMP " +
                             "\n " + " DROP TABLE #TMP1 ";

        DataTable DT = GlobalFill.FillDataTable(StrQry);
        return DT;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Ginf = new GlobalInformation(this);

        if (!Page.IsPostBack)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            RefreshData();
            if (Request.QueryString["ID"] != null)
            {
                Int64 ID = Convert.ToInt64(Request.QueryString["ID"]);
                Obj = new LeaveTrans(ID);
                //RefreshData();
                LoadFields();
            }


            lblLevel.Text = Request.QueryString["Level"];

            if (Request.QueryString["Mode"] == "Add")
            {
                btnTrans.Text = "Save";
                LoadFields();

            }
            else if (Request.QueryString["Mode"] == "Edit")
            {
                btnTrans.Text = "Update";
            }
            else if (Request.QueryString["Mode"] == "Delete")
            {
                btnTrans.Text = "Remove";
            }
            else if (Request.QueryString["Mode"] == "Approve")
            {
                btnTrans.Text = "Approve";
                DisableFields();
            }
            else if (Request.QueryString["Mode"] == "Reject")
            {
                btnTrans.Text = "Reject";
                DisableFields();
            }

        }




    }
    private void DisableFields()
    {
        txtFromDate.Disabled = true;
        txtToDate.Disabled = true;
        txtReason.Enabled = false;
        ddlLeavetype.Enabled = false;

    }
    private void LoadFields()
    {

        if (btnTrans.Text == "Save" || btnTrans.Text == "Update")
        {
            Employee ObjEmployee = new Employee(Ginf.EmployeeId);
            lblEmployee.Text = ObjEmployee.EmployeeFirstName + " " + ObjEmployee.EmployeeLastName;
            lblEmployeeID.Text = Convert.ToString(ObjEmployee.EmployeeID);

        }
        else
        {
            Employee ObjEmployee = new Employee(Convert.ToInt64(Obj.EmployeeID));
            lblEmployee.Text = ObjEmployee.EmployeeFirstName + " " + ObjEmployee.EmployeeLastName;
            lblEmployeeID.Text = Convert.ToString(ObjEmployee.EmployeeID);

        }




        txtFromDate.Value = Obj.FromDate.ToString("dd-MMMM-yyyy");
        txtToDate.Value = Obj.ToDate.ToString("dd-MMMM-yyyy");


        if (Obj.HalfDayOnFrom)
            ddlfrom.SelectedIndex = 0;
        else
            ddlfrom.SelectedIndex = 1;
        if (Obj.HalfDayOnTo)
            ddlto.SelectedIndex = 0;
        else
            ddlto.SelectedIndex = 1;
        lblAppliedOn.Text = Obj.LeaveAppliedDate.ToString("dd-MMMM-yyyy");
        ddlLeavetype.SelectedValue = Obj.LeaveTypeID.ToString();
        txtReason.Text = Obj.Reason;
    }
    private void LoadEntities()
    {
        try
        {
            Int64 ID = Convert.ToInt64(Request.QueryString["ID"]);
            Obj.LeaveTransID = ID;

            Obj.FromDate = Convert.ToDateTime(txtFromDate.Value);
            Obj.ToDate = Convert.ToDateTime(txtToDate.Value);

            Obj.EmployeeID = Convert.ToInt64(lblEmployeeID.Text);

            if (ddlfrom.SelectedItem.ToString() == "Half Day")
                Obj.HalfDayOnFrom = true;
            else
                Obj.HalfDayOnFrom = false;
            if (ddlto.SelectedItem.ToString() == "Half Day")
                Obj.HalfDayOnTo = true;
            else
                Obj.HalfDayOnTo = false;
            Obj.LeaveAppliedDate = Convert.ToDateTime(lblAppliedOn.Text);
            Obj.Reason = txtReason.Text;
            Obj.LeaveTypeID = Convert.ToInt64(ddlLeavetype.SelectedValue);
            Obj.CompanyID = Ginf.CompanyID;

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }
    private void AddLeaveApplication()
    {

    }
    private void UpdateLeaveApplication()
    {

    }
    private void DeleteLeaveApplication()
    {

    }

    private Boolean ValidateLeaveApplication()
    {
        if (Convert.ToInt32(ddlLeavetype.SelectedValue) <= 0)
        {
            lblMessage.Text = "Please Select Leave Type ...";
            return false;
        }
        if ((txtFromDate.Value == txtToDate.Value) && (ddlfrom.SelectedItem.Text != ddlto.SelectedItem.Text))
        {
            lblMessage.Text = "Half Day and Full Day not Valid for the Same Day ... ";
            return false;
        }
        if (Convert.ToDateTime(txtFromDate.Value) > Convert.ToDateTime(txtToDate.Value))
        {
            lblMessage.Text = "fromdate should be greater or equal todate";
            return false;
        }

        if (Obj.LeaveApplicationExists()) 
        {
            lblMessage.Text = "Leave Entry Exists on Selected Date";
            return false;
        }
        else
        {




            String configCheckDashBoardFromDB = Convert.ToString(GlobalFill.GetQueryValue("select property_value from TK_CONFIG where property_key='APPLY_LEAVE_ONLY_ASSIGNED'"));

            if (configCheckDashBoardFromDB.ToUpper().Equals("TRUE"))
            {
                String StrSql1 = "SELECT COUNT(EMPLOYEEID) FROM TK_LeaveOpBal WHERE EMPLOYEEID = " + Obj.EmployeeID + " AND LEAVETYPEID = " + Convert.ToInt32(ddlLeavetype.SelectedValue);

                double i1 = Convert.ToDouble(GlobalFill.GetQueryValue(StrSql1));
                if (i1 <= 0)
                {
                    lblMessage.Text = "Leave Not Assigned to Employee " + "Leave Not Found";
                    return true;
                }
            }


            String StrSql = "SELECT COUNT(EMPLOYEEID) FROM TK_ATTENDANCE WHERE ISONLEAVE = 1 AND ATTENDANCEDATE BETWEEN '" + Obj.FromDate.ToString("ddMMMyyyy") + "' AND '" + Obj.ToDate.ToString("ddMMMyyyy") + "' AND EMPLOYEEID = " + Obj.EmployeeID;

            double i = Convert.ToDouble(GlobalFill.GetQueryValue(StrSql));
            if (i > 0)
            {
                lblMessage.Text = "Leave Exit between " + Obj.FromDate.ToString("ddMMMyyyy") + " and " + Obj.ToDate.ToString("ddMMMyyyy");
                return true;
            }
            StrSql = "SELECT COUNT(EMPLOYEEID) FROM TK_ATTENDANCE WHERE HOLIDAY = 1 AND ATTENDANCEDATE BETWEEN '" + Obj.FromDate.ToString("ddMMMyyyy") + "' AND '" + Obj.ToDate.ToString("ddMMMyyyy") + "' AND EMPLOYEEID = " + Obj.EmployeeID;

            i = Convert.ToDouble(GlobalFill.GetQueryValue(StrSql));
            if (i > 0)
            {
                lblMessage.Text = "Holiday Exit between " + Obj.FromDate.ToString("ddMMMyyyy") + " and " + Obj.ToDate.ToString("ddMMMyyyy") + "Leave Found";
                return true;
            }

            StrSql = "SELECT COUNT(EMPLOYEEID) FROM TK_EMPLOYEE WHERE INACTIVE=1 AND EMPLOYEEID = " + Obj.EmployeeID;

            i = Convert.ToDouble(GlobalFill.GetQueryValue(StrSql));

            if (i > 0)
            {
                lblMessage.Text = "Inactive Employee can not apply for Leave " + "Employee Not Active ";
                return true;
            }



            return true;
        }
    }


    

    protected void Button7_Click(object sender, EventArgs e)
    {

        if ((Request.QueryString["Mode"] == "Approve") || (Request.QueryString["Mode"] == "Reject"))
        {
            leavesUpdate();
        }
        //else
        //{
        //    if(
        //}
        else
        {
            saveEmployeeLeave();
        }




    }


    protected void leavesUpdate() {

        LoadEntities();

        if (ValidateLeaveApplication()==true)
        {

            Employee ObjEmployee = new Employee(Obj.EmployeeID);


            SqlConnection SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            SqlTransaction SqlTrans = SqlConn.BeginTransaction();

            try
            {
                String Mes = "";
                if (btnTrans.Text == "Save")
                {
                    Int64 I = Obj.Add(SqlTrans, false);
                    //Mes = Obj.CompanyID.ToString() + " " + Obj.EmployeeID.ToString() + " " + Obj.LeaveTypeID.ToString() + " " +
                    //    Obj.FromDate.ToString() + " " + Obj.ToDate.ToString() + " ADD - " + Obj.Added.ToString() + " -- " +
                    //     I.ToString() + " Record Saved Successfully";
                    Mes = " Record Saved Successfully";

                }
                else if (btnTrans.Text == "Update")
                {
                    Obj.Modify(SqlTrans, false);
                    Mes = "Record Updated Successfully";
                }
                else if (btnTrans.Text == "Approve")
                {
                    if (lblLevel.Text == "HOD")
                    {
                        Obj.IsApproved = true;
                        Obj.ApprovedBy = Ginf.EmployeeName;

                        Obj.AuthorisedDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));
                    }



                    if (lblLevel.Text == "RO")
                    {
                        Obj.IsROApproved = true;
                        Obj.RoApprovedBy = Ginf.EmployeeName;

                        if (ObjEmployee.Reporting1IsFinal)
                        {
                            Obj.IsApproved = true;
                            Obj.ApprovedBy = Ginf.EmployeeName;

                            Obj.AuthorisedDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));
                        }



                    }
                    if (lblLevel.Text == "RO2")
                    {


                        Obj.IsRO2Approved = true;
                        Obj.Ro2ApprovedBy = Ginf.EmployeeName;

                        if (ObjEmployee.Reporting2IsFinal)
                        {
                            Obj.IsApproved = true;
                            Obj.ApprovedBy = Ginf.EmployeeName;

                            Obj.AuthorisedDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));
                        }

                    }
                    Obj.Modify(SqlTrans, false);


                    if (Obj.IsApproved)
                    {

                        DateTime stD = Obj.FromDate;
                        DateTime toD = Obj.ToDate;
                        SqlCommand SqlCmd;
                        for (stD = Obj.FromDate; stD <= toD; stD = stD.AddDays(1))
                        {
                            Boolean blnH = false;
                            if (stD == Obj.FromDate)
                                blnH = Convert.ToBoolean(Convert.ToInt32(Obj.HalfDayOnFrom));
                            else if (stD == Obj.ToDate)
                                blnH = Convert.ToBoolean(Convert.ToInt32(Obj.HalfDayOnTo));



                            SqlCmd = new SqlCommand();
                            SqlCmd.Connection = SqlTrans.Connection; // GlobalFunctions.GetConnection();
                            SqlCmd.Transaction = SqlTrans;
                            //SqlCmd.CommandText = "UPDATE TK_ATTENDANCE SET ISONLEAVE = 1,HALFDAY = 0, " +
                            //                     " HALFDAYLEAVE = " + Convert.ToInt32(blnH) + "," +
                            //                     " LEAVETYPEID =  " + Obj.LeaveTypeID +
                            //                     " WHERE EMPLOYEEID = " + Obj.EmployeeID +
                            //                   " AND ATTENDANCEDATE = '" + stD.ToString("MM/dd/yyyy") + "'";

                            SqlCmd.CommandText = "UPDATE TK_ATTENDANCE SET ISONLEAVE = 1,HALFDAY = 0, " +
                                           " HALFDAYLEAVE = " + Convert.ToInt32(blnH) + "," +
                                           " LEAVETYPEID =  " + Obj.LeaveTypeID +
                                           " WHERE EMPLOYEEID = " + Obj.EmployeeID +
                                // " AND ATTENDANCEDATE = '" + stD.ToString("dd/MMM/yy") + "'";
                                          " AND ATTENDANCEDATE = cast('" + stD.ToString("dd/MMM/yy") + "' as DateTime)";
                            //      " AND ATTENDANCEDATE = '" + stD + "'";
                            //" AND ATTENDANCEDATE = '" + stD.ToString("MM/dd/yyyy") + "'";
                      
                            SqlCmd.ExecuteNonQuery();
                        }

                    }


                    Mes = "Leave Approved Successfully";
                }
                else if (btnTrans.Text == "Reject")
                {
                    if (lblLevel.Text == "HOD")
                    {
                        Obj.IsRejected = true;

                    }
                    if (lblLevel.Text == "RO")
                    {
                        Obj.IsRORejected = true;

                    }

                    if (lblLevel.Text == "RO2")
                    {
                        Obj.IsRO2Rejected = true;

                    }

                    Obj.Modify(SqlTrans, false);
                    Mes = "Leave Rejected Successfully";
                }
                else if (btnTrans.Text == "Remove")
                {
                    Obj.Delete(SqlTrans, false);
                    Mes = "Record Deleted Successfully";
                }
                //SqlTrans.Commit();


                lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Text = Mes;
                btnTrans.Visible = false;

                if (btnTrans.Text == "Save")
                {
                    lblMessage.Text += " " + SendHODMail();
                }
                else if (btnTrans.Text == "Update")
                {
                    lblMessage.Text += " " + SendHODMail();
                }
                else if (btnTrans.Text == "Approve")
                {
                    lblMessage.Text += " " + SendEmployeeMail("Leave Application Approved");
                }
                else if (btnTrans.Text == "Reject")
                {
                    lblMessage.Text += " " + SendEmployeeMail("Leave Application Rejected");
                }

                SqlTrans.Commit();
            }
            catch (Exception E)
            {
                SqlTrans.Rollback();
                lblMessage.Text = E.Message;
            }
            SqlConn.Dispose();
        }
       
    
    
    }
    protected void saveEmployeeLeave() {
        if (Session["LeaveDetails"] != null)
        {
            dt = (DataTable)Session["LeaveDetails"];
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (ddlLeavetype.SelectedItem.Text == dt.Rows[i]["LEAVETYPE"].ToString())
                {
                    int clbal = Convert.ToInt32(dt.Rows[i]["CLBAL"]);
                    DateTime dtFromDate = Convert.ToDateTime(txtFromDate.Value);
                    DateTime dtToDate = Convert.ToDateTime(txtToDate.Value);



                    int ts = (dtToDate - dtFromDate).Days + 1;
                    //
                    Int16 min = Convert.ToInt16(GlobalFill.GetQueryValue("SELECT MINFORAPPLICATION FROM TK_LEAVETYPE WHERE LeaveTypeName= '" + ddlLeavetype.SelectedItem.Text + "'"));
                    Int16 max = Convert.ToInt16(GlobalFill.GetQueryValue("SELECT MAXFORAPPLICATION FROM TK_LEAVETYPE WHERE LeaveTypeName= '" + ddlLeavetype.SelectedItem.Text + "'"));

                    Int16 allowNegative = Convert.ToInt16(GlobalFill.GetQueryValue("SELECT ALLOWNEGATIVE FROM TK_LEAVETYPE WHERE LeaveTypeName= '" + ddlLeavetype.SelectedItem.Text + "'"));


                    if (btnTrans.Text == "Save")
                    {
                        if (ts < min && min > 0)
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Min leave application criteria does not meet, Please try again";
                            return;
                        }

                        if (ts > max && max > 0)
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Max leave application criteria does not meet, Please try again ";
                            return;
                        }

                        if (allowNegative == 0)
                        {
                            if (ts > clbal)
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "Avaliable leave balance is not suffucent to apply leave ";
                                return;
                            }
                        }
                        //else
                        //{
                        //    if (ts > clbal)
                        //    {
                        //        lblMessage.Visible = true;
                        //        lblMessage.Text = "Avaliable leave balance is not suffucent to apply leave ";
                        //        return;
                        //    }
                        //}
                    }

                    leavesUpdate();

                }
            }
        }
    
    
    }


    private string SendHODMail()
    {
        Company ObjCompany = new Company(Ginf.CompanyID);

        String MailBody = ObjCompany.LeaveTemplateHTML;

        string empno = Obj.EmployeeID.ToString();
       // string leavetypeID = Convert.ToString(GlobalFill.GetQueryValue("select Max(LeaveTransID) from TK_LeaveTrans"));
        string leavetypeID = objmstbase.TransID.ToString();
        string LeaveTypeName = Convert.ToString(GlobalFill.GetQueryValue("SELECT LEAVETYPENAME FROM TK_LEAVETYPE WHERE LEAVETYPEID = " + Obj.LeaveTypeID));
        string EmployeeName = Convert.ToString(GlobalFill.GetQueryValue("SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + Obj.EmployeeID));
        string Department = Convert.ToString(GlobalFill.GetQueryValue("SELECT DEPARTMENTNAME FROM TK_DEPARTMENT DM " +
             " INNER JOIN TK_SECTION SEM ON SEM.DEPARTMENTID = DM.DEPARTMENTID " +
             " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = " + Obj.EmployeeID));
        string Designation = Convert.ToString(GlobalFill.GetQueryValue("SELECT DESIGNATIONNAME FROM TK_DESIGNATION DEM" +
            " INNER JOIN TK_EMPLOYEE EM ON EM.DESIGNATIONID = DEM.DESIGNATIONID " + " WHERE EM.EMPLOYEEID = " + Obj.EmployeeID));
        Int64 ReportingEmpID1 = Convert.ToInt64(GlobalFill.GetQueryValue("SELECT REPORTINGEMPLOYEEID1 FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + Obj.EmployeeID));

        MailBody = MailBody.Replace("{LEAVETYPE}", LeaveTypeName);
        MailBody = MailBody.Replace("{REASON}", Obj.Reason);
        MailBody = MailBody.Replace("{FROM}", Obj.FromDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{TO}", Obj.ToDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{EMPLOYEE}", EmployeeName);
        MailBody = MailBody.Replace("{DESIGNATION}", Designation);
        MailBody = MailBody.Replace("{DEPARTMENT}", Department);
        MailBody = MailBody.Replace("{COMPANYNAME}", ObjCompany.CompanyName);
        if (ReportingEmpID1 > 0)
        {
            string Reportting1mailID = Convert.ToString(GlobalFill.GetQueryValue("Select EmailID from tk_employee where EMPLOYEEID = " + ReportingEmpID1));
            return GlobalFunctions.SendEmail(Reportting1mailID, "Leave Application Submitted", Convert.ToInt64(Ginf.CompanyID), LeaveTypeName,empno,Obj.Reason,leavetypeID, MailBody, EmployeeName, Department, Designation, Obj.FromDate.ToString("dd MMMM yyyy"), Obj.ToDate.ToString("dd MMMM yyyy"));
        }
        return "";
    }
    private string SendEmployeeMail(String Subject)
    {
        Company ObjCompany = new Company(Convert.ToInt64(Ginf.CompanyID));
        //LeaveTrans ltv = new LeaveTrans(Convert.ToInt64(LeaveTypeName);
        String MailBody = ObjCompany.LeaveTemplateHTML;
        string empno = Obj.EmployeeID.ToString();
        string leavetypeID = objmstbase.TransID.ToString();
        string LeaveTypeName = Convert.ToString(GlobalFill.GetQueryValue("SELECT LEAVETYPENAME FROM TK_LEAVETYPE WHERE LEAVETYPEID = " + Obj.LeaveTypeID));
        string EmployeeName = Convert.ToString(GlobalFill.GetQueryValue("SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + Obj.EmployeeID));
        string EmpemailID = Convert.ToString(GlobalFill.GetQueryValue("SELECT EmailID FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + Obj.EmployeeID));
        string Department = Convert.ToString(GlobalFill.GetQueryValue("SELECT DEPARTMENTNAME FROM TK_DEPARTMENT DM " +
             " INNER JOIN TK_SECTION SEM ON SEM.DEPARTMENTID = DM.DEPARTMENTID " +
             " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = " + Obj.EmployeeID));
        string Designation = Convert.ToString(GlobalFill.GetQueryValue("SELECT DESIGNATIONNAME FROM TK_DESIGNATION DEM" +
            " INNER JOIN TK_EMPLOYEE EM ON EM.DESIGNATIONID = DEM.DESIGNATIONID " + " WHERE EM.EMPLOYEEID = " + Obj.EmployeeID));

        MailBody = MailBody.Replace("{LEAVETYPE}", LeaveTypeName);
        MailBody = MailBody.Replace("{REASON}", Obj.Reason);
        MailBody = MailBody.Replace("{FROM}", Obj.FromDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{TO}", Obj.ToDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{EMPLOYEE}", EmployeeName);
        MailBody = MailBody.Replace("{DESIGNATION}", Designation);
        MailBody = MailBody.Replace("{DEPARTMENT}", Department);
        MailBody = MailBody.Replace("{COMPANYNAME}", ObjCompany.CompanyName);


        return GlobalFunctions.SendEmailfromHOD(EmpemailID, Subject, Convert.ToInt64(Ginf.CompanyID), LeaveTypeName, empno, Obj.Reason, leavetypeID, MailBody, EmployeeName, Department, Designation, Obj.FromDate.ToString("dd MMMM yyyy"), Obj.ToDate.ToString("dd MMMM yyyy"));


    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (btnTrans.Text == "Approve" || btnTrans.Text == "Reject")
        {
            Response.Redirect("~/ApproveLeaves.aspx");
        }
        else
        {
            Response.Redirect("~/Leaves.aspx");
        }
    }

}
