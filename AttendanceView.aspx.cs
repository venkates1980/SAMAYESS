using System;
using System.Drawing;
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
    Int64 EmployeeID;
    Boolean WHBASEDFILO;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1.SelectedValue = DateTime.Now.Month.ToString();
            // dtlstYear.SelectedValue = DateTime.Now.Year.ToString();
            GInf = new GlobalInformation(this);
            if (Convert.ToInt64(GInf.EmployeeId) == 0)
            {
                Response.Redirect("~/login.aspx");
            }
            EmployeeID = Convert.ToInt64(GInf.EmployeeId);
            WHBASEDFILO = Convert.ToBoolean(GlobalFill.GetQueryValue("SELECT WHBASEDFILO FROM TK_COMPANY WHERE COMPANYID = " + GInf.CompanyID.ToString()));
        }
        else
        {
            GInf = new GlobalInformation(this);
            if (Convert.ToInt64(GInf.EmployeeId) == 0)
            {
                Response.Redirect("~/login.aspx");
            }
            EmployeeID = Convert.ToInt64(GInf.EmployeeId);
            WHBASEDFILO = Convert.ToBoolean(GlobalFill.GetQueryValue("SELECT WHBASEDFILO FROM TK_COMPANY WHERE COMPANYID = " + GInf.CompanyID.ToString()));
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {

    }
    protected void Button12_Click1(object sender, EventArgs e)
    {


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if  (e.Row.RowType == DataControlRowType.Header )
        //{
        //    TableCell cell = e.Row.Cells[0];
        //    cell.Width = new Unit("50px");
        //    cell.Text = "Date";



        //}

        //    'For first column set to 200 px
        //    Dim cell As TableCell = e.Row.Cells(0)
        //    cell.Width = New Unit("200px")

        //    'For others set to 50 px
        //    'You can set all the width individually

        //    For i = 1 To e.Row.Cells.Count - 1
        //        'Mind that i used i=1 not 0 because the width of cells(0) has already been set
        //        Dim cell2 As TableCell = e.Row.Cells(i)
        //        cell2.Width = New Unit("10px")
        //    Next
        //End If
    }
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string quantity = Convert.ToString(e.Row.Cells[4].Text);

            foreach (TableCell cell in e.Row.Cells)
            {
                if (quantity == "Absent")
                {
                    cell.BackColor = Color.Orange;
                }
                if (quantity == "Present")
                {
                    cell.BackColor = Color.LightGreen;
                }
                if (quantity == "Weeklyoff")
                {
                    cell.BackColor = Color.White;
                }
                if (quantity == "Leave-EL")
                {
                    cell.BackColor = Color.Brown;
                }
                if (quantity == "Holiday")
                {
                    cell.BackColor = Color.Yellow;
                }
                if (quantity == "Tour")
                {
                    cell.BackColor = Color.Blue;
                }
                if (quantity == "OD")
                {
                    cell.BackColor = Color.Pink;
                }
            }
        }

    }
    protected void Button12_Click2(object sender, ImageClickEventArgs e)
    {
        string ODstr = " (SELECT ISNULL(SUM(WORKEDMINUTES),0) FROM TK_ONDUTY WHERE ATTENDANCEID = ATM.ATTENDANCEID AND INCLUDEINWORKINGHOURS = 1) ";
        string PerStr = " (SELECT ISNULL(SUM(DURATIONMINUTES),0) FROM TK_TIMEPERMISSION WHERE ATTENDANCEID = ATM.ATTENDANCEID  AND INCLUDEINWORKINGHOURS = 1 ) ";

        String StrAtt = "SELECT CONVERT(VARCHAR,ATTENDANCEDATE,106) as Date ,ASM.SHIFTNAME AS Alloted,ISNULL(PSM.SHIFTNAME,'') AS Presented,Status + '  -  ' + Status1 + Status2 as Status,  " +
            //" CASE WHEN HOLIDAY =1 THEN 'H' ELSE '' END AS HOLIDAY ," +
            //" CASE WHEN ISONLEAVE = 1 THEN 'L' ELSE '' END AS ISONLEAVE," +
            //" CASE WHEN ISONTOUR = 1 THEN 'T' ELSE '' END AS ISONTOUR ," +
            //" CASE WHEN WEEKLYOFF=1 THEN 'WO' ELSE ''  END AS WEEKLYOFF ," +
            //" CASE WHEN HALFDAY=1 THEN 'HD' ELSE ''  END AS HD ," +
            //" ATM.LEAVETYPEID, " +
      " CASE WHEN HOLIDAY = 1 AND UNPAIDHOLIDAY = 1   THEN 'UP-Holiday' " +
      " WHEN HOLIDAY = 1 AND UNPAIDHOLIDAY = 0   THEN 'Holiday' " +
      " WHEN ISONLEAVE = 1 THEN 'Leave-' + LT.LEAVETYPENAME " +
      " WHEN ISONTOUR = 1 THEN 'Tour' " +
      " WHEN WEEKLYOFF = 1 THEN 'Weeklyoff' " +
       " WHEN ISONOD = 1 THEN 'On Duty' " +
      " WHEN STATUS = 'P' THEN 'Present' " +
      " WHEN STATUS = 'PX' THEN 'Present'" +
      " WHEN STATUS = 'A' THEN 'Absent' " +
      " else '' " +
      " END AS Remarks, " +
      "  SUBSTRING(     CONVERT(VARCHAR,INTIME1,108),1,5)    AS FirstIn , " +
      " SUBSTRING(   CONVERT(VARCHAR,    ISNULL(ATM.OUTTIME8,ISNULL(ATM.INTIME8,ISNULL(ATM.OUTTIME7,ISNULL(ATM.INTIME7,ISNULL(ATM.OUTTIME6,ISNULL(ATM.INTIME6,ISNULL(ATM.OUTTIME5, " +
      "    ISNULL(ATM.INTIME5,ISNULL(ATM.OUTTIME4,ISNULL(ATM.INTIME4,ISNULL(ATM.OUTTIME3,ISNULL(ATM.INTIME3,ISNULL(ATM.OUTTIME2,ISNULL(ATM.INTIME2,ATM.OUTTIME1)))))))))))))),108),1,5) " +
      " AS LastOut, ";

        if (WHBASEDFILO)
        {
            StrAtt += "CONVERT(VARCHAR, FLOOR((ABS(ISNULL(DATEDIFF(MI,ISNULL(ATM.OUTTIME8,ISNULL(ATM.INTIME8,ISNULL(ATM.OUTTIME7,ISNULL(ATM.INTIME7,ISNULL(ATM.OUTTIME6,ISNULL(ATM.INTIME6,ISNULL(ATM.OUTTIME5, " +
                " ISNULL(ATM.INTIME5,ISNULL(ATM.OUTTIME4,ISNULL(ATM.INTIME4,ISNULL(ATM.OUTTIME3,ISNULL(ATM.INTIME3,ISNULL(ATM.OUTTIME2,ISNULL(ATM.INTIME2,ISNULL(ATM.OUTTIME2,ISNULL(INTIME2,OUTTIME1)))))))))))))))), " +
                " ATM.INTIME1),0)) + " + ODstr + " + " + PerStr + ")/60.0)) + ':' +RIGHT ('0'+ " +
                " CONVERT(VARCHAR, FLOOR((ABS(ISNULL(DATEDIFF(MI,ISNULL(ATM.OUTTIME8,ISNULL(ATM.INTIME8,ISNULL(ATM.OUTTIME7,ISNULL(ATM.INTIME7,ISNULL(ATM.OUTTIME6,ISNULL(ATM.INTIME6,ISNULL(ATM.OUTTIME5, " +
                " ISNULL(ATM.INTIME5,ISNULL(ATM.OUTTIME4,ISNULL(ATM.INTIME4,ISNULL(ATM.OUTTIME3,ISNULL(ATM.INTIME3,ISNULL(ATM.OUTTIME2,ISNULL(ATM.INTIME2,ISNULL(ATM.OUTTIME2,ISNULL(INTIME2,OUTTIME1)))))))))))))))), " +
                " ATM.INTIME1),0)) +" + ODstr + "+" + PerStr + ") %60) ),2)  AS WHRS, " +
                 " ISNULL(CONVERT(VARCHAR,DATEPART(HH,OVERTIME)),'00') + ':' + ISNULL(CONVERT(VARCHAR,DATEPART(MI,OVERTIME)),'00') AS AOT," +
                " CASE WHEN ((ISNULL(DATEDIFF(MI,ATM.INTIME1,ISNULL(ATM.OUTTIME8,ISNULL(ATM.INTIME8,ISNULL(ATM.OUTTIME7,ISNULL(ATM.INTIME7,ISNULL(ATM.OUTTIME6,ISNULL(ATM.INTIME6,ISNULL(ATM.OUTTIME5, " +
                " ISNULL(ATM.INTIME5,ISNULL(ATM.OUTTIME4,ISNULL(ATM.INTIME4,ISNULL(ATM.OUTTIME3,ISNULL(ATM.INTIME3,ISNULL(ATM.OUTTIME2,ISNULL(ATM.INTIME2,ISNULL(ATM.OUTTIME2,ISNULL(INTIME2,OUTTIME1)))))))))))))))) " +
                " ),0)) ) -(FLOOR( PSM.SHIFTDURATION)*60   +  ( PSM.SHIFTDURATION - FLOOR( PSM.SHIFTDURATION))*100) >0  " +
                " THEN " +
                " CONVERT(VARCHAR, FLOOR(ABS(CONVERT(BIGINT,((ISNULL(DATEDIFF(MI,ATM.INTIME1,ISNULL(ATM.OUTTIME8,ISNULL(ATM.INTIME8,ISNULL(ATM.OUTTIME7,ISNULL(ATM.INTIME7,ISNULL(ATM.OUTTIME6,ISNULL(ATM.INTIME6,ISNULL(ATM.OUTTIME5, " +
                " ISNULL(ATM.INTIME5,ISNULL(ATM.OUTTIME4,ISNULL(ATM.INTIME4,ISNULL(ATM.OUTTIME3,ISNULL(ATM.INTIME3,ISNULL(ATM.OUTTIME2,ISNULL(ATM.INTIME2,ISNULL(ATM.OUTTIME2,ISNULL(INTIME2,OUTTIME1)))))))))))))))) " +
                " ),0)) ) -(FLOOR( PSM.SHIFTDURATION)*60   +  ( PSM.SHIFTDURATION- FLOOR( PSM.SHIFTDURATION))*100) ))/60.0)) + ':' + RIGHT ('0'+ CONVERT(VARCHAR,ABS(CONVERT(BIGINT,((ISNULL(DATEDIFF(MI,ATM.INTIME1,ISNULL(ATM.OUTTIME8,ISNULL(ATM.INTIME8,ISNULL(ATM.OUTTIME7,ISNULL(ATM.INTIME7,ISNULL(ATM.OUTTIME6,ISNULL(ATM.INTIME6,ISNULL(ATM.OUTTIME5, " +
                " ISNULL(ATM.INTIME5,ISNULL(ATM.OUTTIME4,ISNULL(ATM.INTIME4,ISNULL(ATM.OUTTIME3,ISNULL(ATM.INTIME3,ISNULL(ATM.OUTTIME2,ISNULL(ATM.INTIME2,ISNULL(ATM.OUTTIME2,ISNULL(INTIME2,OUTTIME1)))))))))))))))) " +
                " ),0)) ) -(FLOOR( PSM.SHIFTDURATION)*60   +  ( PSM.SHIFTDURATION - FLOOR( PSM.SHIFTDURATION))*100))) % 60),2) " +
                " ELSE " +
                " '00.00' END AS OT ";
        }
        else
        {
            StrAtt += "CONVERT(VARCHAR, FLOOR((ABS(ISNULL(DATEDIFF (MI,INTIME1,OUTTIME1),0) +  ISNULL(DATEDIFF (MI,INTIME2,OUTTIME2),0) +  ISNULL(DATEDIFF (MI,INTIME3,OUTTIME3),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME4,OUTTIME4),0) +  ISNULL(DATEDIFF (MI,INTIME5,OUTTIME5),0) +  ISNULL(DATEDIFF (MI,INTIME6,OUTTIME6),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME7,OUTTIME7),0) +  ISNULL(DATEDIFF (MI,INTIME8,OUTTIME8),0) ) + " + ODstr + "+" + PerStr + ")/60.0)) + ':' + RIGHT ('0'+ CONVERT(VARCHAR,ABS((ISNULL(DATEDIFF (MI,INTIME1,OUTTIME1),0) +  ISNULL(DATEDIFF (MI,INTIME2,OUTTIME2),0) +  ISNULL(DATEDIFF (MI,INTIME3,OUTTIME3),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME4,OUTTIME4),0) +  ISNULL(DATEDIFF (MI,INTIME5,OUTTIME5),0) +  ISNULL(DATEDIFF (MI,INTIME6,OUTTIME6),0) +  " +
                  " ISNULL(DATEDIFF (MI,INTIME7,OUTTIME7),0) +  ISNULL(DATEDIFF (MI,INTIME8,OUTTIME8),0) ) + " + ODstr + "+" + PerStr + ") % 60),2) AS WHRS, " +
                  " ISNULL(CONVERT(VARCHAR,DATEPART(HH,OVERTIME)),'00') + ':' + ISNULL(CONVERT(VARCHAR,DATEPART(MI,OVERTIME)),'00') AS AOT," +
                  " CASE WHEN ((ISNULL(DATEDIFF (MI,INTIME1,OUTTIME1),0) +  ISNULL(DATEDIFF (MI,INTIME2,OUTTIME2),0) +  ISNULL(DATEDIFF (MI,INTIME3,OUTTIME3),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME4,OUTTIME4),0) +  ISNULL(DATEDIFF (MI,INTIME5,OUTTIME5),0) +  ISNULL(DATEDIFF (MI,INTIME6,OUTTIME6),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME7,OUTTIME7),0) +  ISNULL(DATEDIFF (MI,INTIME8,OUTTIME8),0))   - ( PSM.SHIFTDURATION * 60) +     (PSM.SHIFTDURATION - FLOOR(PSM.SHIFTDURATION))  ) > 0  " +
                  " THEN " +
                  " RIGHT('0' +  CONVERT(VARCHAR, FLOOR(ABS((ISNULL(DATEDIFF (MI,INTIME1,OUTTIME1),0) +  ISNULL(DATEDIFF (MI,INTIME2,OUTTIME2),0) +  ISNULL(DATEDIFF (MI,INTIME3,OUTTIME3),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME4,OUTTIME4),0) +  ISNULL(DATEDIFF (MI,INTIME5,OUTTIME5),0) +  ISNULL(DATEDIFF (MI,INTIME6,OUTTIME6),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME7,OUTTIME7),0) +  ISNULL(DATEDIFF (MI,INTIME8,OUTTIME8),0))   - ( PSM.SHIFTDURATION * 60) +     (PSM.SHIFTDURATION - FLOOR(PSM.SHIFTDURATION)))/60.0)),2) " +
                  "  + '.' + " +
                  "  RIGHT ('0'+ CONVERT(VARCHAR,CONVERT(BIGINT,   ABS((ISNULL(DATEDIFF (MI,INTIME1,OUTTIME1),0) +  ISNULL(DATEDIFF (MI,INTIME2,OUTTIME2),0) +  ISNULL(DATEDIFF (MI,INTIME3,OUTTIME3),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME4,OUTTIME4),0) +  ISNULL(DATEDIFF (MI,INTIME5,OUTTIME5),0) +  ISNULL(DATEDIFF (MI,INTIME6,OUTTIME6),0) +   " +
                  " ISNULL(DATEDIFF (MI,INTIME7,OUTTIME7),0) +  ISNULL(DATEDIFF (MI,INTIME8,OUTTIME8),0))   - ( PSM.SHIFTDURATION * 60) +     (PSM.SHIFTDURATION - FLOOR(PSM.SHIFTDURATION)))) % 60),2) " +
                  " ELSE '00:00' END  AS OT     ";
        }


        StrAtt += " FROM TK_ATTENDANCE ATM " +
            " INNER JOIN TK_SHIFT ASM ON ASM.SHIFTID = ATM.ALLOTEDSHIFTID " +
            " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = ATM.EMPLOYEEID " +
            " INNER JOIN TK_SECTION SEM ON SEM.SECTIONID = EM.SECTIONID " +
            " INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEM.DEPARTMENTID " +
            " LEFT JOIN TK_SHIFT PSM ON PSM.SHIFTID = ATM.PRESENTSHIFTID" +
            " LEFT JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = ATM.LEAVETYPEID ";


        string StrSql = StrAtt + " WHERE ATM.INACTIVE = 0 AND EM.EMPLOYEEID = " + EmployeeID + " AND MONTH(ATTENDANCEDATE) = " + DropDownList1.SelectedValue + " AND YEAR(ATTENDANCEDATE) = " + dtlstYear.SelectedItem +
            " ORDER BY ATTENDANCEDATE ";



        GridView1.DataSource = GlobalFill.FillDataTable(StrSql);
        GridView1.DataBind();




    }
}