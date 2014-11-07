using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
    Int64 EmployeeID;
    Boolean WHBASEDFILO;
    protected void Page_Load(object sender, EventArgs e)
    {
        Ginf = new GlobalInformation(this);
        if (Convert.ToInt64(Ginf.EmployeeId ) == 0)
        {
            Response.Redirect("~/login.aspx");
        }
        Page.Title = "SamayBio Employee Self Service";

        EmployeeID = Convert.ToInt64(Ginf.EmployeeId );
        grid1data();
        gridapprove();
   
        //////if (IsPostBack != true)
        //////{
        //////    lblErrorMsg.Visible = false;
        //////    txtFromDate.Value = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy"));
        //////    txtToDate.Value = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy"));

        //////    DateTime dtFromDate = Convert.ToDateTime(txtFromDate.Value);
        //////    DateTime dtToDate = Convert.ToDateTime(txtToDate.Value);

        //////    DataTable dt = GetEmployeeLeaves(EmployeeID, dtFromDate, dtToDate, "", Convert.ToInt64(Ginf.CalendarYearID));
        //////   // gvledger.DataSource = dt;
        //////   // gvledger.DataBind();
        //////}
    }

    public void gridapprove()
    {
      ////////  string StrSql = " SELECT   LTR.OnDutyID as Id , CONVERT(VARCHAR,ta.AttendanceDate,106) AS AppliedOn , " +
      ////////" CONVERT(VARCHAR,LTR.FromTime,100) as FromDate, CONVERT(VARCHAR,LTR.ToTime,100)  as ToDate , ltr.Description as Reason,     " +
      ////////" LTR.ApprovedBy, CASE WHEN COALESCE(ApprovedBy,'')=''   THEN 'APPROVED'  END AS STATUS  " +
      ////////" FROM TK_OnDuty LTR  ,TK_Attendance ta ,TK_EMPLOYEE EM where EM.EMPLOYEEID = ta.EMPLOYEEID and ta.AttendanceID = LTR.AttendanceID " +
      ////////" and COALESCE(ApprovedBy,'')!='NULL' AND EM.EMPLOYEEID = " + EmployeeID + " AND year(AttendanceDate)=YEAR(getdate())  order by AttendanceDate desc";
        string StrSql = "SELECT   LTR.OnDutyID as Id , CONVERT(VARCHAR,ta.AttendanceDate,106) AS AppliedOn ," +
         " CONVERT(VARCHAR,LTR.FromTime,100) as FromDate, CONVERT(VARCHAR,LTR.ToTime,100)  as ToDate , ltr.Description as Reason," +
         " CASE WHEN ISAPPROVED ='true' THEN 'APPROVED' WHEN ISREJECTED='true' then 'REJECTED'  END AS STATUS" +
         " FROM TK_OnDuty LTR  ,TK_Attendance ta ,TK_EMPLOYEE EM where EM.EMPLOYEEID = ta.EMPLOYEEID and ta.AttendanceID = LTR.AttendanceID" +
         " and (COALESCE(ISAPPROVED,'')='true' or COALESCE(ISREJECTED,'')='true')  AND EM.EMPLOYEEID =" + EmployeeID + "  AND year(AttendanceDate)=YEAR(getdate())  order by AttendanceDate desc";
        //StrSql = "select CASE WHEN isapproved = 1 THEN 'APPROVED' WHEN ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS Status,OnDutyID as ID, FromTime,ToTime,Description from TK_OnDuty where isapproved is not null or isrejected is not null and year(FromTime)=YEAR(getdate()) order by FromTime desc";
        gvApproved.DataSource = GlobalFill.FillDataTable(StrSql);
        gvApproved.DataBind();
    }
    public void grid1data()
    {
        ////////////string StrSql = " SELECT   LTR.OnDutyID as Id , CONVERT(VARCHAR,ta.AttendanceDate,106) AS AppliedOn , " +
        ////////////" CONVERT(VARCHAR,LTR.FromTime,100) as FromDate, CONVERT(VARCHAR,LTR.ToTime,100)  as ToDate , ltr.Description as Reason,     " +
        ////////////" LTR.ApprovedBy, CASE WHEN COALESCE(ApprovedBy,'')='' THEN 'APPROVAL PENDING'  END AS STATUS  " +
        ////////////" FROM TK_OnDuty LTR  ,TK_Attendance ta ,TK_EMPLOYEE EM where EM.EMPLOYEEID = ta.EMPLOYEEID and ta.AttendanceID = LTR.AttendanceID " +
        ////////////" and COALESCE(ApprovedBy,'')='' AND EM.EMPLOYEEID = " + EmployeeID + " AND year(AttendanceDate)=YEAR(getdate())  order by AttendanceDate desc";
        //" FROM TK_TOURPLAN LTR  INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID   " +
        //" WHERE GRANTDATE IS  NULL AND REJECTDATE IS  NULL AND EM.EMPLOYEEID = " + EmployeeID;
        //string StrSql = "select OnDutyID as ID, FromTime,ToTime,Description,isapproved as status from TK_OnDuty where isapproved is null AND year(FromTime)=YEAR(getdate())  order by FromTime desc";
        // string StrSql = "select OnDutyID as ID, FromTime,ToTime,Description,CASE WHEN COALESCE(isapproved,'')='' THEN 'APPROVAL PENDING'  END AS STATUS from TK_OnDuty where isapproved is null AND year(FromTime)=YEAR(getdate())  order by FromTime desc";
        string StrSql = "SELECT   LTR.OnDutyID as Id , CONVERT(VARCHAR,ta.AttendanceDate,106) AS AppliedOn ,"+
         " CONVERT(VARCHAR,LTR.FromTime,100) as FromDate, CONVERT(VARCHAR,LTR.ToTime,100)  as ToDate , ltr.Description as Reason,"+
         " CASE WHEN ISAPPROVED ='FALSE' THEN 'APPROVAL PENDING' WHEN ISREJECTED='FALSE' then 'APPROVAL PENDING' ELSE 'APPROVAL PENDING' END AS STATUS"+ 
         " FROM TK_OnDuty LTR,TK_Attendance ta ,TK_EMPLOYEE EM where EM.EMPLOYEEID = ta.EMPLOYEEID and ta.AttendanceID = LTR.AttendanceID " +
         " and COALESCE(ISAPPROVED,'')='FALSE' AND COALESCE(ISREJECTED,'')='FALSE' AND EM.EMPLOYEEID =" + EmployeeID + "  AND year(AttendanceDate)=YEAR(getdate())  order by AttendanceDate desc";
        GridView1.DataSource = GlobalFill.FillDataTable(StrSql);
        GridView1.DataBind();
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        
    }
    protected void Button12_Click1(object sender, EventArgs e)
    {



    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       //if  (e.Row.RowType == DataControlRowType.Header )
       //{
       //    TableCell cell = e.Row.Cells[0];
       //    cell.Width = new Unit("50px");
       //    cell.Text = "Date";
         

 
       //}
      
        //    'For first column set to 200 px
        //    Dim cell As TableCell = e.Row.Cells(0)
        //    cell.Width = New Unit("200px")
 
        //    'For others set to 50 px
        //    'You can set all the width individually

        //    For i = 1 To e.Row.Cells.Count - 1
        //        'Mind that i used i=1 not 0 because the width of cells(0) has already been set
        //        Dim cell2 As TableCell = e.Row.Cells(i)
        //        cell2.Width = New Unit("10px")
        //    Next
        //End If
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

       // Response.Redirect("~/ApplyOnDuty.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName);
        string strcom = e.CommandArgument.ToString();
        string strcomname = e.CommandName;
        ViewState["comarg"]=strcom;
        ViewState["comname"] = strcomname;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ApplyOnDuty.aspx?Mode=Add");
    }

    public DataTable GetEmployeeLeaves(Int64 EmployeeId, DateTime FrDate, DateTime ToDate, string FilterStr, Int64 pCalendarYearId)
    {               
        string StrQry = "";

        string stDate = "'" + FrDate.ToString("ddMMMyyyy") + "'";
        string endDate = "'" + ToDate.ToString("ddMMMyyyy") + "'";
        
        
        StrQry = "SELECT STARTINGDATE FROM TK_CALENDARYEAR WHERE CALENDARYEARID = " + pCalendarYearId ;
        DateTime StartingDate = Convert.ToDateTime(GlobalFill.GetQueryValue(StrQry));
        StrQry = "SELECT ENDINGDATE FROM TK_CALENDARYEAR WHERE CALENDARYEARID = " + pCalendarYearId ;
        DateTime EndingDate = Convert.ToDateTime(GlobalFill.GetQueryValue(StrQry));


        StrQry = "DECLARE @FROMDATE DATETIME " +
                          "\n " + "DECLARE @TODATE DATETIME " +
                          "\n " + " DECLARE @CALENDARYEARSTARTDATE DATETIME " +
                          "\n " + " SET @FROMDATE = " + stDate +
                         "\n " + " SET @TODATE = " + endDate +
                        "\n " + " SET @CALENDARYEARSTARTDATE = '" + StartingDate.ToString("ddMMMyyyy") + "'" +
                        "  CREATE TABLE #TMP (EMPLOYEEID BIGINT, LEAVETYPEID INT, PREVOPBAL FLOAT, CURROPBAL FLOAT, ADJ FLOAT, PREVTAKEN FLOAT, CURRTAKEN FLOAT, CLBAL FLOAT)  " +
                        "\n " + " INSERT INTO #TMP  " +
                        "\n " + " SELECT OP.EMPLOYEEID , LT.LEAVETYPEID, OPBALANCE AS PREVOPBAL, 0,0,0,0 ,0 " +
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

        StrQry += " AND FRDATE  < @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID + " AND ISADJUSTED = 0 " +

                   "\n " + " INSERT INTO #TMP  " +
                   "\n " + " SELECT OP.EMPLOYEEID , LT.LEAVETYPEID, OPBALANCE AS CURROPBAL, 0,0,0,0 ,0 " +
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

                               "\n " + " AND ISADJUSTED = 0 " +
                               "\n " + " INSERT INTO #TMP  " +
                               "\n " + " SELECT EM.EMPLOYEEID , LT.LEAVETYPEID, 0, 0,OPBALANCE,0,0 ,0 " +
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


                            "\n " + " INSERT INTO #TMP " +
                            "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA  " +
                             "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                             "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                             "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                             "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                            "\n " + " WHERE TA.WEEKLYOFF = 0 AND TA.HOLIDAY = 0 AND  ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;

        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                            "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID  " +

// for include wo
                "\n " + " INSERT INTO #TMP " +
                            "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA  " +
                             "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                             "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                             "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                             "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                            "\n " + " WHERE TA.WEEKLYOFF = 1 AND LT.INCLUDEWEEKLYOFF = 1 AND  ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;

        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                            "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID  " +

// for include holiday
                "\n " + " INSERT INTO #TMP " +
                            "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA  " +
                             "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                             "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                             "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                             "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                            "\n " + " WHERE TA.holiday = 1 AND LT.INCLUDEHOLIDAY = 1 AND  TA.WEEKLYOFF = 0 AND LT.INCLUDEWEEKLYOFF = 0 AND ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;

        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                            "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID  " +


                            // CURRENT LEAVES

                            "\n " + " INSERT INTO #TMP " +
                            "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0 FROM TK_ATTENDANCE TA  " +
                             "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                             "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                             "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                             "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                             "\n " + " WHERE TA.WEEKLYOFF = 0 AND TA.HOLIDAY = 0 AND   ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

// FOR INCLUDE WO
        "\n " + " INSERT INTO #TMP " +
                             "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0 FROM TK_ATTENDANCE TA  " +
                              "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
                              "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
                              "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
                              "\n" + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = TA.LEAVETYPEID  " +
                              "\n " + " WHERE TA.WEEKLYOFF = 1 AND LT.INCLUDEWEEKLYOFF = 1 AND   ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
        if (EmployeeId > 0)
            StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        if (FilterStr != "")
            StrQry += "\n " + FilterStr;

        StrQry += " AND ISONLEAVE = 1 " +
                             "\n " + " GROUP BY TA.EMPLOYEEID,TA.LEAVETYPEID " +

// FOR INCLUDE HOLIAY
"\n " + " INSERT INTO #TMP " +
                             "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0 FROM TK_ATTENDANCE TA  " +
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

                             "\n " + " SELECT EMPLOYEEID,LEAVETYPEID , SUM(PREVOPBAL) AS PREVOPBAL , SUM(CURROPBAL) AS CURROPBAL, SUM(ADJ) AS ADJ , SUM(PREVTAKEN) AS PREVTAKEN , SUM(CURRTAKEN) AS CURRTAKEN , SUM(CLBAL)  AS CLBAL " +
                             "\n " + "  INTO #TMP1 " +
                             "\n " + " FROM #TMP GROUP BY EMPLOYEEID ,LEAVETYPEID " +
                             "\n " + "UPDATE #TMP1 SET  CLBAL = PREVOPBAL + CURROPBAL + ADJ- PREVTAKEN - CURRTAKEN  " +
                             "\n " + " SELECT  EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME + ' ' + EM.EMPLOYEELASTNAME AS EMPLOYEE, LT.LEAVETYPENAME AS LEAVETYPE, PREVOPBAL+ CURROPBAL AS OPBAL , T.ADJ AS ADJUSTMENTS, PREVTAKEN + CURRTAKEN AS TAKEN , CLBAL " +
                             "\n " + " FROM #TMP1 T " +
                             "\n " + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = T.EMPLOYEEID " +
                             "\n " + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = T.LEAVETYPEID  " +
                             "\n " + " ORDER BY T.EMPLOYEEID " +
                             "\n " + " DROP TABLE #TMP " +
                             "\n " + " DROP TABLE #TMP1 ";

        




        //StrQry = "DECLARE @FROMDATE DATETIME " +
        //       "\n " + "DECLARE @TODATE DATETIME " +
        //       "\n " + " DECLARE @CALENDARYEARSTARTDATE DATETIME " +
        //       "\n " + " SET @FROMDATE = " + stDate +
        //      "\n " + " SET @TODATE = " + endDate +
        //     "\n " + " SET @CALENDARYEARSTARTDATE = '" + StartingDate.ToString("ddMMMyyyy") + "'" +
        //     "  CREATE TABLE #TMP (EMPLOYEEID BIGINT, LEAVETYPEID INT, PREVOPBAL FLOAT, CURROPBAL FLOAT, ADJ FLOAT, PREVTAKEN FLOAT, CURRTAKEN FLOAT, CLBAL FLOAT)  " +
        //     "\n " + " INSERT INTO #TMP  " +
        //     "\n " + " SELECT OP.EMPLOYEEID , LEAVETYPEID, OPBALANCE AS PREVOPBAL, 0,0,0,0 ,0 " +
        //     "\n " + "  FROM     TK_LEAVEOPBAL OP " +
        //     "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
        //     "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
        //     "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
        //     "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

        //if (EmployeeId > 0)
        //    StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;

        //if (FilterStr != "")
        //    StrQry += "\n " + FilterStr;

        //StrQry += " AND FRDATE  < @FROMDATE AND CALENDARYEARID = " + pCalendarYearId  + " AND EM.COMPANYID =  " + Ginf.CompanyID .ToString ()+ " AND ISADJUSTED = 0 " +

        //           "\n " + " INSERT INTO #TMP  " +
        //           "\n " + " SELECT OP.EMPLOYEEID , LEAVETYPEID, OPBALANCE AS CURROPBAL, 0,0,0,0 ,0 " +
        //           "\n " + "  FROM     TK_LEAVEOPBAL OP " +
        //         "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
        //         "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
        //         "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
        //             "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";


        //if (EmployeeId > 0)
        //    StrQry += "\n " + "  AND OP.EMPLOYEEID = " + EmployeeId;
        //if (FilterStr != "")
        //    StrQry += "\n " + FilterStr;



        //StrQry += "\n " + " AND FRDATE BETWEEN @FROMDATE AND @TODATE AND CALENDARYEARID = " +pCalendarYearId  + " AND EM.COMPANYID =  " + Ginf.CompanyID .ToString ()+

        //                       "\n " + " AND ISADJUSTED = 0 " +
        //                       "\n " + " INSERT INTO #TMP  " +
        //                       "\n " + " SELECT EM.EMPLOYEEID , LEAVETYPEID, 0, 0,OPBALANCE,0,0 ,0 " +
        //                     "\n " + "  FROM     TK_LEAVEOPBAL OP " +
        //                     "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = OP.EMPLOYEEID " +
        //                     "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
        //                     "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
        //                     "\n" + "  WHERE OP.EMPLOYEEID <> 0 ";

        //if (EmployeeId > 0)
        //    StrQry += "\n " + " AND  OP.EMPLOYEEID = " + EmployeeId;
        //if (FilterStr != "")
        //    StrQry += "\n " + FilterStr;

        //StrQry += "\n " + " AND FRDATE <=  @FROMDATE AND CALENDARYEARID = " + pCalendarYearId + " AND EM.COMPANYID =  " + Ginf.CompanyID .ToString ()+
        //                    "\n " + "  AND ISADJUSTED = 1 " +
        //                    "\n " + " INSERT INTO #TMP " +
        //                    "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0,0 FROM TK_ATTENDANCE  TA  " +
        //                     "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
        //                     "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
        //                     "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
        //                    "\n " + " WHERE ATTENDANCEDATE <  @FROMDATE AND ATTENDANCEDATE >= @CALENDARYEARSTARTDATE ";
        //if (EmployeeId > 0)
        //    StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;

        //if (FilterStr != "")
        //    StrQry += "\n " + FilterStr;

        //StrQry += " AND ISONLEAVE = 1 " +
        //                    "\n " + " GROUP BY TA.EMPLOYEEID,LEAVETYPEID  " +
        //                    "\n " + " INSERT INTO #TMP " +
        //                    "\n " + " SELECT TA.EMPLOYEEID, TA.LEAVETYPEID,0,0,0,0, ISNULL(SUM(CASE WHEN HALFDAYLEAVE = 1 THEN 0.5 ELSE 1 END),0) ,0 FROM TK_ATTENDANCE TA  " +
        //                     "\n" + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TA.EMPLOYEEID " +
        //                     "\n" + " INNER JOIN TK_SECTION S ON S.SECTIONID = EM.SECTIONID " +
        //                     "\n" + " INNER JOIN TK_DEPARTMENT D ON D.DEPARTMENTID = S.DEPARTMENTID " +
        //                     "\n " + " WHERE ATTENDANCEDATE BETWEEN @FROMDATE AND @TODATE ";
        //if (EmployeeId > 0)
        //    StrQry += "\n " + " AND  TA.EMPLOYEEID = " + EmployeeId;
        //if (FilterStr != "")
        //    StrQry += "\n " + FilterStr;

        //StrQry += " AND ISONLEAVE = 1 " +
        //                     "\n " + " GROUP BY TA.EMPLOYEEID,LEAVETYPEID " +
        //                     "\n " + " SELECT EMPLOYEEID,LEAVETYPEID , SUM(PREVOPBAL) AS PREVOPBAL , SUM(CURROPBAL) AS CURROPBAL, SUM(ADJ) AS ADJ , SUM(PREVTAKEN) AS PREVTAKEN , SUM(CURRTAKEN) AS CURRTAKEN , SUM(CLBAL)  AS CLBAL " +
        //                     "\n " + "  INTO #TMP1 " +
        //                     "\n " + " FROM #TMP GROUP BY EMPLOYEEID ,LEAVETYPEID " +
        //                     "\n " + "UPDATE #TMP1 SET  CLBAL = PREVOPBAL + CURROPBAL + ADJ- PREVTAKEN - CURRTAKEN  " +
        //                     "\n " + " SELECT LT.LEAVETYPENAME AS LeaveType, PREVOPBAL+ CURROPBAL AS Opening , T.ADJ AS Adjusted, PREVTAKEN + CURRTAKEN AS Taken ,clbal as  ClosingBalance " +
        //                     "\n " + " FROM #TMP1 T " +
        //                     "\n " + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = T.EMPLOYEEID " +
        //                     "\n " + " INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = T.LEAVETYPEID  " +
        //                     "\n " + " ORDER BY T.EMPLOYEEID " +
        //                     "\n " + " DROP TABLE #TMP " +
        //                     "\n " + " DROP TABLE #TMP1 ";

        DataTable DT = GlobalFill.FillDataTable(StrQry);
        return DT;
    }


    protected void btnGet_Click(object sender, EventArgs e)
    {
        DateTime dtFromDate = Convert.ToDateTime(txtFromDate.Value);
        DateTime dtToDate = Convert.ToDateTime(txtToDate.Value);

        if (dtFromDate > dtToDate)
        {
            lblErrorMsg.Visible = true;
        }
        else
        {
            lblErrorMsg.Visible = false;
            DataTable dt = GetEmployeeLeaves(EmployeeID, dtFromDate, dtToDate, "", Convert.ToInt64(Ginf.CalendarYearID));
            //gvledger.DataSource = dt;
            //gvledger.DataBind();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        Response.Redirect("~/ApplyOnDuty.aspx?ID=" + ViewState["comarg"] + "&Mode=" + ViewState["comname"]);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         GridView1.PageIndex = e.NewPageIndex;
         grid1data();
       
    }
    protected void gvApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApproved.PageIndex = e.NewPageIndex;
        gridapprove();
    }
}