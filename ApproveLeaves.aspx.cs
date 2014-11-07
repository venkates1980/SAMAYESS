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
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
{
    string StrSql;
    GlobalInformation GInf;
    private LeaveTrans Obj = new LeaveTrans();
        
    protected void Page_Load(object sender, EventArgs e)
    {
        GInf = new GlobalInformation(this);
        btnapprovehod.Visible = false;
        btnrejecthod.Visible = false;
    
        if (GInf.EmployeeId == 0)
        {
            Response.Redirect("~/login.aspx");
        }


        Int64 EmployeeID = Convert.ToInt64(GInf.EmployeeId);

        if (GInf.HodDeptIDStr != "")
        {
            griddata();
        }
        //if (GInf.ReportEmployeesCount > 0)
        //{
        if (!IsPostBack)
        {
            gridROdata();



            gridRO2Data();
        }
        //}
        //else
        //{
        //    gvRo.Visible = false;
        //    lblRoTitle.Visible = false;
        //}
    }
    public void griddata()
    {

        StrSql = " SELECT LTR.LEAVETRANSID , LTR.LEAVEAPPLIEDDATE, EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
           " LT.LEAVETYPENAME ,   LTR.FROMDATE , LTR.TODATE, " +
           " LTR.APPROVEDBY, LTR.AUTHORISEDDATE, " +
           " DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
           " LTR.REMARKS, CASE WHEN LTR.ISAPPROVED = 1 THEN 'APPROVED' WHEN LTR.ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS " +
           " FROM TK_LEAVETRANS LTR " +
           " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
           " INNER JOIN TK_SECTION SEM ON SEM.SECTIONID = EM.SECTIONID " +
           " INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID " +
           "  INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = LTR.LEAVETYPEID " +
           " WHERE ISAPPROVED = 0 AND ISREJECTED = 0 AND " +
            "CASE WHEN (ISROAPPROVED = 1 OR REPORTINGEMPLOYEEID1 IS NULL) THEN  " +
            "CASE WHEN (ISRO2APPROVED = 1 OR REPORTINGEMPLOYEEID2 IS NULL) THEN 1 ELSE 0 END " +
            " ELSE 0 END = 1 " +
           " AND REPORTING1ISFINAL = 0 AND REPORTING2ISFINAL = 0  " +
           "AND  DM.DEPARTMENTID IN (" + GInf.HodDeptIDStr + ") ";


        gvHod.DataSource = GlobalFill.FillDataTable(StrSql);
        gvHod.DataBind();
    }
    public void gridROdata()
    {
    //    StrSql = " SELECT LTR.LEAVETRANSID , LTR.LEAVEAPPLIEDDATE, EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
    //" LT.LEAVETYPENAME ,   LTR.FROMDATE , LTR.TODATE, " +
    //" LTR.APPROVEDBY, LTR.AUTHORISEDDATE, " +
    //" DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
    //" LTR.REMARKS, CASE WHEN LTR.ISAPPROVED = 1 THEN 'APPROVED' WHEN LTR.ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS " +
    //" FROM TK_LEAVETRANS LTR " +
    //" INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
    //" INNER JOIN TK_SECTION SEM ON SEM.SECTIONID = EM.SECTIONID " +
    //" INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID " +
    //"  INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = LTR.LEAVETYPEID " +
    //" WHERE ISAPPROVED = 0 AND ISREJECTED = 0 AND ISROAPPROVED = 0 AND ISROREJECTED = 0  " +
    //         " AND REPORTINGEMPLOYEEID1 =  " + GInf.EmployeeId.ToString();


        StrSql = " SELECT   LTR.LEAVETRANSID ,EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
    " LT.LEAVETYPENAME ,   LTR.FROMDATE , LTR.TODATE, " +
    " DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
    " CASE WHEN LTR.ISAPPROVED = 1 THEN 'APPROVED' WHEN LTR.ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS " +
    " FROM TK_LEAVETRANS LTR " +
    " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
    " INNER JOIN TK_SECTION SEM ON SEM.SECTIONID = EM.SECTIONID " +
    " INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID " +
    "  INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = LTR.LEAVETYPEID " +
    " WHERE ISAPPROVED = 0 AND ISREJECTED = 0 AND ISROAPPROVED = 0 AND ISROREJECTED = 0  " +
             " AND REPORTINGEMPLOYEEID1 =  " + GInf.EmployeeId.ToString();

        gvRo.DataSource = GlobalFill.FillDataTable(StrSql);
        gvRo.DataBind();
    }
    public void gridRO2Data()
    {
        StrSql = " SELECT LTR.LEAVETRANSID ,  EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
 " LTR.FROMDATE , LTR.TODATE, " +
// " LTR.APPROVEDBY, " +
 //" DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
 " CASE WHEN LTR.ISAPPROVED = 1 THEN 'APPROVED' WHEN LTR.ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS " +
 " FROM TK_LEAVETRANS LTR " +
 " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
 " INNER JOIN TK_SECTION SEM ON SEM.SECTIONID = EM.SECTIONID " +
 " INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID " +
 "  INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = LTR.LEAVETYPEID " +
 " WHERE ISAPPROVED = 0 AND ISREJECTED = 0 AND ISRO2APPROVED = 0 AND ISRO2REJECTED = 0 AND ISROAPPROVED = 1 " +
          " AND REPORTINGEMPLOYEEID2 =  " + GInf.EmployeeId.ToString();
        gvRo2.DataSource = GlobalFill.FillDataTable(StrSql);
        gvRo2.DataBind();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Response.Redirect("~/leaveapplication.aspx?ID=" +  + "&Mode=" + e.CommandName + "&Level=HOD");
        string strcomarg = e.CommandArgument.ToString();
        string strcomname = e.CommandName;
        ViewState["comarg"]=strcomarg;
        ViewState["comname"]=strcomname;
       // ViewState["hod"] = strlevelHOD;
        if (strcomname == "Approve")
        {
            Response.Redirect("~/leaveapplication.aspx?ID=" +e.CommandArgument.ToString()+ "&Mode=" + e.CommandName + "&Level=HOD");
        }
        else
        {
            if (strcomname == "Reject")
            {
                Response.Redirect("~/leaveapplication.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=HOD");
            }
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void gvRo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strcomname = e.CommandName;
        if (strcomname == "Approve")
        {
            Response.Redirect("~/leaveapplication.aspx?ID=" +e.CommandArgument.ToString()+ "&Mode=" + e.CommandName + "&Level=RO");
        }
        else
        {
            if (strcomname == "Reject")
            {
                Response.Redirect("~/leaveapplication.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=RO");
            }
        }

        
    }
    protected void gvRo2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("~/leaveapplication.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=RO2");
    }
    protected void gvHod_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHod.PageIndex = e.NewPageIndex;
        griddata();
    }

    protected void gvRo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRo.PageIndex = e.NewPageIndex;
        gridROdata();
    }
    protected void btnapprovehod_Click(object sender, EventArgs e)
    {
        GInf = new GlobalInformation(this);
        Int64 EmployeeID = Convert.ToInt64(GInf.EmployeeId);
        CheckBox chkAll = (gvRo.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvRo.Rows)
        {
            //if (row.RowType == DataControlRowType.DataRow)
            //{
            //  bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().Single().Checked;
            //for (int i = 0; i < gvRo.Rows.Count; i++)
            //{
            //CheckBox chk = (CheckBox)gvRo.Rows[i].Cells[0].FindControl("CheckBox1");
            bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().Single().Checked;
            if (isChecked)
            {
                DateTime stD = DateTime.Parse(gvRo.Rows[row.RowIndex].Cells[6].Text);
                DateTime toD = DateTime.Parse(gvRo.Rows[row.RowIndex].Cells[7].Text);

                for (stD = stD; stD <= toD; stD = stD.AddDays(1))
                {
                    SqlConnection SqlConn = GlobalFunctions.GetConnection();
                    if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
                    SqlTransaction SqlTrans = SqlConn.BeginTransaction();
                    SqlCommand SqlCmd = new SqlCommand();
                    SqlCmd.Connection = SqlTrans.Connection; // GlobalFunctions.GetConnection();
                    SqlCmd.Transaction = SqlTrans;

                    string noofdaysonleave = gvRo.Rows[row.RowIndex].Cells[8].Text;
                    string leavetypename = gvRo.Rows[row.RowIndex].Cells[5].Text;
                    SqlCmd.CommandText = "select LeaveTypeID from TK_LeaveType where LeaveTypeName ='" + leavetypename + "'";
                    Int16 leavetypeid = Convert.ToInt16(SqlCmd.ExecuteScalar());

                    Int16 blnH;
                    if (noofdaysonleave.ToString() == "0.5")
                    {
                        blnH = 1;
                    }
                    else
                    {
                        blnH = 0;
                    }
                    string leavetypeno = gvRo.Rows[row.RowIndex].Cells[3].Text;
                    SqlCmd.CommandText = "select EmployeeID from TK_LeaveTrans where LeaveTransID='" + leavetypeno + "'";
                    Int64 employeeno = Convert.ToInt64(SqlCmd.ExecuteScalar());

                    SqlCmd.CommandText = "select EmployeeFirstName+ ' ' +EmployeeLastName from TK_Employee where EmployeeID='" + EmployeeID + "'";
                    string employeename = Convert.ToString(SqlCmd.ExecuteScalar());

                    SqlCmd.CommandText = "UPDATE TK_Leavetrans SET IsApproved = 1,ApprovedBy= '" + employeename + "', " +
                                             " AUTHORISEDDATE = '" + Obj.FromDate + "'" +
                                             " WHERE LeaveTransID = " + leavetypeno;

                    SqlCmd.ExecuteNonQuery();

                    SqlCmd.CommandText = "select WeeklyOff from TK_Attendance where EmployeeID='" + employeeno + "' and AttendanceDate='" + stD.ToString("MM/dd/yyyy") + "'";
                    Boolean weeklyoffstatus = Convert.ToBoolean(SqlCmd.ExecuteScalar());

                    if (weeklyoffstatus == true)
                    {
                        SqlCmd.CommandText = "UPDATE TK_ATTENDANCE SET ISONLEAVE =0,HALFDAY =0, " +
                                                                 " HALFDAYLEAVE = " + blnH + "," +
                                                                 " LEAVETYPEID =  " + leavetypeid +
                                                                 " WHERE EMPLOYEEID = " + employeeno +
                            // " AND ATTENDANCEDATE = '" + Obj.FromDate + "'";
                            //" AND ATTENDANCEDATE = cast('" + frmdate.ToString("dd/MMM/yy") + "' as DateTime)";
                            //" AND ATTENDANCEDATE = '" + stD + "'";
                   " AND ATTENDANCEDATE = '" + stD.ToString("MM/dd/yyyy") + "'";
                        SqlCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCmd.CommandText = "UPDATE TK_ATTENDANCE SET ISONLEAVE =1,HALFDAY =0, " +
                                                                  " HALFDAYLEAVE = " + blnH + "," +
                                                                  " LEAVETYPEID =  " + leavetypeid +
                                                                  " WHERE EMPLOYEEID = " + employeeno +
                            // " AND ATTENDANCEDATE = '" + Obj.FromDate + "'";
                            //" AND ATTENDANCEDATE = cast('" + frmdate.ToString("dd/MMM/yy") + "' as DateTime)";
                            //" AND ATTENDANCEDATE = '" + stD + "'";
                    " AND ATTENDANCEDATE = '" + stD.ToString("MM/dd/yyyy") + "'";
                        SqlCmd.ExecuteNonQuery();

                    }
                                        SqlTrans.Commit();

                }
            }
            else
            {
                DateTime stD = DateTime.Parse(gvRo.Rows[row.RowIndex].Cells[6].Text);
                DateTime toD = DateTime.Parse(gvRo.Rows[row.RowIndex].Cells[7].Text);

                for (stD = stD; stD <= toD; stD = stD.AddDays(1))
                {



                    SqlConnection SqlConn = GlobalFunctions.GetConnection();
                    if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
                    SqlTransaction SqlTrans = SqlConn.BeginTransaction();
                    SqlCommand SqlCmd = new SqlCommand();
                    SqlCmd.Connection = SqlTrans.Connection; // GlobalFunctions.GetConnection();
                    SqlCmd.Transaction = SqlTrans;

                    string noofdaysonleave = gvRo.Rows[row.RowIndex].Cells[8].Text;
                    string leavetypename = gvRo.Rows[row.RowIndex].Cells[5].Text;
                    SqlCmd.CommandText = "select LeaveTypeID from TK_LeaveType where LeaveTypeName ='" + leavetypename + "'";
                    Int16 leavetypeid = Convert.ToInt16(SqlCmd.ExecuteScalar());

                    Int16 blnH;
                    if (noofdaysonleave.ToString() == "0.5")
                    {
                        blnH = 1;
                    }
                    else
                    {
                        blnH = 0;
                    }
                    string leavetypeno = gvRo.Rows[row.RowIndex].Cells[3].Text;
                    SqlCmd.CommandText = "select EmployeeID from TK_LeaveTrans where LeaveTransID='" + leavetypeno + "'";
                    Int64 employeeno = Convert.ToInt64(SqlCmd.ExecuteScalar());

                    SqlCmd.CommandText = "select EmployeeFirstName+ ' ' +EmployeeLastName from TK_Employee where EmployeeID='" + EmployeeID + "'";
                    string employeename = Convert.ToString(SqlCmd.ExecuteScalar());

                    SqlCmd.CommandText = "UPDATE TK_Leavetrans SET IsApproved = 1,ApprovedBy= '" + employeename + "', " +
                                             " AUTHORISEDDATE = '" + Obj.FromDate + "'" +
                                             " WHERE LeaveTransID = " + leavetypeno;

                    SqlCmd.ExecuteNonQuery();


                    //SqlCmd.CommandText="select employeeid from tk_leavetrans where 

                    SqlCmd.CommandText = "UPDATE TK_ATTENDANCE SET ISONLEAVE =1,HALFDAY =0, " +
                                                                  " HALFDAYLEAVE = " + blnH + "," +
                                                                  " LEAVETYPEID =  " + leavetypeid +
                                                                  " WHERE EMPLOYEEID = " + employeeno +
                        //  " AND ATTENDANCEDATE = '" + Obj.FromDate + "'";
                        //" AND ATTENDANCEDATE = cast('" + frmdate.ToString("dd/MMM/yy") + "' as DateTime)";
                        //" AND ATTENDANCEDATE = '" + stD + "'";
                    " AND ATTENDANCEDATE = '" + stD.ToString("MM/dd/yyyy") + "'";
                    SqlCmd.ExecuteNonQuery();
                    SqlTrans.Commit();

                }
            }
            //break;
        }

        gridROdata();
        btnapprovehod.Visible = false;
        btnrejecthod.Visible = false;

    }
    protected void btnrejecthod_Click(object sender, EventArgs e)
    {

    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvRo.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in gvRo.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }



    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvRo.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                    btnapprovehod.Visible = true;
                    btnrejecthod.Visible = false;
                }
            }
        }
        CheckBox chkAll = (gvRo.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvRo.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                //for (int i = 1; i < row.Cells.Count; i++)
                //{
                    if (isChecked)
                    {
                        btnapprovehod.Visible = true;
                        break;
                        //btnrejecthod.Visible = false;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;
                        btnapprovehod.Visible = false;

                    }
                //}
            }
        }
    }


    protected void gvRo_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
