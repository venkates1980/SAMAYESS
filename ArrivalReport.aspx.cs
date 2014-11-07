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
    private void RefreshData()
    {


        String StrSql = " SELECT SECTIONID, DEPARTMENTNAME + ' - '+ SECTIONNAME AS SECTIONNAME " +
           " FROM TK_SECTION SM  " +
           " INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SM.DEPARTMENTID  " +
           " WHERE DM.DEPARTMENTID IN (" + GInf.HodDeptIDStr + ") AND DM.COMPANYID =  " + GInf.CompanyID.ToString();




        ddlDepartment.DataSource = GlobalFill.FillDataTable(StrSql);
        ddlDepartment.DataValueField = "SECTIONID";
        ddlDepartment.DataTextField = "SECTIONNAME";
        ddlDepartment.DataBind();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GInf = new GlobalInformation(this);
        if (!Page.IsPostBack)
        {
            RefreshData();
        }
    }

    
    protected void Button7_Click(object sender, EventArgs e)
    {
        //string departmentid = ddlDepartment.SelectedValue.ToString ();
        //DataTable dt = GetArrivalReportData(Convert.ToDateTime(txtAttendanceDate.Value ),departmentid  , "", "");
        //GridView1.DataSource = dt ;
        //GridView1.DataBind();
        gridarrival();
    }
    public void gridarrival()
    {
        string departmentid = ddlDepartment.SelectedValue.ToString();
        DataTable dt = GetArrivalReportData(Convert.ToDateTime(txtAttendanceDate.Value), departmentid, "", "");
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    public DataTable GetArrivalReportData(DateTime mattendancedate, string mDepartmentIDStr, string mShiftIDStr, string mLocationIDStr)
    {

        Reporter ObjReporter = new Reporter();// SamayBioReporter.Reporter(new object(), new object(), new object(), new object(), new object(), new object(), new object(), new object(), new object(), new object());
        string StrSql = "SELECT EmployeeCode as Code,EMPLOYEENAME as Name,DEPARTMENTNAME as Department,SECTIONNAME as Section,FirstIn,LastOut,Status, WORKEDHOURS as WHrs FROM ( " +
        ObjReporter.GetStr(false, Convert.ToInt64(GInf.CompanyID   ))      + " WHERE  intime1 is not null ";
        StrSql += " AND ATTENDANCEDATE BETWEEN  '" + mattendancedate.ToString("ddMMMyyyy") + "' AND '" + mattendancedate.ToString("ddMMMyyyy") + "'";
        
        if (mDepartmentIDStr.Trim() != "")
        {
            StrSql += " AND SECTIONID IN (" + mDepartmentIDStr + ") ";
        }
        if (mShiftIDStr.Trim() != "")
        {
            StrSql += " AND PRESENTSHIFTID IN (" + mShiftIDStr + ") ";
        }


        if (mLocationIDStr.Trim() != "")
        {
            StrSql += " AND LOCATIONID IN (" + mLocationIDStr + ") ";
        }
        StrSql += "  ) A ";

        //if (ObjReporter.StrGrade != "")
        //{
        //    StrSql += " AND GRADEID IN (" + ObjReporter.StrGrade + ") ";
        //}
        DataTable dt = GlobalFill.FillDataTable(StrSql);
        return dt;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridarrival();
    }
}
