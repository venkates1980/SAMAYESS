using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for UserInfo
/// </summary>
public class GlobalInformation
{
    Int64 lEmployeeID;
    Int64 lCompanyID;
   Int64 lCalendarYearID;
   string lHodDeptIDstr = "";
    Boolean blnVarified;
    string strPrvPageUrl;
    Int64  lReporting1EmployeeID;
    Int64 lReportEmployeesCount = 0;
    string lEmployeeName = "";


    public GlobalInformation(Page pg)
    {
        HttpCookie hcEmployeeId = pg.Request.Cookies["EmployeeID"];
        HttpCookie hcEmployeeName = pg.Request.Cookies["EmployeeName"];
        HttpCookie hcComapnyID = pg.Request.Cookies["CompanyID"];
        HttpCookie hcCalendarYearID = pg.Request.Cookies["CalendarYearID"];
        HttpCookie hcHodDeptIDStr = pg.Request.Cookies["HodDeptIDStr"];
        HttpCookie hcPwdVarified = pg.Request.Cookies["PwdVerified"];
        HttpCookie hcPrvPageUrl = pg.Request.Cookies["PrvPageUrl"];
        HttpCookie hcReporting1EmployeeID = pg.Request.Cookies["Reporting1EmployeeID"];
        HttpCookie hcReportEmployeesCount = pg.Request.Cookies["ReportEmployeesCount"];

        if (hcEmployeeName  != null)
        { lEmployeeName  = Convert.ToString(hcEmployeeName .Value); }

        if (hcReportEmployeesCount  != null)
        { lEmployeeID = Convert.ToInt64(hcEmployeeId.Value); }


        if (hcEmployeeId  != null)
        { lEmployeeID  = Convert.ToInt64(hcEmployeeId.Value); }

        if (hcReporting1EmployeeID  != null)
        { lReporting1EmployeeID  = Convert.ToInt64(hcReporting1EmployeeID.Value); }

        if (hcComapnyID   != null)
        { lCompanyID   = Convert.ToInt64(hcComapnyID.Value); }

        if (hcCalendarYearID    != null)
        { lCalendarYearID   = Convert.ToInt64(hcCalendarYearID .Value); }
        if (hcPwdVarified != null)
        { blnVarified = Convert.ToBoolean(hcPwdVarified.Value); }
        if (hcPrvPageUrl != null)
        { strPrvPageUrl = Convert.ToString(hcPrvPageUrl.Value); }

        if (hcHodDeptIDStr != null)
        { lHodDeptIDstr = Convert.ToString(hcHodDeptIDStr.Value); }
    }

    public GlobalInformation(UserControl pg)
    {
        HttpCookie hcEmployeeId = pg.Request.Cookies["EmployeeID"];
        HttpCookie hcEmployeeName = pg.Request.Cookies["EmployeeName"];
        HttpCookie hcComapnyID = pg.Request.Cookies["CompanyID"];
        HttpCookie hcCalendarYearID = pg.Request.Cookies["CalendarYearID"];
        HttpCookie hcHodDeptIDStr = pg.Request.Cookies["HodDeptIDStr"];
        HttpCookie hcPwdVarified = pg.Request.Cookies["PwdVerified"];
        HttpCookie hcPrvPageUrl = pg.Request.Cookies["PrvPageUrl"];
        HttpCookie hcReporting1EmployeeID = pg.Request.Cookies["Reporting1EmployeeID"];
        HttpCookie hcReportEmployeesCount = pg.Request.Cookies["ReportEmployeesCount"];

        if (hcReportEmployeesCount  != null)
        { lReportEmployeesCount  = Convert.ToInt64(hcReportEmployeesCount .Value); }

        if (hcEmployeeName  != null)
        { lEmployeeName  = Convert.ToString(hcEmployeeName .Value); }


        if (hcEmployeeId != null)
        { lEmployeeID = Convert.ToInt64(hcEmployeeId.Value); }

        if (hcReporting1EmployeeID != null)
        { lReporting1EmployeeID = Convert.ToInt64(hcReporting1EmployeeID.Value); }

        if (hcComapnyID != null)
        { lCompanyID = Convert.ToInt64(hcComapnyID.Value); }

        if (hcCalendarYearID != null)
        { lCalendarYearID = Convert.ToInt64(hcCalendarYearID.Value); }
        if (hcPwdVarified != null)
        { blnVarified = Convert.ToBoolean(hcPwdVarified.Value); }
        if (hcPrvPageUrl != null)
        { strPrvPageUrl = Convert.ToString(hcPrvPageUrl.Value); }
        if (hcHodDeptIDStr != null)
        { lHodDeptIDstr = Convert.ToString(hcHodDeptIDStr.Value); }
    }

    public Int64 EmployeeId
    {
        get { return lEmployeeID ; }
        set { lEmployeeID  = value; }
    }

    public string  EmployeeName
    {
        get { return lEmployeeName ; }
        set { lEmployeeName  = value; }
    }

    public Int64 ReportEmployeesCount
    {
        get { return lReportEmployeesCount ; }
        set { lReportEmployeesCount  = value; }
    }


    public Int64 Reporting1EmployeeId
    {
        get { return lReporting1EmployeeID ; }
        set { lReporting1EmployeeID  = value; }
    }


    public Int64 CompanyID
    {
        get { return lCompanyID ; }
        set { lCompanyID  = value; }
    }

    public Int64 CalendarYearID
    {
        get { return  lCalendarYearID ; }
        set { lCalendarYearID  = value; }
    }

    public String  HodDeptIDStr
    {
        get { return lHodDeptIDstr ; }
        set { lHodDeptIDstr  = value; }
    }

    public Boolean PasswordVarified
    {
        get { return blnVarified; }
        set { blnVarified = value; }
    }

    public string PrevPageUrl
    {
        get { return strPrvPageUrl; }
        set { strPrvPageUrl = value; }
    }

    public void Upload(Page pg)
    {
        HttpCookie hcEmployeeID = new HttpCookie("EmployeeID", Convert.ToString(lEmployeeID ));
        HttpCookie hcEmployeeName = new HttpCookie("EmployeeName", Convert.ToString(lEmployeeName ));
        HttpCookie hcCompanyID = new HttpCookie("CompanyID", Convert.ToString(lCompanyID ));
        HttpCookie hcCalendarYearID = new HttpCookie("CalendarYearID", Convert.ToString(lCalendarYearID ));
        HttpCookie hcHodDeptIDStr = new HttpCookie("HodDeptIDStr", Convert.ToString(lHodDeptIDstr ));
        HttpCookie hcPwdVerified = new HttpCookie("PwdVerified", Convert.ToString(blnVarified));
        HttpCookie hcPrvPageUrl = new HttpCookie("PrvPageUrl", Convert.ToString(strPrvPageUrl));
        HttpCookie hcReporting1EmployeeID = new HttpCookie("Reporting1EmployeeID", Convert.ToString(lReporting1EmployeeID ));
        HttpCookie hcReportEmployeesCount = new HttpCookie("ReportEmployeesCount", Convert.ToString(lReportEmployeesCount ));

        pg.Response.Cookies.Add(hcEmployeeID );
        pg.Response.Cookies.Add(hcEmployeeName );
        pg.Response.Cookies.Add(hcCompanyID );
        pg.Response.Cookies.Add(hcCalendarYearID );
        pg.Response.Cookies.Add(hcHodDeptIDStr );
        pg.Response.Cookies.Add(hcReporting1EmployeeID );
        pg.Response.Cookies.Add(hcReportEmployeesCount );
    }

    public void Upload(MasterPage pg)
    {
        HttpCookie hcEmployeeID = new HttpCookie("EmployeeID", Convert.ToString(lEmployeeID));
        HttpCookie hcEmployeeName = new HttpCookie("EmployeeName", Convert.ToString(lEmployeeName ));
        HttpCookie hcCompanyID = new HttpCookie("CompanyID", Convert.ToString(lCompanyID));
        HttpCookie hcCalendarYearID = new HttpCookie("CalendarYearID", Convert.ToString(lCalendarYearID));
        HttpCookie hcHodDeptIDStr = new HttpCookie("HodDeptIDStr", Convert.ToString(lHodDeptIDstr));
        HttpCookie hcPwdVerified = new HttpCookie("PwdVerified", Convert.ToString(blnVarified));
        HttpCookie hcPrvPageUrl = new HttpCookie("PrvPageUrl", Convert.ToString(strPrvPageUrl));
        HttpCookie hcReporting1EmployeeID = new HttpCookie("Reporting1EmployeeID", Convert.ToString(lReporting1EmployeeID));
        HttpCookie hcReportEmployeesCount = new HttpCookie("ReportEmployeesCount", Convert.ToString(lReportEmployeesCount ));

        pg.Response.Cookies.Add(hcEmployeeID);
        pg.Response.Cookies.Add(hcEmployeeName );
        pg.Response.Cookies.Add(hcCompanyID);
        pg.Response.Cookies.Add(hcCalendarYearID);
        pg.Response.Cookies.Add(hcHodDeptIDStr );
        pg.Response.Cookies.Add(hcReportEmployeesCount );
    }

    //public Int64 GetEmployee(string EmpCode, string Pwd)
    //{
    //    string StrSql = "";
    //    SqlConnection sConn = GlobalFunctions.GetConnection();
    //    sConn.Open();
    //    StrSql = "SELECT ISNULL(EMPLOYEEID, 0) FROM TK_EMPLOYEE WHERE EMPLOYEECODE = '" + EmpCode + "' AND KPWD = '" + Pwd + "'";
    //    SqlCommand SQLC = new SqlCommand(StrSql, sConn);
    //    Int64 EmpId = Convert.ToInt64(SQLC.ExecuteScalar());
    //    sConn.Close();
    //    sConn.Dispose();
    //    SQLC.Dispose();
    //    return EmpId;
    //}

    //public void SetEmployeeKPwd(Int64 EmpId, string Pwd)
    //{
    //    try.
    //    {
    //        string StrSql = "";
    //        SqlConnection sConn = GlobalFunctions.GetConnection();
    //        sConn.Open();
    //        StrSql = "UPDATE TK_EMPLOYEE SET KPWD = '" + Pwd + "' WHERE EMPLOYEEID = " + EmpId.ToString();
    //        SqlCommand SQLC = new SqlCommand(StrSql, sConn);
    //        SQLC.ExecuteNonQuery();
    //        sConn.Close();
    //        sConn.Dispose();
    //        SQLC.Dispose();
    //    }
    //    catch (Exception E)
    //    {
    //        throw E;
    //    }

    //}
}


