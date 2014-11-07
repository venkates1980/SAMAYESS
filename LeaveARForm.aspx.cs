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
    GlobalInformation Ginf;
    LeaveTrans Obj = new LeaveTrans();
    MasterBase objmstbase = new MasterBase();
    Int64 EmployeeID;
    DataTable dt;
    string STRBODY;

    protected void Page_Load(object sender, EventArgs e)
    {
        String ltname = Request.QueryString["Leavetype"];
        String empnodetails = Request.QueryString["emp"];
        String strstatus = Request.QueryString["status"];
        String strfromdate = Request.QueryString["frmdate"];
        String strtodate = Request.QueryString["todate"];
        String strattendid = Request.QueryString[""];
        if (strstatus == "Approve" && (ltname == "CL" || ltname == "SL" || ltname == "EL" || ltname == "Compensatory Leave" || ltname == "PL" || ltname == "VL" || ltname == "Maternity Leave" || ltname == "Medical Leave" || ltname == "Festival Leave" || ltname == "Flood Relief Leaves"))
        {
            SqlConnection SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            SqlTransaction SqlTrans = SqlConn.BeginTransaction();
            string strcheck = "select count(*) from TK_LeaveTrans where EmployeeID='" + empnodetails + "' and FromDate= '" + strfromdate + "' and ToDate= '" + strtodate + "'";
            SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
            sqlselect.Transaction = SqlTrans;
            Int64 ig = 0;
            ig = Convert.ToInt64(sqlselect.ExecuteScalar());

            if (ig > 0)
            {
                try
                {
                    string str2 = "update TK_LeaveTrans SET IsApproved='true' where EmployeeID='" + empnodetails + "' and FromDate= '" + strfromdate + "' and ToDate= '" + strtodate + "'";
                    SqlCommand sqlcmdupdate = new SqlCommand(str2, SqlConn);
                    sqlcmdupdate.Transaction = SqlTrans;
                    sqlcmdupdate.ExecuteNonQuery();
                    SqlTrans.Commit();
                    lblaccept.Visible = true;
                    SendEmployeeMail("Leave Application Approved");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
            else
            {
            }
        }
        else
        {
            if (strstatus == "Reject" && (ltname == "CL" || ltname == "SL" || ltname == "EL" || ltname == "Compensatory Leave" || ltname == "PL" || ltname == "VL" || ltname == "Maternity Leave" || ltname == "Medical Leave" || ltname == "Festival Leave" || ltname == "Flood Relief Leaves"))
            {
                SqlConnection SqlConn = GlobalFunctions.GetConnection();
                if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
                SqlTransaction SqlTrans = SqlConn.BeginTransaction();
                string strcheck = "select count(*) from TK_LeaveTrans where EmployeeID='" + empnodetails + "' and FromDate= '" + strfromdate + "' and ToDate= '" + strtodate + "'";
                SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
                sqlselect.Transaction = SqlTrans;
                Int64 ir = 0;
                ir = Convert.ToInt64(sqlselect.ExecuteScalar());
                if (ir > 0)
                {
                    string strqury = "update TK_LeaveTrans set IsRORejected='true' where EmployeeID='" + empnodetails + "' and FromDate= '" + strfromdate + "' and ToDate= '" + strtodate + "'";
                    SqlCommand sqlcmdupdate = new SqlCommand(strqury,SqlConn);
                    sqlcmdupdate.Transaction = SqlTrans;
                    sqlcmdupdate.ExecuteNonQuery();
                    SqlTrans.Commit();
                    lblreject.Visible = true;
                    SendEmployeeMail("Leave Application Rejected");
                }
            }
            else
            {
            }
        }
        if (ltname == "ONDUTY" && strstatus == "Approve")
        {
            SqlConnection SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            SqlTransaction SqlTrans = SqlConn.BeginTransaction();
            string strcheck = "SELECT count(*),TK_Attendance.AttendanceID, TK_Attendance.EmployeeID, TK_OnDuty.FromTime, TK_OnDuty.ToTime, TK_OnDuty.AttendanceID AS Expr1 FROM     TK_Attendance INNER JOIN TK_OnDuty ON TK_Attendance.AttendanceID = TK_OnDuty.AttendanceID inner join TK_Employee on TK_Attendance.EmployeeID=TK_Employee.EmployeeID  where tk_attendance.EmployeeID='" + empnodetails + "' and TK_OnDuty.FromTimee='" + strfromdate + "' and TK_OnDuty.ToTime='" + strtodate + "' group by tk_attendance.AttendanceID,TK_Attendance.EmployeeID, TK_OnDuty.FromTime, TK_OnDuty.ToTime, TK_OnDuty.AttendanceID";
            SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
            sqlselect.Transaction = SqlTrans;
            Int64 ir = 0;
            ir = Convert.ToInt64(sqlselect.ExecuteScalar());
            if (ir > 0)
            {
                string str2 = "update TK_OnDuty SET IsApproved='true' where EmployeeID='" + empnodetails + "' and FromTime= '" + strfromdate + "' and ToTime= '" + strtodate + "'";
                SqlCommand sqlcmdupdate = new SqlCommand(str2, SqlConn);
                sqlcmdupdate.Transaction = SqlTrans;
                sqlcmdupdate.ExecuteNonQuery();
                SqlTrans.Commit();
                
            }
            else
            {
            }
        }
        else
        {
            if (ltname == "ONDUTY" && strstatus == "Reject")
            {
                SqlConnection SqlConn = GlobalFunctions.GetConnection();
                if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
                SqlTransaction SqlTrans = SqlConn.BeginTransaction();
                string strcheck = "SELECT count(*),TK_Attendance.AttendanceID, TK_Attendance.EmployeeID, TK_OnDuty.FromTime, TK_OnDuty.ToTime, TK_OnDuty.AttendanceID AS Expr1 FROM     TK_Attendance INNER JOIN TK_OnDuty ON TK_Attendance.AttendanceID = TK_OnDuty.AttendanceID inner join TK_Employee on TK_Attendance.EmployeeID=TK_Employee.EmployeeID  where tk_attendance.EmployeeID='" + empnodetails + "' and TK_OnDuty.FromTime='" + strfromdate + "' and TK_OnDuty.ToTime='" + strtodate + "' group by tk_attendance.AttendanceID,TK_Attendance.EmployeeID, TK_OnDuty.FromTime, TK_OnDuty.ToTime, TK_OnDuty.AttendanceID";
                SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
                sqlselect.Transaction = SqlTrans;
                Int64 ir = 0;
                ir = Convert.ToInt64(sqlselect.ExecuteScalar());
                if (ir > 0)
                {
                    string str2 = "update TK_OnDuty SET IsRejected='true' where EmployeeID='" + empnodetails + "' and FromTime= '" + strfromdate + "' and ToTime= '" + strtodate + "'";
                    SqlCommand sqlcmdupdate = new SqlCommand(str2, SqlConn);
                    sqlcmdupdate.Transaction = SqlTrans;
                    sqlcmdupdate.ExecuteNonQuery();
                    SqlTrans.Commit();
                }
                else
                {
                }
            }
        }

        if (ltname == "TOUR" && strstatus == "Approve")
        {
            SqlConnection SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            SqlTransaction SqlTrans = SqlConn.BeginTransaction();
            string strcheck = "select count(*) from TK_TourPlan where EmployeeId='"+empnodetails+"' and FromDate='"+strfromdate+"' and ToDate='"+strtodate+"'";
            SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
            sqlselect.Transaction = SqlTrans;
            Int64 ir = 0;
            ir = Convert.ToInt64(sqlselect.ExecuteScalar());
            if (ir > 0)
            {
                string str2 = "update TK_TourPlan SET GrantDate='" + Obj.FromDate.ToString("dd MMMM yyyy") + "' where EmployeeID='" + empnodetails + "' and FromDate= '" + strfromdate + "' and ToDate= '" + strtodate + "'";
                SqlCommand sqlcmdupdate = new SqlCommand(str2, SqlConn);
                sqlcmdupdate.Transaction = SqlTrans;
                sqlcmdupdate.ExecuteNonQuery();
                SqlTrans.Commit();
                SendEmployeeMail("Tour Application Accepted");
             
            }
            else
            {
            }
          
        }
        else
        {
            if (ltname == "TOUR" && strstatus == "Reject")
            {
                SqlConnection SqlConn = GlobalFunctions.GetConnection();
                if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
                SqlTransaction SqlTrans = SqlConn.BeginTransaction();
                string strcheck = "select count(*) from TK_TourPlan where EmployeeId='" + empnodetails + "' and FromDate='" + strfromdate + "' and ToDate='" + strtodate + "'";
                SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
                sqlselect.Transaction = SqlTrans;
                Int64 ir = 0;
                ir = Convert.ToInt64(sqlselect.ExecuteScalar());
                if (ir > 0)
                {
                    string str2 = "update TK_TourPlan SET RejectDate='" + Obj.FromDate.ToString("dd MMMM yyyy") + "' where EmployeeID='" + empnodetails + "' and FromDate= '" + strfromdate + "' and ToDate= '" + strtodate + "'";
                    SqlCommand sqlcmdupdate = new SqlCommand(str2, SqlConn);
                    sqlcmdupdate.Transaction = SqlTrans;
                    sqlcmdupdate.ExecuteNonQuery();
                    SqlTrans.Commit();
                    SendEmployeeMail("Tour Application Rejected");
             
                }
                else
                {
                }
            }
            else
            {
            }
        }
            // SqlConnection SqlConn = GlobalFunctions.GetConnection();
            //    if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            //    SqlTransaction SqlTrans = SqlConn.BeginTransaction();
            //string strcheck = "SELECT count(*),TK_Attendance.AttendanceID, TK_Attendance.EmployeeID, TK_OnDuty.FromTime, TK_OnDuty.ToTime, TK_OnDuty.AttendanceID AS Expr1 FROM     TK_Attendance INNER JOIN TK_OnDuty ON TK_Attendance.AttendanceID = TK_OnDuty.AttendanceID inner join TK_Employee on TK_Attendance.EmployeeID=TK_Employee.EmployeeID  where tk_attendance.EmployeeID='" + empnodetails + "' and TK_OnDuty.FromTimee='" + strfromdate + "' and TK_OnDuty.ToTime='" + strtodate + "' group by tk_attendance.AttendanceID,TK_Attendance.EmployeeID, TK_OnDuty.FromTime, TK_OnDuty.ToTime, TK_OnDuty.AttendanceID";
            //    SqlCommand sqlselect = new SqlCommand(strcheck, SqlConn);
            //    sqlselect.Transaction = SqlTrans;
            //    Int64 ir = 0;
            //    ir = Convert.ToInt64(sqlselect.ExecuteScalar());
             
            //if(ir>0)
            //{
            //}
            //else
            //{
            //}
       
        
    }
    private string SendEmployeeMail(String Subject)
    {
        Ginf = new GlobalInformation(this);
        Company ObjCompany = new Company(Convert.ToInt64(Ginf.CompanyID));
        Employee objemp = new Employee(Convert.ToInt64(Ginf.EmployeeId));
        //LeaveTrans ltv = new LeaveTrans(Convert.ToInt64(LeaveTypeName);
        String MailBody = ObjCompany.LeaveTemplateHTML;
        string empno = objemp.EmployeeID.ToString();
        string leavetypeID = objmstbase.TransID.ToString();
        string LeaveTypeName = Request.QueryString["Leavetype"];//Convert.ToString(GlobalFill.GetQueryValue("SELECT LEAVETYPENAME FROM TK_LEAVETYPE WHERE LEAVETYPEID = " + Obj.LeaveTypeID));
        string EmployeeName = Convert.ToString(GlobalFill.GetQueryValue("SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + objemp.EmployeeID));
        string EmpemailID = Convert.ToString(GlobalFill.GetQueryValue("SELECT EmailID FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + objemp.EmployeeID));
        string Department = Convert.ToString(GlobalFill.GetQueryValue("SELECT DEPARTMENTNAME FROM TK_DEPARTMENT DM " +
             " INNER JOIN TK_SECTION SEM ON SEM.DEPARTMENTID = DM.DEPARTMENTID " +
             " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = " + objemp.EmployeeID));
        string Designation = Convert.ToString(GlobalFill.GetQueryValue("SELECT DESIGNATIONNAME FROM TK_DESIGNATION DEM" +
            " INNER JOIN TK_EMPLOYEE EM ON EM.DESIGNATIONID = DEM.DESIGNATIONID " + " WHERE EM.EMPLOYEEID = " + objemp.EmployeeID));
        string fmdate = Request.QueryString["frmdate"];
        string tddate = Request.QueryString["todate"];
        string strreason = Request.QueryString["desc"];

        MailBody = MailBody.Replace("{LEAVETYPE}", LeaveTypeName);
        MailBody = MailBody.Replace("{REASON}", strreason);
        MailBody = MailBody.Replace("{FROM}", fmdate);
        MailBody = MailBody.Replace("{TO}", tddate);
        MailBody = MailBody.Replace("{EMPLOYEE}", EmployeeName);
        MailBody = MailBody.Replace("{DESIGNATION}", Designation);
        MailBody = MailBody.Replace("{DEPARTMENT}", Department);
        MailBody = MailBody.Replace("{COMPANYNAME}", ObjCompany.CompanyName);


        return GlobalFunctions.SendEmailfromHOD(EmpemailID, Subject, Convert.ToInt64(Ginf.CompanyID), LeaveTypeName, empno, strreason, leavetypeID, MailBody, EmployeeName, Department, Designation, fmdate, tddate);


    }

}
