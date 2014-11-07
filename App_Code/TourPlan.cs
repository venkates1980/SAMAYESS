using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

  public class TourPlan : MasterBase
  {
    public Int64 TourPlanID;
    public Int64 EmployeeID;
    public DateTime FromDate = System.DateTime.Now ;
    public DateTime ToDate = System.DateTime.Now ;
    public String Status ="A"; // A - Applied, G - Granted , R - Rejected.
    public DateTime  ApplyDate = System.DateTime.Now  ;
    public DateTime GrantDate = GlobalFunctions.NullRefDate;
    public DateTime RejectedDate = GlobalFunctions.NullRefDate;
    public String AuthorisedBy = "";
    public String TourDetails = "";
    public String RejectReason = "";
    public String Remarks = "";
    public Boolean HalfDayOnFrom = false;
    public Boolean HalfDayOnTo = false;

    
    public TourPlan()
    {
      LoadHeaders();
    }
    ~TourPlan ()
    {
  
      GC.SuppressFinalize(this);

    }
    public TourPlan(Int64 ID)
    {
      TourPlanID  = ID;
      LoadAttributes(ID);
      LoadHeaders();
    }

    public Boolean LeaveApplicationExists()
    {
        SqlConn = GlobalFunctions.GetConnection();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
        for (DateTime d = Convert.ToDateTime(FromDate.ToString("dd/MMM/yy")); d <= Convert.ToDateTime(ToDate.ToString("dd/MMM/yy")); d = d.AddDays(1))
        {
            StrSql = "SELECT * FROM TK_TourPlan WHERE EMPLOYEEID = " + EmployeeID.ToString() + " AND '" + "' BETWEEN FROMDATE AND TODATE AND TourPlanID <> " + TourPlanID.ToString();
            SqlCmd = new SqlCommand(StrSql, SqlConn);
            SqlDataReader SDR = SqlCmd.ExecuteReader();
            if (SDR.Read())
            {
                SDR.Close();
                return true;
            }
            SDR.Close();
        }
        return false;
    }

    public void LoadHeaders()
    {
      EntryHeader = "Tour Plan";
      EntryDescription = "";
      //SearchColumnName = "EmployeeCode";
    }
    //////public Boolean LeaveApplicationExists()
    //////{
    //////    SqlConn = GlobalFunctions.GetConnection();
    //////    if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
    //////    for (DateTime d = Convert.ToDateTime(FromDate.ToString("dd/MMM/yy")); d <= Convert.ToDateTime(ToDate.ToString("dd/MMM/yy")); d = d.AddDays(1))
    //////    {
    //////        StrSql = "SELECT * FROM TK_TOURPLAN WHERE TOURPLANID = " + +"";
    //////        SqlCmd = new SqlCommand(StrSql, SqlConn);
    //////        SqlDataReader SDR = SqlCmd.ExecuteReader();
    //////        if (SDR.Read())
    //////        {
    //////            SDR.Close();
    //////            return true;
    //////        }
    //////        SDR.Close();
    //////    }
    //////    return false;
    //////}

    public override void LoadAttributes(long ID)
    {
      try
      {
            SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            StrSql = "SELECT * FROM TK_TOURPLAN WHERE TOURPLANID = " + ID;
            SqlDataReader DR;
            SqlCmd = new SqlCommand(StrSql, SqlConn);
            DR = SqlCmd.ExecuteReader();
            if (DR.Read())
            {

        

          TourPlanID  = Convert.ToInt64(DR["TOURPLANID"]);
          EmployeeID  = Convert.ToInt64(DR["EMPLOYEEID"]);
          FromDate = Convert.ToDateTime(DR["FROMDATE"]);
          ToDate = Convert.ToDateTime(DR["TODATE"]);
          Status = Convert.ToString(DR["STATUS"]);
          ApplyDate = Convert.ToDateTime(DR["APPLYDATE"]);
          if (DR["GRANTDATE"] != DBNull.Value )
          {
            GrantDate = Convert.ToDateTime(DR["GRANTDATE"]);
          }
          if (DR["REJECTDATE"] != DBNull.Value)
          {
            RejectedDate = Convert.ToDateTime(DR["REJECTDATE"]);
          }
          AuthorisedBy = Convert.ToString(DR["AUTHORISEDBY"]);
          RejectReason = Convert.ToString(DR["REJECTREASON"]);
          TourDetails = Convert.ToString(DR["TOURDETAILS"]);
          Remarks = Convert.ToString(DR["REMARKS"]);
          HalfDayOnFrom = Convert.ToBoolean(DR["HALFDAYONFROM"]);
          HalfDayOnTo = Convert.ToBoolean(DR["HALFDAYONTO"]);
         
        }
        DR.Close();
      }
      catch (Exception E)
      {
        ////SamayMsgBox.Show(E.Message);
      }
    }
    public string GetProcStr(String TransType)
    {
      StrSql = "";
      switch (TransType.ToUpper())
      {
        case "INSERT":
          StrSql = " SELECT @TOURPLANID = ISNULL(MAX(TOURPLANID),0)+1 FROM TK_TOURPLAN" +
           " INSERT INTO TK_TOURPLAN(TOURPLANID,EMPLOYEEID,FROMDATE,TODATE,STATUS,APPLYDATE,TOURDETAILS,GRANTDATE,REJECTDATE,REJECTREASON,AUTHORISEDBY,REMARKS,COMPANYID,HALFDAYONFROM,HALFDAYONTO) " +
           " VALUES (@TOURPLANID,@EMPLOYEEID,@FROMDATE,@TODATE,@STATUS,@APPLYDATE,@TOURDETAILS,@GRANTDATE,@REJECTDATE,@REJECTREASON,@AUTHORISEDBY,@REMARKS,@COMPANYID,@HALFDAYONFROM,@HALFDAYONTO)" +
           " SELECT @OUTPUTPARAM = @TOURPLANID ";
          break;
        case "UPDATE":
          StrSql = " UPDATE TK_TOURPLAN  SET TOURPLANID  = @TOURPLANID , " +
                                  " EMPLOYEEID = @EMPLOYEEID," +
                                  " FROMDATE = @FROMDATE," +
                                  " TODATE = @TODATE," +
                                  " STATUS = @STATUS," +
                                  " TOURDETAILS = @TOURDETAILS," +
                                  " GRANTDATE = @GRANTDATE," +
                                  " REJECTDATE = @REJECTDATE," +
                                  " REJECTREASON= @REJECTREASON," +
                                  " AUTHORISEDBY = @AUTHORISEDBY, " +
                                  " HALFDAYONFROM = @HALFDAYONFROM, " +
                                  " HALFDAYONTO = @HALFDAYONTO, "+
                                  " COMPANYID = @COMPANYID " +
                        "	WHERE TOURPLANID = @TOURPLANID " +
                         " SELECT @OUTPUTPARAM = @TOURPLANID ";
          break;
        case "DELETE":
          StrSql = " DELETE FROM TK_TOURPLAN" +
               " WHERE TOURPLANID = @TOURPLANID " +
               "  SELECT @OUTPUTPARAM = @TOURPLANID ";
          break;
      }

      return StrSql;
    }

    public override long GetExecuteCommand(string TransType, SqlTransaction SqlTrans)
    {
      try
      {

          if (TransType.ToUpper() == "DELETE") {
              SqlCommand SqlCmd1 = new SqlCommand();
              SqlCmd1.Connection = GlobalFunctions.GetConnection();
              //SqlCmd1.Connection = GlobalVariables.SqlConn;
              SqlCmd1.Transaction = SqlTrans;
              string SQLstr = "UPDATE TK_ATTENDANCE SET IsOnTour = 0 " +
             "  WHERE EMPLOYEEID = " + EmployeeID + "  AND ATTENDANCEDATE >= '" + FromDate.ToString("ddMMMyyyy") + "'" +
             " AND ATTENDANCEDATE <= '" + ToDate.ToString("ddMMMyyyy") + "'";
              SqlCmd1.CommandText = SQLstr;
              SqlCmd1.ExecuteNonQuery();
          
          }
        SqlCmd = new SqlCommand();
        SqlCmd.Connection = SqlTrans.Connection;
        //SqlCmd.Connection = GlobalVariables.SqlConn;
        SqlCmd.Transaction = SqlTrans;
        SqlCmd.CommandText = GetProcStr(TransType.ToUpper());
        SqlCmd.CommandType = CommandType.Text;
        SqlCmd.Parameters.AddWithValue("@TOURPLANID", TourPlanID );
        SqlCmd.Parameters.AddWithValue("@EMPLOYEEID",EmployeeID );
        SqlCmd.Parameters.AddWithValue("@FROMDATE",FromDate);
        SqlCmd.Parameters.AddWithValue("@TODATE", ToDate);
        SqlCmd.Parameters.AddWithValue("@STATUS", Status);
        SqlCmd.Parameters.AddWithValue("@APPLYDATE",ApplyDate);
        SqlCmd.Parameters.AddWithValue("@TOURDETAILS", TourDetails);
        if (GrantDate != GlobalFunctions.NullRefDate)
        {
          SqlCmd.Parameters.AddWithValue("@GRANTDATE", GrantDate);
        }
        else
        {
          SqlCmd.Parameters.AddWithValue("@GRANTDATE", DBNull.Value );
        }
        if (RejectedDate != GlobalFunctions.NullRefDate)
        {
          SqlCmd.Parameters.AddWithValue("@REJECTDATE", RejectedDate);
        }
        else
        {
          SqlCmd.Parameters.AddWithValue("@REJECTDATE", DBNull.Value );
        }
        SqlCmd.Parameters.AddWithValue("@REJECTREASON",RejectReason);
        SqlCmd.Parameters.AddWithValue("@AUTHORISEDBY", AuthorisedBy);
        SqlCmd.Parameters.AddWithValue("@REMARKS",Remarks);
        SqlCmd.Parameters.AddWithValue("@HALFDAYONFROM", HalfDayOnFrom );
        SqlCmd.Parameters.AddWithValue("@HALFDAYONTO", HalfDayOnTo );
        SqlCmd.Parameters.AddWithValue("@COMPANYID", CompanyID);
        SqlParameter OutPutParameter = SqlCmd.Parameters.AddWithValue("OUTPUTPARAM", 0);
        OutPutParameter.Direction = ParameterDirection.InputOutput;
        if (SqlCmd.ExecuteNonQuery() > 0)
        {
       
           

          return Convert.ToInt64(OutPutParameter.Value);
        }
        return 0;
      }
      catch (Exception E)
      {
        //SamayMsgBox.Show(E.Message);
        return 0;
      }
    }

    public override string GetViewQuery(string condStr)
    {

        if ( condStr.Equals("100") || condStr.Equals("500") || condStr.Equals("1000"))
        {
            StrSql = "SELECT top(" + condStr + ")  ";
            condStr = "";
        }
        else
        {
            StrSql = "SELECT  ";
        }
        StrSql += " TP.TOURPLANID,TP.APPLYDATE, EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME + ' ' + EM.EMPLOYEELASTNAME AS EMPLOYEENAME, " +
          " TP.FROMDATE,TP.TODATE, TP.TOURDETAILS, " + 
          " CASE WHEN GRANTDATE IS NOT NULL THEN 'GRANTED' WHEN REJECTDATE IS NOT NULL THEN 'REJECTED' ELSE 'PENDING' END AS STATUS, " +
          " TP.GRANTDATE,TP.REJECTDATE, TP.REJECTREASON, TP.REMARKS, TP.COMPANYID, TP.EMPLOYEEID, TP.AUTHORISEDBY " +
          " FROM TK_TOURPLAN TP INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = TP.EMPLOYEEID "+
            " WHERE EM.COMPANYID = " + CompanyID;
        if ((condStr.Trim().Length > 3) && (condStr.Trim() != ""))
        {
            StrSql = StrSql + " AND " + condStr;
        }
      StrSql += " ORDER BY TP.APPLYDATE  DESC ";
      return StrSql;
    }

    //public override void SetColWidth(System.Windows.Forms.DataGridView DgVw)
    //{
    //  DgVw.Columns["TOURPLANID"].Visible = false;
    //  DgVw.Columns["EMPLOYEECODE"].Width = 100;
    //  DgVw.Columns["EMPLOYEENAME"].Width = 200;
    //  DgVw.Columns["FROMDATE"].Width = 100;
    //  DgVw.Columns["FROMDATE"].DefaultCellStyle.Format = "dd MMM yyyy";
    //  DgVw.Columns["TODATE"].Width = 100;
    //  DgVw.Columns["TODATE"].DefaultCellStyle.Format = "dd MMM yyyy";
    //  DgVw.Columns["TOURDETAILS"].Visible = true ;
    //  DgVw.Columns["TOURDETAILS"].Width = 100;
    //  DgVw.Columns["STATUS"].Width = 100;
    //  DgVw.Columns["APPLYDATE"].Width = 100;
    //  DgVw.Columns["APPLYDATE"].DefaultCellStyle.Format = "dd MMM yyyy";
    //  DgVw.Columns["GRANTDATE"].Width = 100;
    //  DgVw.Columns["GRANTDATE"].DefaultCellStyle.Format = "dd MMM yyyy";
    //  DgVw.Columns["REJECTDATE"].Width = 100;
    //  DgVw.Columns["REJECTDATE"].DefaultCellStyle.Format = "dd MMM yyyy";
    //  DgVw.Columns["REJECTREASON"].Visible = false;
    //  DgVw.Columns["REMARKS"].Width = 150;
    //  DgVw.Columns["COMPANYID"].Visible = false;
    //  DgVw.Columns["EMPLOYEEID"].Visible = false;
    //  DgVw.Columns["AUTHORISEDBY"].Width = 100;
    //}
    //public override void SetColWidthStr()
    //{
    //    DisplayColumnsList = "ApplyDate,EmployeeCode,EmployeeName,FromDate,ToDate,TourDetails,Status,GrantDate,RejectDate";
    //    DisplayColWidthPerStr = "10,10,20,10,10,10,10,10,10";
    //}
    //public override void SetColHeaders(System.Windows.Forms.DataGridView DgVw)
    //{
    //    DgVw.Columns["EmployeeCode"].HeaderText = "Code";
    //    DgVw.Columns["EmployeeName"].HeaderText = "Employee Name";
    //    DgVw.Columns["FromDate"].HeaderText = "From";
    //    DgVw.Columns["ToDate"].HeaderText = "To";
    //    DgVw.Columns["TourDetails"].HeaderText = "Details";
    //    DgVw.Columns["fromdate"].DefaultCellStyle.Format = "dd MMM yyyy";
    //    DgVw.Columns["todate"].DefaultCellStyle.Format = "dd MMM yyyy";

    //    DgVw.Columns["APPLYDATE"].DefaultCellStyle.Format = "dd MMM yyyy";

    //    DgVw.Columns["GRANTDATE"].DefaultCellStyle.Format = "dd MMM yyyy";

    //    DgVw.Columns["REJECTDATE"].DefaultCellStyle.Format = "dd MMM yyyy";


    //    DgVw.Columns["Status"].HeaderText = "Status";
    //    DgVw.Columns["ApplyDate"].HeaderText = "Applied On";
    //    DgVw.Columns["GrantDate"].HeaderText = "Granted On";
    //    DgVw.Columns["RejectDate"].HeaderText = "Rejected On";
    //}
  }





