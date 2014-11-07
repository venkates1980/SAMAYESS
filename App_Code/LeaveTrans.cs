using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public class LeaveTrans : MasterBase
{
    public Int64 LeaveTransID;
    public Int64 LeaveTypeID;
    public DateTime LeaveAppliedDate = System.DateTime.Now;
    public Int64 EmployeeID;
    public DateTime FromDate = System.DateTime.Now;
    public DateTime ToDate = System.DateTime.Now;
    public Boolean IsApproved = false ;
    public Boolean IsRejected= false ;
    public DateTime AuthorisedDate = System.DateTime.Now;
    public string ApprovedBy = "";
    public string Remarks = "";
    public string Reason = "";
    public Boolean HalfDayOnFrom = false;
    public Boolean HalfDayOnTo = false;
    public Boolean IsROApproved = false;
    public Boolean IsRORejected = false;
    public string RoApprovedBy = "";

    public Boolean IsRO2Approved = false;
    public Boolean IsRO2Rejected = false;
    public string Ro2ApprovedBy = "";


    public LeaveTrans()
    {
        LoadHeaders();

    }

    public LeaveTrans(Int64 ID)
    {
        LeaveTransID = ID;
        LoadAttributes(ID);
        LoadHeaders();
    }

    public void LoadHeaders()
    {
        EntryHeader = "LeaveTrans";
        EntryDescription = "";
        DisplayColumnsList = "EmployeeCode;EmployeeName;LeaveTypeName;FromDate;ToDate";
    }

    public override void LoadAttributes(long ID)
    {
        try
        {
            SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            StrSql = "SELECT * FROM TK_LEAVETRANS WHERE LEAVETRANSID = " + ID;
            SqlDataReader DR;
            SqlCmd = new SqlCommand(StrSql, SqlConn);
            DR = SqlCmd.ExecuteReader();
            if (DR.Read())
            {
                LeaveTransID = Convert.ToInt64(DR["LEAVETRANSID"]);

                LeaveTypeID = Convert.ToInt64(DR["LEAVETYPEID"]);
                LeaveAppliedDate = Convert.ToDateTime(DR["LEAVEAPPLIEDDATE"]);
                EmployeeID = Convert.ToInt64(DR["EMPLOYEEID"]);
                FromDate = Convert.ToDateTime(DR["FROMDATE"]);
                ToDate = Convert.ToDateTime(DR["TODATE"]);
                IsApproved = Convert.ToBoolean(DR["ISAPPROVED"]);
                IsRejected = Convert.ToBoolean(DR["ISREJECTED"]);
                if (DR["AUTHORISEDDATE"] != System.DBNull.Value)
                    AuthorisedDate = Convert.ToDateTime(DR["AUTHORISEDDATE"]);
                ApprovedBy = Convert.ToString(DR["APPROVEDBY"]);
                Remarks = Convert.ToString(DR["REMARKS"]);
                Reason = Convert.ToString(DR["REASONFORLEAVE"]);
                HalfDayOnFrom = Convert.ToBoolean(DR["HALFDAYONFROM"]);
                HalfDayOnTo = Convert.ToBoolean(DR["HALFDAYONTO"]);
                CompanyID = Convert.ToInt64(DR["COMPANYID"]);
                IsROApproved = Convert.ToBoolean(DR["ISROAPPROVED"]);
                IsRORejected = Convert.ToBoolean(DR["ISROREJECTED"]);
                RoApprovedBy = Convert.ToString(DR["RoApprovedby"]);
                IsRO2Approved = Convert.ToBoolean(DR["ISRO2APPROVED"]);
                IsRO2Rejected = Convert.ToBoolean(DR["ISRO2REJECTED"]);
                Ro2ApprovedBy = Convert.ToString(DR["Ro2Approvedby"]);
            }
            DR.Close();
        }
        catch (Exception E)
        {
            //SamayMsgBox.Show(E.Message);
        }
    }
    public string GetProcStr(String TransType)
    {
        StrSql = "";
        switch (TransType.ToUpper())
        {
            case "INSERT":
                StrSql = " SELECT @LEAVETRANSID= ISNULL(MAX(LEAVETRANSID),0)+1 FROM TK_LEAVETRANS " +
                 " INSERT INTO TK_LEAVETRANS(LEAVETRANSID,LEAVEAPPLIEDDATE,LEAVETYPEID,EMPLOYEEID,FROMDATE,TODATE,ISAPPROVED,ISREJECTED,AUTHORISEDDATE,APPROVEDBY,REMARKS,REASONFORLEAVE,COMPANYID,HALFDAYONFROM,HALFDAYONTO,ISROAPPROVED,ISROREJECTED,ROAPPROVEDBY,ISRO2APPROVED,ISRO2REJECTED,RO2APPROVEDBY) " +
                 " VALUES (@LEAVETRANSID,@LEAVEAPPLIEDDATE,@LEAVETYPEID,@EMPLOYEEID ,@FROMDATE,@TODATE,@ISAPPROVED,@ISREJECTED,@AUTHORISEDDATE,@APPROVEDBY,@REMARKS,@REASONFORLEAVE,@COMPANYID,@HALFDAYONFROM,@HALFDAYONTO,@ISROAPPROVED,@ISROREJECTED,@ROAPPROVEDBY,@ISRO2APPROVED,@ISRO2REJECTED,@RO2APPROVEDBY)" +
                 " SELECT @OUTPUTPARAM = @LEAVETRANSID ";
                break;
            case "UPDATE":
                StrSql = " UPDATE TK_LEAVETRANS SET LEAVETRANSID = @LEAVETRANSID , " +
                                        " LEAVEAPPLIEDDATE = @LEAVEAPPLIEDDATE, " +
                                        " LEAVETYPEID = @LEAVETYPEID," +
                                        " EMPLOYEEID = @EMPLOYEEID," +
                                        " FROMDATE = @FROMDATE," +
                                        " TODATE = @TODATE," +
                                        " ISAPPROVED = @ISAPPROVED, " +
                                        " ISREJECTED = @ISREJECTED, " +
                                        " AUTHORISEDDATE = @AUTHORISEDDATE, " +
                                        " APPROVEDBY = @APPROVEDBY, " +
                                        " HALFDAYONFROM = @HALFDAYONFROM," +
                                        " HALFDAYONTO = @HALFDAYONTO," +
                                        " REMARKS = @REMARKS ," +
                                        " REASONFORLEAVE = @REASONFORLEAVE, " +
                                        " ISROAPPROVED = @ISROAPPROVED, "+
                                        " ISROREJECTED = @ISROREJECTED, " +
                                        " ROAPPROVEDBY = @ROAPPROVEDBY, "+
                                        " ISRO2APPROVED = @ISRO2APPROVED, " +
                                        " ISRO2REJECTED = @ISRO2REJECTED, " +
                                        " RO2APPROVEDBY = @RO2APPROVEDBY, " +
                                        " COMPANYID = @COMPANYID " +
                              "	WHERE LEAVETRANSID = @LEAVETRANSID " +
                               " SELECT @OUTPUTPARAM = @LEAVETRANSID  ";
                break;
            case "DELETE":
                StrSql = " DELETE FROM TK_LEAVETRANS " +
                     " WHERE LEAVETRANSID = @LEAVETRANSID " +
                     "  SELECT @OUTPUTPARAM = @LEAVETRANSID ";
                break;
        }

        return StrSql;
    }

    public override long GetExecuteCommand(string TransType, SqlTransaction SqlTrans)
    {
        try
        {
            SqlCmd = new SqlCommand();
            SqlCmd.Connection = SqlTrans.Connection;
            SqlCmd.Transaction = SqlTrans;
            SqlCmd.CommandText = GetProcStr(TransType.ToUpper());
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.Parameters.AddWithValue("@LEAVETRANSID", LeaveTransID);
            if (TransType.ToUpper() != "DELETE")
            {
                SqlCmd.Parameters.AddWithValue("@LEAVETYPEID", LeaveTypeID);
                SqlCmd.Parameters.AddWithValue("@LEAVEAPPLIEDDATE", LeaveAppliedDate);
                SqlCmd.Parameters.AddWithValue("@EMPLOYEEID", EmployeeID);
                SqlCmd.Parameters.AddWithValue("@FROMDATE", FromDate);
                SqlCmd.Parameters.AddWithValue("@TODATE", ToDate);
                SqlCmd.Parameters.AddWithValue("@ISAPPROVED", IsApproved);
                SqlCmd.Parameters.AddWithValue("@ISREJECTED", IsRejected);
                if (IsApproved || IsRejected)
                    SqlCmd.Parameters.AddWithValue("@AUTHORISEDDATE", AuthorisedDate);
                else
                    SqlCmd.Parameters.AddWithValue("@AUTHORISEDDATE", System.DBNull.Value);
                SqlCmd.Parameters.AddWithValue("@APPROVEDBY", ApprovedBy);
                SqlCmd.Parameters.AddWithValue("@HALFDAYONFROM", HalfDayOnFrom);
                SqlCmd.Parameters.AddWithValue("@HALFDAYONTO", HalfDayOnTo);
                SqlCmd.Parameters.AddWithValue("@REMARKS", Remarks);
                SqlCmd.Parameters.AddWithValue("@REASONFORLEAVE", Reason);
                SqlCmd.Parameters.AddWithValue("@COMPANYID", CompanyID);
                SqlCmd.Parameters.AddWithValue("@ISROAPPROVED", IsROApproved );
                SqlCmd.Parameters.AddWithValue("@ISROREJECTED", IsRORejected );
                SqlCmd.Parameters.AddWithValue("@ROAPPROVEDBY", RoApprovedBy );

                SqlCmd.Parameters.AddWithValue("@ISRO2APPROVED", IsRO2Approved);
                SqlCmd.Parameters.AddWithValue("@ISRO2REJECTED", IsRO2Rejected);
                SqlCmd.Parameters.AddWithValue("@RO2APPROVEDBY", Ro2ApprovedBy);


            }
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
        StrSql = "SELECT LTR.LEAVETRANSID, EM.EMPLOYEECODE,EM.EMPLOYEERFID, EM.EMPLOYEEFIRSTNAME + ' ' + EM.EMPLOYEELASTNAME AS EMPLOYEENAME, " +
          " LT.LEAVETYPENAME ,   CONVERT(VARCHAR,LTR.FROMDATE,106) AS FROMDATE , CONVERT(VARCHAR,LTR.TODATE,106) AS TODATE " +
          " FROM TK_LEAVETRANS LTR " +
          " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = LTR.EMPLOYEEID  " +
          "  INNER JOIN TK_LEAVETYPE LT ON LT.LEAVETYPEID = LTR.LEAVETYPEID " +
            " WHERE EM.COMPANYID = " + CompanyID.ToString();
        if (condStr.Trim() != "") { StrSql = StrSql + " AND " + condStr; }
        return StrSql;
    }

    public Boolean LeaveApplicationExists(int daysapplied)
    {
        SqlConn = GlobalFunctions.GetConnection();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
        for (DateTime d = Convert.ToDateTime(FromDate.ToString("dd/MMM/yy")); d <= Convert.ToDateTime(ToDate.ToString("dd/MMM/yy")); d = d.AddDays(1))
        {
            StrSql = "SELECT * FROM TK_LEAVETRANS WHERE EMPLOYEEID = " + EmployeeID.ToString() + " AND + '" + d.ToString("dd/MMM/yy") + "' AND '" + d.ToString("dd/MMM/yy") + "' BETWEEN FROMDATE AND TODATE AND ISREJECTED = 0 AND LEAVETRANSID <> " + LeaveTransID.ToString();
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


    public Boolean LeaveApplicationExists()
    {
        SqlConn = GlobalFunctions.GetConnection();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
        for (DateTime d = Convert.ToDateTime(FromDate.ToString("dd/MMM/yy")); d <= Convert.ToDateTime(ToDate.ToString("dd/MMM/yy")); d = d.AddDays(1))
        {
            //StrSql = "SELECT * FROM TK_LEAVETRANS WHERE EMPLOYEEID = " + EmployeeID.ToString() + " AND '" + d.ToString("dd/MMM/yy") + "' BETWEEN FROMDATE AND TODATE AND ISREJECTED = 0 AND LEAVETRANSID <> " + LeaveTransID.ToString();
            StrSql = "SELECT * FROM TK_LEAVETRANS WHERE EMPLOYEEID = " + EmployeeID.ToString() + " AND '" + "' BETWEEN FROMDATE AND TODATE AND ISREJECTED = 0 AND LEAVETRANSID <> " + LeaveTransID.ToString();
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


    public DataTable GetEmployeeOpeningClosing(Int64 EmployeeId, DateTime StDate, DateTime EndDate, Int64 CalendarYearId)
    {
        string StrSql = "";

        //CREATE TEMP TABLE
        StrSql = "CREATE TABLE #TMPLEDGER (EMPLOYEEID BIGINT, LEAVETYPEID INT, OPBAL FLOAT, TAKEN FLOAT, ADJUSTED FLOAT, CLBAL FLOAT) ";
        StrSql += "\nINSERT INTO #TMPLEDGER " +
                  "\nSELECT E.EMPLOYEEID, L.LEAVETYPEID, 0 AS OPBAL, 0 AS TAKENLEAVES, 0 AS ADJUSTEDLEAVES, 0 AS CLBAL " +
                  "\nFROM TK_EMPLOYEE E " +
                  "\nOUTER APPLY TK_LEAVETYPE L " +
                  "\nWHERE E.EMPLOYEEID = @EMPLOYEEID ";

        //OPENING BALANCE FROM CALENDAR YEAR
        StrSql += "UPDATE T SET OPBAL = OP.OPBALANCE, CLBAL = OP.OPBALANCE " +
                  "\nFROM #TMPLEDGER T " +
                  "\nINNER JOIN TK_LEAVEOPBAL OP ON OP.EMPLOYEEID = T.EMPLOYEEID AND OP.LEAVETYPEID = T.LEAVETYPEID " +
                  "\nWHERE OP.CALENDARYEARID = @CALENDARYEARID ";

        //OPENING BALANCE FROM LEAVETRANS
        StrSql += "UPDATE T SET OPBAL = OPBAL - L.TAKEN, TAKEN = T.TAKEN + L.TAKEN " +
                  "\nFROM #TMPLEDGER T " +
                  "\nINNER JOIN " +
                  "\n(SELECT EMPLOYEEID, LEAVETYPEID, " +
                  "\n   SUM(DATEDIFF(DD, FROMDATE, TODATE)+1-(CASE WHEN HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN HALFDAYONTO = 1 THEN 0.5 ELSE 0 END)) AS TAKEN, " +
                  "\n   FROM TK_LEAVETRANS LT " +
                  "\n   INNER JOIN TK_CALENDARYEAR C ON LT.FROMDATE BETWEEN C.STARTINGDATE AND C.ENDINGDATE " +
                  "\n   WHERE LT.ISAPPROVED = 1 AND LT.FROMDATE < @STDATE AND C.CALENDARYEARID = @CALENDARYEARID " +
                  "\n   GROUP BY EMPLOYEEID, LEAVETYPEID) L ON L.EMPLOYEEID = T.EMPLOYEEID AND L.LEAVETYPEID = T.LEAVETYPEID ";

        //OPENING BALANCE FROM LEAVE ADJUSTMENTS
        StrSql += "UPDATE T SET OPBAL = OPBAL + A.ADJUSTEDLEAVES, ADJUSTED = ADJUSTED + A.ADJUSTEDLEAVES " +
                  "\nFROM #TMPLEDGER T " +
                  "\nINNER JOIN " +
                  "\n(SELECT EMPLOYEEID, LEAVETYPEID, SUM(ADJUSTEDLEAVES) AS ADJUSTEDLEAVES " +
                  "\n   FROM TK_LEAVEADJUSTMENT AD " +
                  "\n   INNER JOIN TK_CALENDARYEAR C ON AD.LEAVEADJUSTMENTDATE BETWEEN C.STARTINGDATE AND C.ENDINGDATE " +
                  "\n   WHERE AD.LEAVEADJUSTMENTDATE < @STDATE AND C.CALENDARYEARID = @CALENDARYEARID " +
                  "\n   GROUP BY EMPLOYEEID, LEAVETYPEID) A ON A.EMPLOYEEID = T.EMPLOYEEID AND A.LEAVETYPEID = T.LEAVETYPEID ";

        //CLOSING BALANCE FROM LEAVETRANS
        StrSql += "UPDATE T SET CLBAL = CLBAL - L.TAKEN, TAKEN = T.TAKEN - L.TAKEN " +
                  "\nFROM #TMPLEDGER T " +
                  "\nINNER JOIN " +
                  "\n(SELECT EMPLOYEEID, LEAVETYPEID, " +
                  "\n   SUM(DATEDIFF(DD, FROMDATE, TODATE)+1-(CASE WHEN HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN HALFDAYONTO = 1 THEN 0.5 ELSE 0 END)) AS TAKEN, " +
                  "\n   FROM TK_LEAVETRANS LT " +
                  "\n   INNER JOIN TK_CALENDARYEAR C ON LT.FROMDATE BETWEEN C.STARTINGDATE AND C.ENDINGDATE " +
                  "\n   WHERE LT.ISAPPROVED = 1 AND LT.FROMDATE <= @ENDDATE AND C.CALENDARYEARID = @CALENDARYEARID " +
                  "\n   GROUP BY EMPLOYEEID, LEAVETYPEID) L ON L.EMPLOYEEID = T.EMPLOYEEID AND L.LEAVETYPEID = T.LEAVETYPEID ";


        //CLOSING BALANCE FROM LEAVE ADJUSTMENTS
        StrSql += "UPDATE T SET CLBAL = CLBAL + A.ADJUSTEDLEAVES, ADJUSTED = A.ADJUSTEDLEAVES - ADJUSTED " +
                  "\nFROM #TMPLEDGER T " +
                  "\nINNER JOIN " +
                  "\n(SELECT EMPLOYEEID, LEAVETYPEID, SUM(ADJUSTEDLEAVES) AS ADJUSTEDLEAVES " +
                  "\n   FROM TK_LEAVEADJUSTMENT AD " +
                  "\n   INNER JOIN TK_CALENDARYEAR C ON AD.LEAVEADJUSTMENTDATE BETWEEN C.STARTINGDATE AND C.ENDINGDATE " +
                  "\n   WHERE AD.LEAVEADJUSTMENTDATE <= @ENDDATE AND C.CALENDARYEARID = @CALENDARYEARID " +
                  "\n   GROUP BY EMPLOYEEID, LEAVETYPEID) A ON A.EMPLOYEEID = T.EMPLOYEEID AND A.LEAVETYPEID = T.LEAVETYPEID ";


        StrSql += "SELECT L.LEAVETYPENAME AS LEAVETYPE, OPBAL, T.TAKEN, T.ADJUSTED, CLBAL FROM #TMPLEDGER T " +
                 "\nINNER JOIN TK_EMPLOYEE E ON E.EMPLOYEEID = T.EMPLOYEEID " +
                 "\nINNER JOIN TK_LEAVETYPE L ON L.LEAVETYPEID = T.LEAVETYPEID ";
        StrSql += "\nDROP TABLE #TMPLEDGER";

        SqlCmd = new SqlCommand();
        SqlCmd.CommandType = CommandType.Text;
        SqlCmd.CommandText = StrSql;
        SqlCmd.Connection = GlobalFunctions.GetConnection();
        SqlCmd.Parameters.AddWithValue("@STDATE", StDate);
        SqlCmd.Parameters.AddWithValue("@ENDDATE", EndDate);
        SqlCmd.Parameters.AddWithValue("@CALENDARYEARID", CalendarYearId);
        SqlCmd.Parameters.AddWithValue("@EMPLOYEEID", EmployeeId.ToString());
        SqlDataAdapter SDA = new SqlDataAdapter(SqlCmd);
        DataTable DT = new DataTable("EMPOP");
        SDA.Fill(DT);
        return DT;


    }

    public DataTable GetEmployeeLeaveAdjDates(Int64 EmployeeId, DateTime StDate, DateTime EndDate, Int64 CalendarYearId)
    {
        string StrSql = "";
        //CREATING TEMPTABLE
        StrSql = "CREATE TABLE #TMPLEDGER (LEAVETRANSID BIGINT, APPLIEDDATE DATETIME, EMPLOYEEID BIGINT, LEAVETYPEID INT, LFRDATE DATETIME, LTODATE DATETIME, ADJUSTMENT FLOAT, TAKEN FLOAT, REMARKS VARCHAR(500), ISAPPROVED BIT, ISREJECTED BIT, APPROVEDBY VARCHAR(100), REASONFORLEAVE VARCHAR(500)) ";

        //LEAVE TRANSACTION DETAILS
        StrSql += "INSERT INTO #TMPLEDGER " +
                  "\nSELECT L.LEAVETRANSID, L.LEAVEAPPLIEDDATE, L.EMPLOYEEID, L.LEAVETYPEID, FROMDATE, TODATE, 0 AS ADJUSTMENT, " +
            //"\nCASE WHEN L.HALFDAYONFROM = 1 THEN CONVERT(FLOAT, DATEDIFF(DD, L.FROMDATE, L.TODATE)+1)/2 ELSE CONVERT(FLOAT, DATEDIFF(DD, L.FROMDATE, L.TODATE))+1 END AS TAKEN, " +
                  "\nDATEDIFF(DD, FROMDATE, TODATE)+1-(CASE WHEN HALFDAYONFROM = 1 THEN 0.5 ELSE 0 END)-(CASE WHEN TODATE > FROMDATE AND HALFDAYONTO = 1 THEN 0.5 ELSE 0 END) AS TAKEN, " +
                  "\nL.REMARKS AS REMARKS, L.ISAPPROVED, L.ISREJECTED, L.APPROVEDBY, L.REASONFORLEAVE  " +
                  "\nFROM TK_LEAVETRANS L " +
                  "\nINNER JOIN TK_CALENDARYEAR C ON L.FROMDATE BETWEEN C.STARTINGDATE AND C.ENDINGDATE " +
                  "\nWHERE L.EMPLOYEEID = @EMPLOYEEID AND C.CALENDARYEARID = @CALENDARYEARID AND L.FROMDATE BETWEEN @STDATE AND @ENDDATE ";

        ////LEAVE ADJUSTMENTS
        //StrSql += "INSERT INTO #TMPLEDGER " +
        //          "\nSELECT 0 AS LEAVETRANSID, A.EMPLOYEEID, A.LEAVETYPEID, LEAVEADJUSTMENTDATE AS LFRDATE, NULL AS LTODATE, ADJUSTEDLEAVES, 0 AS TAKEN, " +
        //          "\nA.REMARKS, CONVERT(BIT,1) AS ISAPPROVED, '' AS APPROVEDBY " +
        //          "\nFROM TK_LEAVEADJUSTMENT A " +
        //          "\nINNER JOIN TK_CALENDARYEAR C ON A.LEAVEADJUSTMENTDATE BETWEEN C.STARTINGDATE AND C.ENDINGDATE " +
        //          "\nWHERE A.EMPLOYEEID = @EMPLOYEEID AND C.CALENDARYEARID = @CALENDARYEARID AND A.LEAVEADJUSTMENTDATE BETWEEN @STDATE AND @ENDDATE ";

        StrSql += "SELECT LEAVETRANSID, T.APPLIEDDATE, E.EMPLOYEECODE, E.EMPLOYEEFIRSTNAME + ' ' + E.EMPLOYEELASTNAME AS EMPLOYEENAME, L.LEAVETYPENAME AS LEAVETYPE, LFRDATE, LTODATE, " +
                  "\nADJUSTMENT AS ADJ, TAKEN, T.REMARKS, T.REASONFORLEAVE, ISAPPROVED, ISREJECTED, APPROVEDBY, CASE WHEN ISAPPROVED = 1 THEN 'APPROVED' WHEN ISREJECTED = 1 THEN 'REJECTED' ELSE 'APPROVAL PENDING' END AS STATUS  FROM #TMPLEDGER T " +
                  "\nINNER JOIN TK_EMPLOYEE E ON E.EMPLOYEEID = T.EMPLOYEEID " +
                  "\nINNER JOIN TK_LEAVETYPE L ON L.LEAVETYPEID = T.LEAVETYPEID " +
                  "\nORDER BY LFRDATE DESC ";
        StrSql += "\nDROP TABLE #TMPLEDGER";

        SqlCmd = new SqlCommand();
        SqlCmd.CommandType = CommandType.Text;
        SqlCmd.CommandText = StrSql;
        SqlCmd.Connection = GlobalFunctions.GetConnection();
        SqlCmd.Parameters.AddWithValue("@STDATE", StDate);
        SqlCmd.Parameters.AddWithValue("@ENDDATE", EndDate);
        SqlCmd.Parameters.AddWithValue("@CALENDARYEARID", CalendarYearId);
        SqlCmd.Parameters.AddWithValue("@EMPLOYEEID", EmployeeId.ToString());
        SqlDataAdapter SDA = new SqlDataAdapter(SqlCmd);
        DataTable DT = new DataTable("EMPTRANS");
        SDA.Fill(DT);
        return DT;
    }



}

