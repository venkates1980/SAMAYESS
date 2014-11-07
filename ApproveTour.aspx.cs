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
    GlobalInformation GInf;
    protected void Page_Load(object sender, EventArgs e)
    {
        GInf = new GlobalInformation(this);

        if (GInf.EmployeeId  == 0)
        {
            Response.Redirect("~/login.aspx");
        }


     Int64    EmployeeID = Convert.ToInt64(GInf.EmployeeId );
     string StrSql;
     if (GInf.HodDeptIDStr != "")
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
         " AND REPORTING1ISFINAL = 0 AND REPORTING2ISFINAL = 0  "+
         "AND  DM.DEPARTMENTID IN (" + GInf.HodDeptIDStr + ") ";


         gvHod.DataSource = GlobalFill.FillDataTable(StrSql);
         gvHod.DataBind();
     }
     //if (GInf.ReportEmployeesCount > 0)
     //{

//     StrSql = " SELECT LTR.TourPlanId as Id , CONVERT(VARCHAR,LTR.ApplyDate,106) AS AppliedOn , " +
//" CONVERT(VARCHAR,LTR.FROMDATE,106) as   FromDate  ,CONVERT(VARCHAR,LTR.TODATE,106)  as ToDate , ltr.TourDetails as Reason, " +
//" DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE  " +
//" AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS Taken,  LTR.AuthorisedBy," +
//" CASE WHEN GRANTDATE IS NOT NULL THEN 'APPROVED' WHEN REJECTDATE IS NOT NULL THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS" +
//" FROM TK_TOURPLAN LTR  INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID   " +
//" WHERE (GRANTDATE IS NOT NULL or REJECTDATE IS NOT NULL) AND EM.EMPLOYEEID = " + EmployeeID;
//     string temp = StrSql;



       StrSql = "   SELECT LTR.TourPlanId as Id ,EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, CONVERT(VARCHAR,LTR.ApplyDate,106) AS AppliedOn ,  CONVERT(VARCHAR,LTR.FROMDATE,106) as   FromDate  ,  " +
"  CONVERT(VARCHAR,LTR.TODATE,106)  as ToDate , ltr.TourDetails as Reason,  DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN  LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE   AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0  " +
"  END) AS Taken,  LTR.AuthorisedBy, CASE WHEN GRANTDATE IS NOT NULL THEN 'APPROVED' WHEN REJECTDATE IS NOT NULL THEN  " +
" 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS FROM TK_TOURPLAN LTR  INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
" INNER JOIN TK_SECTION SEM  " +
 "   ON SEM.SECTIONID = EM.SECTIONID  INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID    " +
 "  WHERE (GRANTDATE IS NULL and REJECTDATE IS  NULL)  " +
 "  and  REPORTINGEMPLOYEEID1 =   " + GInf.EmployeeId.ToString();




     //StrSql = " SELECT LTR.LEAVETRANSID , LTR.LEAVEAPPLIEDDATE, EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
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


         gvRo.DataSource = GlobalFill.FillDataTable(StrSql);
         gvRo.DataBind();



         //StrSql = " SELECT LTR.LEAVETRANSID , LTR.LEAVEAPPLIEDDATE, EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME +  ' ' + EM.EMPLOYEELASTNAME AS EmployeeName, " +
    //" LT.LEAVETYPENAME ,   LTR.FROMDATE , LTR.TODATE, " +
    //" LTR.APPROVEDBY, LTR.AUTHORISEDDATE, " +
    //" DATEDIFF(DD, LTR.FROMDATE, LTR.TODATE)+1-(CASE WHEN LTR.HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN LTR.FROMDATE <> LTR.TODATE AND LTR.HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
    //" LTR.REMARKS, CASE WHEN LTR.ISAPPROVED = 1 THEN 'APPROVED' WHEN LTR.ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS " +
    //" FROM TK_LEAVETRANS LTR " +
    //" INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
    //" INNER JOIN TK_SECTION SEM ON SEM.SECTIONID = EM.SECTIONID " +
    //" INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID " +
    //"  INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = LTR.LEAVETYPEID " +
    //" WHERE ISAPPROVED = 0 AND ISREJECTED = 0 AND ISRO2APPROVED = 0 AND ISRO2REJECTED = 0 AND ISROAPPROVED = 1 " +
    //         " AND REPORTINGEMPLOYEEID2 =  " + GInf.EmployeeId.ToString();


    //     gvRo2.DataSource = GlobalFill.FillDataTable(StrSql);
    //     gvRo2.DataBind();

     //}
     //else
     //{
     //    gvRo.Visible = false;
     //    lblRoTitle.Visible = false;
     //}
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("~/ApplyTour.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=HOD");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void gvRo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("~/ApplyTour.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=RO");
    }
    protected void gvRo2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("~/ApplyTour.aspx?ID=" + e.CommandArgument.ToString() + "&Mode=" + e.CommandName + "&Level=RO2");
    }
    protected void gvHod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
