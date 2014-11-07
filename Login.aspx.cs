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
    
    GlobalInformation GInf ;
    protected void Page_Load(object sender, EventArgs e)
    {
        GInf  = new GlobalInformation (this);
       Page.Title = "SamayBio Employee Self Service";

    }
    //protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    //{
        
    //}
    //protected void Login1_Authenticate1(object sender, AuthenticateEventArgs e)
    //{
    //    String StrSql = "SELECT EMPLOYEEID FROM TK_EMPLOYEE  WHERE EMPLOYEECODE   = '" + Login1.UserName + "' AND KPWD = '" + Login1.Password + "' and INACTIVE='0'";
    //    Int64 LoginId = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

    //    //if (LoginId == 0)
    //    //{
    //    //    StrSql = "SELECT EMPLOYEEID FROM TK_EMPLOYEE  WHERE EMPLOYEECODE   = '" + Login1.UserName + "' AND CONVERT(VARCHAR,DOJ,12) = '" + Login1.Password + "'";
    //    //    LoginId = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));
    //    //}

    //    if (LoginId > 0)
    //    {
    //        GInf.EmployeeId = LoginId;

    //        StrSql = "SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
    //        GInf.EmployeeName = Convert.ToString(GlobalFill.GetQueryValue(StrSql));
           


    //        StrSql = "SELECT COMPANYID FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
    //        GInf .CompanyID  = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));
           
    //        StrSql = "SELECT HODDEPTIDSTR FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
    //        GInf.HodDeptIDStr  = Convert.ToString(GlobalFill.GetQueryValue(StrSql));

    //        StrSql = "SELECT TOP 1 CALENDARYEARID FROM TK_CALENDARYEAR  WHERE ISDEFAULT = 1 AND COMPANYID = " + GInf.CompanyID  + "  ORDER BY  STARTINGDATE DESC";
    //        GInf.CalendarYearID  = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

    //        StrSql = "SELECT ISNULL(COUNT(*),0) FROM TK_EMPLOYEE WHERE REPORTINGEMPLOYEEID1 = " + LoginId + " OR REPORTINGEMPLOYEEID2 = " + LoginId;
    //        GInf.ReportEmployeesCount  = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));



    //        GInf.Upload(this);

    //        string RedirectUrl = "~/Profile.aspx";
    //        Response.Redirect(RedirectUrl);

    //    }
    //}
    //protected void btnlogin_Click(object sender, EventArgs e)
    //{
    //    String StrSql = "SELECT EMPLOYEEID FROM TK_EMPLOYEE  WHERE EMPLOYEECODE   = '" + txtusername.Text+ "' AND KPWD = '" + txtpwd.Text+ "' and INACTIVE='0'";
    //    Int64 LoginId = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

    //    //if (LoginId == 0)
    //    //{
    //    //    StrSql = "SELECT EMPLOYEEID FROM TK_EMPLOYEE  WHERE EMPLOYEECODE   = '" + Login1.UserName + "' AND CONVERT(VARCHAR,DOJ,12) = '" + Login1.Password + "'";
    //    //    LoginId = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));
    //    //}

    //    if (LoginId > 0)
    //    {
    //        GInf.EmployeeId = LoginId;

    //        StrSql = "SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
    //        GInf.EmployeeName = Convert.ToString(GlobalFill.GetQueryValue(StrSql));



    //        StrSql = "SELECT COMPANYID FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
    //        GInf.CompanyID = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

    //        StrSql = "SELECT HODDEPTIDSTR FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
    //        GInf.HodDeptIDStr = Convert.ToString(GlobalFill.GetQueryValue(StrSql));

    //        StrSql = "SELECT TOP 1 CALENDARYEARID FROM TK_CALENDARYEAR  WHERE ISDEFAULT = 1 AND COMPANYID = " + GInf.CompanyID + "  ORDER BY  STARTINGDATE DESC";
    //        GInf.CalendarYearID = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

    //        StrSql = "SELECT ISNULL(COUNT(*),0) FROM TK_EMPLOYEE WHERE REPORTINGEMPLOYEEID1 = " + LoginId + " OR REPORTINGEMPLOYEEID2 = " + LoginId;
    //        GInf.ReportEmployeesCount = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));



    //        GInf.Upload(this);

    //        string RedirectUrl = "~/Profile.aspx";
    //        Response.Redirect(RedirectUrl);
    //    }
    //    lblerror.Visible = true;
    //    lblerror.Text = "Please Contact Administrator";
        
    //}
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        String StrSql = "SELECT EMPLOYEEID FROM TK_EMPLOYEE  WHERE EMPLOYEECODE   = '" + txtusername.Text + "' AND KPWD = '" + txtpwd.Text + "' and INACTIVE='0'";
        Int64 LoginId = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

        //if (LoginId == 0)
        //{
        //    StrSql = "SELECT EMPLOYEEID FROM TK_EMPLOYEE  WHERE EMPLOYEECODE   = '" + Login1.UserName + "' AND CONVERT(VARCHAR,DOJ,12) = '" + Login1.Password + "'";
        //    LoginId = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));
        //}

        if (LoginId > 0)
        {
            GInf.EmployeeId = LoginId;

            StrSql = "SELECT EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
            GInf.EmployeeName = Convert.ToString(GlobalFill.GetQueryValue(StrSql));



            StrSql = "SELECT COMPANYID FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
            GInf.CompanyID = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

            StrSql = "SELECT HODDEPTIDSTR FROM TK_EMPLOYEE WHERE EMPLOYEEID = " + LoginId;
            GInf.HodDeptIDStr = Convert.ToString(GlobalFill.GetQueryValue(StrSql));

            StrSql = "SELECT TOP 1 CALENDARYEARID FROM TK_CALENDARYEAR  WHERE ISDEFAULT = 1 AND COMPANYID = " + GInf.CompanyID + "  ORDER BY  STARTINGDATE DESC";
            GInf.CalendarYearID = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));

            StrSql = "SELECT ISNULL(COUNT(*),0) FROM TK_EMPLOYEE WHERE REPORTINGEMPLOYEEID1 = " + LoginId + " OR REPORTINGEMPLOYEEID2 = " + LoginId;
            GInf.ReportEmployeesCount = Convert.ToInt64(GlobalFill.GetQueryValue(StrSql));



            GInf.Upload(this);

            string RedirectUrl = "~/Profile.aspx";
            Response.Redirect(RedirectUrl);
        }
        lblerror.Visible = true;
        lblerror.Text = "Please Contact Administrator";
        
    }
}
