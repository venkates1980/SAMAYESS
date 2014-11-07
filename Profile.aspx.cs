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
    string StrSql = "";
    GlobalInformation Ginf;
    protected void Page_Load(object sender, EventArgs e)
    {

        Ginf  = new GlobalInformation(this);
        //if (!IsPostBack)
        //{
        //    Emp = new Employee(UInfo.EmpId);
        //    RefreshData();
        //    LoadFields();
       //}

        //string StrSql = "";
        //StrSql = "SELECT PHOTO FROM TK_EMPLOYEE  WHERE EMPLOYEEID = " + Convert.ToString(Request.QueryString["Id"]);


        //Response.ContentType = "image/jpg";
        //if (GlobalFill.GetQueryValue(StrSql) != System.DBNull.Value)
        //{
        //    Response.BinaryWrite((byte[])GlobalFill.GetQueryValue(StrSql));
        //}
        //else
        //{
        //    string strurl = "~/Images/SmallLogo.jpg";
        //    Image img = new Image(strurl);
        //    img.ImageUrl = "~/" + img;

        //}

        SqlConnection SqlConn = GlobalFunctions.GetConnection();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
        SqlTransaction SqlTrans = SqlConn.BeginTransaction();
        SqlCommand SqlCmd = new SqlCommand();
        SqlCmd.Connection = SqlTrans.Connection; // GlobalFunctions.GetConnection();
        SqlCmd.Transaction = SqlTrans;

        Int64 EmployeeID = Ginf.EmployeeId ;
        Page.Title = "SamayBio Employee Self Service";

      
        
        SqlCmd.CommandText = "select ReportingEmployeeID1 from tk_employee where EMPLOYEEID='" + EmployeeID + "'";
       
        string str = Convert.ToString(SqlCmd.ExecuteScalar());

        SqlCmd.CommandText = "select ReportingEmployeeID2 from tk_employee where EMPLOYEEID='" + EmployeeID + "'";
        string strreporting2 = Convert.ToString(SqlCmd.ExecuteScalar());

        StrSql = " SELECT EMPLOYEEID , EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME ,EMPLOYEECODE,DESIGNATIONNAME, EMPLOYEERFID, CARDNUMBER, FATHERNAME, MOTHERNAME, " +
            " RESIDENTIALADDRESS, PERMANENTADDRESS, CONTACTNO, MOBILENO, EMAILID, POB, BLOODGROUP, " +
            "  DEPARTMENTNAME, SECTIONNAME, GRADENAME, CATEGORYNAME, LOCATIONNAME, EMPLOYEETYPENAME, RECRUITMENTMODENAME , " +
            " DOB, DOJ, GENDER, EM.INACTIVE ,DOR ,photo," +
            " (select EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME from TK_EMPLOYEE EMP1 where EMP1.EmployeeID='"+str+"') as REPORTINGID1, " +
            " (select EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME from TK_EMPLOYEE EMP2 where EMP2.EmployeeID='"+strreporting2+"') as REPORTINGID2 " +
            "  FROM TK_EMPLOYEE EM " +
            " INNER JOIN TK_SECTION SEC ON SEC.SECTIONID = EM.SECTIONID " +
            " INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEC.DEPARTMENTID " +
            " INNER JOIN TK_CATEGORY CM ON CM.CATEGORYID = EM.CATEGORYID " +
            " INNER JOIN TK_GRADE GM ON GM.GRADEID = EM.GRADEID " +
            " INNER JOIN TK_LOCATION LM ON LM.LOCATIONID = EM.LOCATIONID " +
            " INNER JOIN TK_EMPLOYEETYPE ETP ON ETP.EMPLOYEETYPEID = EM.EMPLOYEETYPEID " +
            " INNER JOIN TK_RECRUITMENTMODE RM ON RM.RECRUITMENTMODEID = EM.RECRUITMENTMODEID  " +
            " INNER JOIN TK_Designation DG ON EM.DesignationID=DG.DesignationID " +
            " WHERE EM.EMPLOYEEID = " + EmployeeID.ToString();
        SqlDataSource1.SelectCommand = StrSql;

      
    }
}
