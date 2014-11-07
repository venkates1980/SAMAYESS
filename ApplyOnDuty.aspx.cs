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
    TourPlan Obj = new TourPlan();
    LeaveTrans obhleavetran = new LeaveTrans();
    OnDuty Obj1 = new OnDuty();
    MasterBase objmstbase = new MasterBase();
    Int64 EmployeeID;
    DataTable dt;

    private void RefreshData()
    {
        lblAppliedOn.Text = System.DateTime.Now.ToString("ddMMMyyyy");
        //Code Added by kt
        Ginf = new GlobalInformation(this);
         EmployeeID = Convert.ToInt64(Ginf.EmployeeId);
        //to bring id values from query string
        
       // Request.QueryString["employeeids"] = EmployeeID.ToString();
        string StrQry = "select * from TK_CalendarYear where CalendarYearId=" + Convert.ToInt64(Ginf.CalendarYearID);
        DataTable DT = GlobalFill.FillDataTable(StrQry);
        if (DT.Rows.Count != 0)
        {
            DateTime dtFromDate = Convert.ToDateTime(DT.Rows[0]["StartingDate"].ToString());
            DateTime dtToDate = Convert.ToDateTime(DT.Rows[0]["EndingDate"].ToString());
            dt = GetEmployeeLeaves(EmployeeID, dtFromDate, dtToDate, "", Convert.ToInt64(Ginf.CalendarYearID));
            //gvledger.DataSource = dt;
            //gvledger.DataBind();
            Session["LeaveDetails"] = dt;
        }
    }

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
                             "\n " + " SELECT  LT.LEAVETYPENAME AS LEAVETYPE, PREVOPBAL+ CURROPBAL AS OPBAL , T.ADJ AS ADJUSTMENTS, PREVTAKEN + CURRTAKEN AS TAKEN , CLBAL " +
                             "\n " + " FROM #TMP1 T " +
                             "\n " + " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = T.EMPLOYEEID " +
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
                //old Obj = new TourPlan(ID);
                //new
                Obj1 = new OnDuty(ID);
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
        // txtFromDate.Disabled = true;
        //venkat txtToDate.Disabled = true;
        txtReason.Enabled = false;
        //venkat ddlLeavetype.Enabled = false;

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
            
           
            string strID = Request.QueryString["ID"];
            SqlConnection SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            SqlTransaction SqlTrans = SqlConn.BeginTransaction();
            //to bring attendance id
            string strcheck = "select AttendanceID from TK_OnDuty  where OnDutyID='" + strID + "'";
            SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
            sqlselect.Transaction = SqlTrans;
            String attendanceID;
            attendanceID = Convert.ToString(sqlselect.ExecuteScalar());
            ViewState["attendanceID"] = attendanceID;
            // to bring employee id
            string strcheckatt = "select EmployeeID from TK_Attendance where AttendanceID='" + attendanceID + "'";
            SqlCommand sqlselectatt = new SqlCommand(strcheckatt, SqlConn);
            sqlselectatt.Transaction = SqlTrans;
            String empID;
            empID = Convert.ToString(sqlselectatt.ExecuteScalar());

            //to bring employee name
            string strcheckemp = "select (TK_Employee.EmployeeFirstName + TK_Employee.EmployeeLastName) as name from TK_Employee where EmployeeID='" + empID + "'";
            SqlCommand sqlselectemp = new SqlCommand(strcheckemp, SqlConn);
            sqlselectemp.Transaction = SqlTrans;
            String empname;
            empname = Convert.ToString(sqlselectemp.ExecuteScalar());
            lblEmployee.Text = empname;
            lblEmployeeID.Text = empID;
            //to bring desc from onduty table
            string strcheckdesc = "select Description from TK_OnDuty  where OnDutyID='" + strID + "'";
            SqlCommand sqlselectdesc = new SqlCommand(strcheckdesc, SqlConn);
            sqlselectdesc.Transaction = SqlTrans;
            String desc;
            desc = Convert.ToString(sqlselectdesc.ExecuteScalar());
            txtReason.Text = desc;

            //to bring fromtime
            string strfromtime = "Select RIGHT(CONVERT(VARCHAR, FromTime, 100),7) as Time From TK_OnDuty where OnDutyID='" + strID + "'";
            SqlCommand sqlftime = new SqlCommand(strfromtime, SqlConn);
            sqlftime.Transaction = SqlTrans;
            String frmtime;
            frmtime = Convert.ToString(sqlftime.ExecuteScalar());
            ViewState["frmtime"] = frmtime;

            //to bring totime.
            string strtotime = "Select RIGHT(CONVERT(VARCHAR, ToTime, 100),7) as Time From TK_OnDuty where OnDutyID='" + strID + "'";
            SqlCommand sqlttime = new SqlCommand(strtotime, SqlConn);
            sqlttime.Transaction = SqlTrans;
            String totime;
            totime = Convert.ToString(sqlttime.ExecuteScalar());
            ViewState["totime"] = totime;

            ViewState["employeenumber"] = empID;

            Label1.Visible = true;
            Label1.Text = frmtime;
            Label2.Visible = true;
            Label2.Text = totime;
            TimeSelector1.Visible = false;
            TimeSelector2.Visible = false;


        }




        // txtFromDate.Value = Obj.FromDate.ToString("dd-MMMM-yyyy");
        //venkat txtToDate.Value = Obj.ToDate.ToString("dd-MMMM-yyyy");


        if (Obj.HalfDayOnFrom)
            //     ddlfrom.SelectedIndex = 0;
            //else
            //   ddlfrom.SelectedIndex = 1;
            if (Obj.HalfDayOnTo)
                // ddlto.SelectedIndex = 0;
                //else
                //ddlto.SelectedIndex = 1;
                lblAppliedOn.Text = Obj.ApplyDate.ToString("dd-MMMM-yyyy");
        //ddlLeavetype.SelectedValue = Obj..ToString();
        
    }
    private void LoadEntities()
    {
        try
        {
            SqlConnection SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            SqlTransaction SqlTrans = SqlConn.BeginTransaction();
            Int64 k = 0;
            string strqury = "select AttendanceID from TK_Attendance where AttendanceDate='" + lblAppliedOn.Text + "' and EmployeeID='" + lblEmployeeID.Text + "'";
            SqlCommand sqlmdc = new SqlCommand(strqury, SqlConn);
            sqlmdc.Transaction = SqlTrans;
            k = Convert.ToInt64(sqlmdc.ExecuteScalar());
            Int64 ID = Convert.ToInt64(Request.QueryString["ID"]);
            Obj1.OnDutyID = ID;
            Obj1.AttendanceID = k;
            DateTime timein = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TimeSelector1.Hour, TimeSelector1.Minute, TimeSelector1.Second, TimeSelector1.AmPm));
            DateTime timeout = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TimeSelector2.Hour, TimeSelector2.Minute, TimeSelector2.Second, TimeSelector2.AmPm));
            Obj1.FromTime = Convert.ToDateTime(timein.ToString("hh:mm:ss tt"));
            Obj1.ToTime = Convert.ToDateTime(timeout.ToString("hh:mm:ss tt"));
            Obj1.Description = txtReason.Text;
            //   Obj1.fromdate = Convert.ToDateTime(txtFromDate.ToString());
            Obj1.CompanyID = Ginf.CompanyID;

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

    //private Boolean ValidateLeaveApplication()
    //{
    //    if (Convert.ToInt32(ddlLeavetype.SelectedValue) <= 0)
    //    {
    //        lblMessage.Text = "Please Select Leave Type ...";
    //        return false;
    //    }
    //    if ((txtFromDate.Value == txtToDate.Value) && (ddlfrom.SelectedItem.Text != ddlto.SelectedItem.Text))
    //    {
    //        lblMessage.Text = "Half Day and Full Day not Valid for the Same Day ... ";
    //        return false;
    //    }

    //    //if (Obj.LeaveApplicationExists())
    //    //{
    //    //    lblMessage.Text = "Leave Entry Exists on Selected Date";

    //    //    return false;
    //    //}
    //    return true;
    //}
    //VENKAT START


    //END VENKAT
    //START VENKAT



    //END VENKAT

    protected void Button7_Click(object sender, EventArgs e)
    {

        LoadEntities();
        Employee ObjEmployee = new Employee(Obj.EmployeeID);
        SqlConnection SqlConn = GlobalFunctions.GetConnection();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
        SqlTransaction SqlTrans = SqlConn.BeginTransaction();
        string strvalidate = "select count(*) from TK_Attendance where EmployeeID='" + lblEmployeeID.Text + "' and AttendanceDate='" + lblAppliedOn.Text + "' and Intime1 is not null";
        SqlCommand cmd232 = new SqlCommand();
        cmd232.Connection = SqlTrans.Connection;
        cmd232.Transaction = SqlTrans;
        cmd232.CommandText = strvalidate;
        Int64 l = 0;
        l = Convert.ToInt64(cmd232.ExecuteScalar());

        if (l > 0)
        {
            try
            {
                String Mes = "";
                if (btnTrans.Text == "Save")
                {
                    Int64 I = Obj1.Add(SqlTrans, false);
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
                        Obj.AuthorisedBy = Ginf.EmployeeName;
                        Obj.GrantDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));
                    }

                    if (lblLevel.Text == "RO")
                    {
                        Obj.AuthorisedBy = Ginf.EmployeeName;
                        if (ObjEmployee.Reporting1IsFinal)
                        {
                            Obj.AuthorisedBy = Ginf.EmployeeName;
                            Obj.GrantDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));
                        }
                    }
                    if (lblLevel.Text == "RO2")
                    {
                        if (ObjEmployee.Reporting2IsFinal)
                        {
                            Obj.AuthorisedBy = Ginf.EmployeeName;
                            Obj.GrantDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));
                        }
                    }
                    Obj.Modify(SqlTrans, false);
                    if (btnTrans.Text == "Approve")
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
                            //SqlCmd.CommandText = "UPDATE TK_ATTENDANCE SET IsOnTour = 1, " +
                            //                    " HALFDAYLEAVE = " + Convert.ToInt32(blnH) +
                            //                    " WHERE EMPLOYEEID = " + ViewState["employeenumber"].ToString() +
                            //                    " AND ATTENDANCEDATE = '" + stD.ToString("dd/MMM/yy") + "'";
                            SqlCmd.CommandText = "update tk_onduty set ApprovedBy='" + Obj.AuthorisedBy.ToString() + "',isapproved='true' where AttendanceID='" + ViewState["attendanceID"] + "'  and OnDutyID='"+Obj1.OnDutyID.ToString()+"'";
                            SqlCmd.ExecuteNonQuery();
                         //   SqlTrans.Commit();
                        }
                    }

                    Mes = "Leave Approved Successfully";
                }
                else if (btnTrans.Text == "Reject")
                {
                    if (lblLevel.Text == "HOD")
                    {
                        Obj.RejectedDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));

                    }
                    if (lblLevel.Text == "RO")
                    {
                        Obj.RejectedDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));

                    }

                    if (lblLevel.Text == "RO2")
                    {
                        Obj.RejectedDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MMM/yy"));

                    }

                    //Obj1.Modify(SqlTrans, false);
                    string STRREJECT = "update tk_onduty set ApprovedBy='" + Obj.AuthorisedBy.ToString() + "',isrejected='true' where AttendanceID='" + ViewState["attendanceID"] + "' and OnDutyID='" + Obj1.OnDutyID.ToString() + "'";
                    SqlCommand cmdreject = new SqlCommand(STRREJECT, SqlConn);
                    cmdreject.Transaction = SqlTrans;
                    cmdreject.ExecuteNonQuery();
                   // SqlTrans.Commit();
                    Mes = "Leave Rejected Successfully";
                }
                else if (btnTrans.Text == "Remove")
                {
                    //Obj.Delete(SqlTrans, false);
                    string strondutydelete = "delete from tk_onduty where OnDutyID= '" + Obj1.OnDutyID.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(strondutydelete, SqlConn);
                    cmd.Transaction = SqlTrans;
                    cmd.ExecuteNonQuery();
                    Mes = "Record Deleted Successfully";
                }

                SqlTrans.Commit();

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
            }
            catch (Exception E)
            {
                SqlTrans.Rollback();
                lblMessage.Text = E.Message;
            }
        }
        else
        {
            lblMessage.Text = "please swipe with your access card";
        }
        SqlConn.Dispose();

    }
    private string SendHODMail()
    {
        Company ObjCompany = new Company(Ginf.CompanyID);
        

        String MailBody = ObjCompany.LeaveTemplateHTML;
        string empno = lblEmployeeID.Text;//Obj1.EmployeeID.ToString();
        string leavetypeID = objmstbase.TransID.ToString();
        string LeaveTypeName = "ONDUTY";// Convert.ToString(GlobalFill.GetQueryValue("SELECT LEAVETYPENAME FROM TK_LEAVETYPE WHERE LEAVETYPEID = " + obhleavetran.LeaveTypeID));
        string EmployeeName = Convert.ToString(GlobalFill.GetQueryValue("SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + empno));
        string Department = Convert.ToString(GlobalFill.GetQueryValue("SELECT DEPARTMENTNAME FROM TK_DEPARTMENT DM " +
             " INNER JOIN TK_SECTION SEM ON SEM.DEPARTMENTID = DM.DEPARTMENTID " +
             " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = " + empno));
        string Designation = Convert.ToString(GlobalFill.GetQueryValue("SELECT DESIGNATIONNAME FROM TK_DESIGNATION DEM" +
            " INNER JOIN TK_EMPLOYEE EM ON EM.DESIGNATIONID = DEM.DESIGNATIONID " + " WHERE EM.EMPLOYEEID = " + empno));
        Int64 ReportingEmpID1 = Convert.ToInt64(GlobalFill.GetQueryValue("SELECT REPORTINGEMPLOYEEID1 FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + empno));
        string strdesc = txtReason.Text;

        MailBody = MailBody.Replace("{LEAVETYPE}", LeaveTypeName);
        MailBody = MailBody.Replace("{REASON}", strdesc);
        MailBody = MailBody.Replace("{FROM}", Obj.FromDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{TO}", Obj.ToDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{EMPLOYEE}", EmployeeName);
        MailBody = MailBody.Replace("{DESIGNATION}", Designation);
        MailBody = MailBody.Replace("{DEPARTMENT}", Department);
        MailBody = MailBody.Replace("{COMPANYNAME}", ObjCompany.CompanyName);
        if (ReportingEmpID1 > 0)
        {
            string Reportting1mailID = Convert.ToString(GlobalFill.GetQueryValue("Select EmailID from tk_employee where EMPLOYEEID = " + ReportingEmpID1));
            return GlobalFunctions.SendEmail(Reportting1mailID, "Tour Application Submitted", Convert.ToInt64(Ginf.CompanyID), LeaveTypeName, empno, strdesc, leavetypeID, MailBody, EmployeeName, Department, Designation, Obj.FromDate.ToString("dd MMMM yyyy"), Obj.ToDate.ToString("dd MMMM yyyy"));
        }
        return "";
    }
    private string SendEmployeeMail(String Subject)
    {
        Company ObjCompany = new Company(Convert.ToInt64(Ginf.CompanyID));

        String MailBody = ObjCompany.LeaveTemplateHTML;
        string empno = lblEmployeeID.Text;//Obj1.EmployeeID.ToString();
        string leavetypeID = objmstbase.TransID.ToString();
        string LeaveTypeName = "ONDUTY";//Convert.ToString(GlobalFill.GetQueryValue("SELECT LEAVETYPENAME FROM TK_LEAVETYPE WHERE LEAVETYPEID = " + obhleavetran.LeaveTypeID));
        string EmployeeName = Convert.ToString(GlobalFill.GetQueryValue("SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + empno));
        string EmpemailID = Convert.ToString(GlobalFill.GetQueryValue("SELECT EmailID FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + empno));
        string Department = Convert.ToString(GlobalFill.GetQueryValue("SELECT DEPARTMENTNAME FROM TK_DEPARTMENT DM " +
             " INNER JOIN TK_SECTION SEM ON SEM.DEPARTMENTID = DM.DEPARTMENTID " +
             " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = " + empno));
        string Designation = Convert.ToString(GlobalFill.GetQueryValue("SELECT DESIGNATIONNAME FROM TK_DESIGNATION DEM" +
            " INNER JOIN TK_EMPLOYEE EM ON EM.DESIGNATIONID = DEM.DESIGNATIONID " + " WHERE EM.EMPLOYEEID = " + empno));
        string strdesc = txtReason.Text;

        MailBody = MailBody.Replace("{LEAVETYPE}", "ONDUTY");
        MailBody = MailBody.Replace("{REASON}", strdesc);
        MailBody = MailBody.Replace("{FROM}", Obj.FromDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{TO}", Obj.ToDate.ToString("dd MMMM yyyy"));
        MailBody = MailBody.Replace("{EMPLOYEE}", EmployeeName);
        MailBody = MailBody.Replace("{DESIGNATION}", Designation);
        MailBody = MailBody.Replace("{DEPARTMENT}", Department);
        MailBody = MailBody.Replace("{COMPANYNAME}", ObjCompany.CompanyName);

        return GlobalFunctions.SendEmail(EmpemailID, Subject, Convert.ToInt64(Ginf.CompanyID), LeaveTypeName, empno, strdesc, leavetypeID, MailBody, EmployeeName, Department, Designation, Obj.FromDate.ToString("dd MMMM yyyy"), Obj.ToDate.ToString("dd MMMM yyyy"));


    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (btnTrans.Text == "Approve" || btnTrans.Text == "Reject")
        {
            Response.Redirect("~/ApproveOnDuty.aspx");
        }
        else
        {
            Response.Redirect("~/onduty.aspx");
        }
    }

}
