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
    string StrSql;
    GlobalInformation GInf;
    protected void Page_Load(object sender, EventArgs e)
    {
        GInf = new GlobalInformation(this);

        if (GInf.EmployeeId == 0)
        {
            Response.Redirect("~/login.aspx");
        }


        Int64 EmployeeID = Convert.ToInt64(GInf.EmployeeId);

        if (GInf.HodDeptIDStr != "")
        {
            StrSql = " SELECT LTR.LEAVETRANSID , EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
            " LT.LEAVETYPENAME ,   LTR.FROMDATE , LTR.TODATE, " +
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
            //StrSql = "select OnDutyID as ID, FromTime,ToTime,Description,CASE WHEN COALESCE(isapproved,'')='' THEN 'APPROVAL PENDING'  END AS STATUS from TK_OnDuty where isapproved is null AND year(FromTime)=YEAR(getdate())  order by FromTime desc";

            gvHod.DataSource = GlobalFill.FillDataTable(StrSql);
            gvHod.DataBind();
        }

        //    StrSql = " SELECT LTR.LEAVETRANSID , EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
        //" LT.LEAVETYPENAME ,   LTR.FROMDATE , LTR.TODATE, " +
        //" DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
        //" LTR.REMARKS, CASE WHEN LTR.ISAPPROVED = 1 THEN 'APPROVED' WHEN LTR.ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS " +
        //" FROM TK_LEAVETRANS LTR " +
        //" INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
        //" INNER JOIN TK_SECTION SEM ON SEM.SECTIONID = EM.SECTIONID " +
        //" INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID " +
        //"  INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = LTR.LEAVETYPEID " +
        //" WHERE ISAPPROVED = 0 AND ISREJECTED = 0 AND ISROAPPROVED = 0 AND ISROREJECTED = 0  " +
        //         " AND REPORTINGEMPLOYEEID1 =  " + GInf.EmployeeId.ToString();
        //StrSql = "select OnDutyID as ID, FromTime,ToTime,Description,isapproved as status from TK_OnDuty where isapproved is null AND year(FromTime)=YEAR(getdate())  order by FromTime desc";
        StrSql = "select OnDutyID as ID, FromTime,ToTime,Description,CASE WHEN ISAPPROVED ='FALSE' THEN 'APPROVAL PENDING' when isrejected='false' then 'APPROVAL PENDING'  ELSE 'APPROVAL PENDING' END AS STATUS from TK_OnDuty where isapproved is null AND isrejected is null and year(FromTime)=YEAR(getdate())  order by FromTime desc";
        gvRo.DataSource = GlobalFill.FillDataTable(StrSql);
        gvRo.DataBind();



        StrSql = " SELECT LTR.LEAVETRANSID , EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
   " LT.LEAVETYPENAME ,   LTR.FROMDATE , LTR.TODATE, " +
   " DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
   " LTR.REMARKS, CASE WHEN LTR.ISAPPROVED = 1 THEN 'APPROVED' WHEN LTR.ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS " +
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
        Response.Redirect("~/ApplyOnDuty.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=HOD");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void gvRo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("~/ApplyOnDuty.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=RO");
    }
    protected void gvRo2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("~/ApplyOnDuty.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=RO2");
    }
}
