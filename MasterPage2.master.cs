using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;

public partial class MasterPage2 : System.Web.UI.MasterPage
{

    GlobalInformation GInf;
    protected void Page_Load(object sender, EventArgs e)
    {
        GInf = new GlobalInformation(this);
        string HodDeptIDstr = GInf.HodDeptIDStr;
        if (HodDeptIDstr == "")
        {
            btnLeaveApproval.Visible = false;
          //  btnTourApproval.Visible = false;
            btnPresentReport.Visible = false;
            btnAbsentReport.Visible = false;
            btnArrivalReport.Visible = false;
           // btnTourApproval.Visible = false;
           // btnODApproval.Visible = false;
            //btnLateReport.Visible = false;
        }
        else
        {
            btnLeaveApproval.Visible = true ;
            //venkat modified true to false
          //  btnODApproval.Visible = true;
           // btnTourApproval.Visible = true;
            //btnTourApproval.Visible = true ;
            btnPresentReport.Visible = true ;
            btnAbsentReport.Visible = true ;
            btnArrivalReport.Visible = true;
            //btnLateReport.Visible = true ;
        }

        if (GInf.ReportEmployeesCount > 0)
        {
            btnLeaveApproval.Visible = true;
         //venkat modified true to false
            //btnODApproval.Visible = true;
           // btnTourApproval.Visible = true;
        }



       lblEmployeeName.Text = GInf.EmployeeName;
      String StrSql = "SELECT COMPANYNAME FROM TK_COMPANY WHERE COMPANYID = " + GInf.CompanyID;
      lblCompany.Text = Convert.ToString(GlobalFill.GetQueryValue(StrSql));
    
        //string StrSql = "";
      //StrSql = "SELECT COMPANYLOGO FROM TK_Company  WHERE EMPLOYEEID = " + Convert.ToString(Request.QueryString["Id"]);
      


      //Response.ContentType = "image/jpg";
      //if (GlobalFill.GetQueryValue(StrSql) != System.DBNull.Value)
        //  Response.BinaryWrite((byte[])GlobalFill.GetQueryValue(StrSql));

      SqlConnection SqlConn = GlobalFunctions.GetConnection();
      if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
      SqlTransaction SqlTrans = SqlConn.BeginTransaction();
      SqlCommand SqlCmd = new SqlCommand();
      SqlCmd.Connection = SqlTrans.Connection; // GlobalFunctions.GetConnection();
      SqlCmd.Transaction = SqlTrans;

      Int64 EmployeeID = GInf.EmployeeId;
      SqlCmd.CommandText = "select EmployeeCode from TK_Employee where EmployeeID='" + GInf.EmployeeId + "'";
      Int16 empcode = Convert.ToInt16(SqlCmd.ExecuteScalar());

      StrSql = "SELECT TK_Company.COMPANYLOGO FROM TK_Company INNER JOIN TK_Employee ON TK_Company.CompanyID = TK_Employee.CompanyID where TK_Company.CompanyID='" + GInf.CompanyID + "' and TK_Employee.EmployeeCode='" + empcode + "'";
      string imagebinary = Convert.ToString(SqlCmd.ExecuteScalar());

      byte[] raw = ((byte[])GlobalFill.GetQueryValue(StrSql));
      string base64String = Convert.ToBase64String(raw, 0, raw.Length);
      Image1.ImageUrl = "data:image/png;base64," + base64String;


      //byte[] raw = ((byte[])GlobalFill.GetQueryValue(StrSql));
      //Image1.ImageUrl = "~/" + raw;
      //Response.ContentType = "image/jpg";
      //if (GlobalFill.GetQueryValue(StrSql) != System.DBNull.Value)
      //Response.BinaryWrite((byte[])GlobalFill.GetQueryValue(StrSql));
    
    }
    protected void Button13_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        string RedirectUrl = "~/AttendanceView.aspx";
        Response.Redirect(RedirectUrl);
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        
        Response.Redirect( "~/login.aspx");
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        
        string RedirectUrl = "~/Profile.aspx";
        Response.Redirect(RedirectUrl);
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        string RedirectUrl = "~/leaves.aspx";
        Response.Redirect(RedirectUrl);
    }
    protected void btnPresentReport_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("~/presentreport.aspx");
    }
    protected void btnLeaveApproval_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ApproveLeaves.aspx");
    }
    protected void btnAbsentReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/absentreport.aspx");
    }
    protected void Button13_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/changepwd.aspx");
    }
    protected void btnPresentReport0_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Arrivalreport.aspx");
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        string RedirectUrl = "~/HolidayView.aspx";
        Response.Redirect(RedirectUrl);

    }
    protected void btnOD_Click(object sender, EventArgs e)
    {
        string RedirectUrl = "~/onduty.aspx";
        Response.Redirect(RedirectUrl);
    }
    protected void Button14_Click(object sender, EventArgs e)
    {

        string RedirectUrl = "~/tours.aspx";
        Response.Redirect(RedirectUrl);
    }
    protected void btnTourApproval_Click(object sender, EventArgs e)
    {
        string RedirectUrl = "~/ApproveTour.aspx";
        Response.Redirect(RedirectUrl);
    }
    protected void btnODApproval_Click(object sender, EventArgs e)
    {
        string RedirectUrl = "~/ApproveOnDuty.aspx";
        Response.Redirect(RedirectUrl);

    }
}
