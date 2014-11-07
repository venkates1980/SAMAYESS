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


public partial class ShowCompanyImage : System.Web.UI.Page
{
    GlobalInformation GInf;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Id"] != null)
        {
            string StrSql = "";
            //StrSql = "SELECT COMPANYLOGO FROM TK_Company  WHERE EMPLOYEEID = " + Convert.ToString(Request.QueryString["Id"]);
            StrSql = "SELECT TK_Company.COMPANYLOGO FROM TK_Company INNER JOIN TK_Employee ON TK_Company.CompanyID = TK_Employee.CompanyID where TK_Company.CompanyID='"+GInf.CompanyID+"' and TK_Employee.EmployeeCode=" + Convert.ToString(Request.QueryString["Id"]);


            Response.ContentType = "image/jpg";
            if (GlobalFill.GetQueryValue(StrSql) != System.DBNull.Value)
                Response.BinaryWrite((byte[])GlobalFill.GetQueryValue(StrSql));

        }
    }
}