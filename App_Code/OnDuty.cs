using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ParaSysCom;
using System.Data;
//namespace SamayBioLib
//{
    public class OnDutyTransCollection : System.Collections.CollectionBase
    {
        public void Add(OnDuty  ObjOnDuty)
        {
            this.List.Add(ObjOnDuty);
        }

        public virtual OnDuty  this[int index]
        {
            get { return (OnDuty )this.List[index]; }
            set { this.List[index] = value; }
        }

        //TOTAL DATATABLE FOR ENTIRE COLLECTION
        public DataTable GetCollectionTable()
        {
            DataTable dtCl = new DataTable();
            OnDuty .FilldtCl();
            dtCl = OnDuty.DtOnDutyTrans ;

            dtCl.Rows.Clear();

            try
            {
                foreach (OnDuty  ObjOnDuty in this.List)
                {
                    if (ObjOnDuty.Deleted == false)
                        {
                            DataRow DR = dtCl.NewRow();
                            DataRow DRTrans = ObjOnDuty.GetDataRow();
                            foreach (DataColumn dc in dtCl.Columns)
                            {
                                DR[dc.ColumnName] = DRTrans[dc.ColumnName];
                            }
                            DR["SNo"] = List.IndexOf(ObjOnDuty) + 1;
                            dtCl.Rows.Add(DR);
                        }
                }
                return dtCl;
            }
            catch (Exception E)
            {
                ////SamayMsgBox.Show(E.Message, "ObjOnDuty.GetCollectionTable");
                return dtCl;
            }
        }
    }


    public class OnDuty : MasterBase
    {
        public Int64 EmployeeID;
        public Int64 OnDutyID;
        public Int64 AttendanceID;
        public DateTime fromdate;
        public DateTime FromTime;// = System.DateTime.Now ;
        public DateTime ToTime;// = System.DateTime.Now ;
        public String Description = "";
        public String ApprovedBy = "";
        public Int32 WorkedMinutes = 0;
        public Boolean LateComingExclude = false;
        public Boolean EarlyGoingExclude = false;
        public Boolean IncludeInWorkingHours = false;

        public static DataTable DtOnDutyTrans = new DataTable();
        public OnDuty()
        {
            //LoadHeaders();
        }

        public OnDuty(Int64 ID)
        {
            OnDutyID  = ID;
            LoadAttributes(ID);
            //LoadHeaders();
        }
        //public void LoadHeaders()
        //{
        //    EntryHeader = "OnDuty";
        //    EntryDescription = "";
        //    SearchColumnName = "EmployeeCode";
        //}

        public override void LoadAttributes(long ID)
        {
            try
            {
                StrSql = "SELECT * FROM TK_ONDUTY WHERE ONDUTYID = " + ID;
                SqlDataReader DR;
                SqlCmd = new SqlCommand(StrSql, GlobalVariables.SqlConn);
                DR = SqlCmd.ExecuteReader();
                if (DR.Read())
                {
                    OnDutyID  = Convert.ToInt64(DR["ONDUTYID"]);
                    AttendanceID  = Convert.ToInt64 (DR["ATTENDANCEID"]);
                    FromTime = Convert.ToDateTime(DR["FROMTIME"]);
                    ToTime = Convert.ToDateTime(DR["TOTIME"]);
                    Description = Convert.ToString(DR["DESCRIPTION"]);
                    ApprovedBy = Convert.ToString(DR["APPROVEDBY"]);
                    CompanyID = Convert.ToInt64(DR["COMPANYID"]);
                    WorkedMinutes = Convert.ToInt32(DR["WORKEDMINUTES"]);
                    LateComingExclude = Convert.ToBoolean(DR["LATECOMINGEXCLUDE"]);
                    EarlyGoingExclude = Convert.ToBoolean(DR["EARLYGOINGEXCLUDE"]);
                    IncludeInWorkingHours = Convert.ToBoolean(DR["INCLUDEINWORKINGHOURS"]);
                }
                DR.Close();
            }
            catch (Exception E)
            {
                ////SamayMsgBox.Show(E.Message);
            }
            //try
            //{
            //    string strquery = "select * from tk_Employee where EmployeeID='"++"'";
            //}
            //catch (Exception E)
            //{
            //}
        }
        public string GetProcStr(String TransType)
        {
            StrSql = "";
            switch (TransType.ToUpper())
            {
                case "INSERT":
                    StrSql = " SELECT @ONDUTYID = ISNULL(MAX(ONDUTYID),0)+1 FROM TK_ONDUTY " +
                     " INSERT INTO TK_ONDUTY(ONDUTYID,ATTENDANCEID,FROMTIME,TOTIME,DESCRIPTION,APPROVEDBY,COMPANYID,WORKEDMINUTES,LATECOMINGEXCLUDE,EARLYGOINGEXCLUDE,INCLUDEINWORKINGHOURS) " +
                     " VALUES (@ONDUTYID,@ATTENDANCEID,@FROMTIME,@TOTIME,@DESCRIPTION,@APPROVEDBY,@COMPANYID,@WORKEDMINUTES,@LATECOMINGEXCLUDE,@EARLYGOINGEXCLUDE,@INCLUDEINWORKINGHOURS)" +
                     " SELECT @OUTPUTPARAM = @ONDUTYID ";
                    break;
                case "UPDATE":
                    StrSql = " UPDATE TK_ONDUTY SET ONDUTYID  = @ONDUTYID , " +
                                            " ATTENDANCEID = @ATTENDANCEID," +
                                            " FROMTIME = @FROMTIME, "+
                                            " TOTIME = @TOTIME, "+
                                            " DESCRIPTION = @DESCRIPTION, "+
                                            " APPROVEDBY = @APPROVEDBY, "+
                                            " WORKEDMINUTES = @WORKEDMINUTES, "+
                                            " LATECOMINGEXCLUDE = @LATECOMINGEXCLUDE, "+
                                            " EARLYGOINGEXCLUDE = @EARLYGOINGEXCLUDE, "+
                                            " INCLUDEINWORKINGHOURS = @INCLUDEINWORKINGHOURS, "+
                                            " COMPANYID = @COMPANYID " +
                                  "	WHERE ONDUTYID  = @ONDUTYID " +
                                   " SELECT @OUTPUTPARAM = @ONDUTYID ";
                    break;
                case "DELETE":
                    StrSql = " DELETE FROM TK_ONDUTY" +
                         " WHERE ONDUTYID = @ONDUTYID " +
                         "  SELECT @OUTPUTPARAM = @ONDUTYID ";
                    break;
            }

            return StrSql;
        }

        //venkat comments temporry
        public override long GetExecuteCommand(string TransType, SqlTransaction SqlTrans)
        {
            try
            {
                SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlTrans.Connection;
                SqlCmd.Transaction = SqlTrans;
                SqlCmd.CommandText = GetProcStr(TransType.ToUpper());
                SqlCmd.CommandType = CommandType.Text;
                SqlCmd.Parameters.AddWithValue("@ONDUTYID", OnDutyID );
                SqlCmd.Parameters.AddWithValue("@ATTENDANCEID", AttendanceID );
                SqlCmd.Parameters.AddWithValue("@FROMTIME", FromTime );
                SqlCmd.Parameters.AddWithValue("@TOTIME", ToTime );
                SqlCmd.Parameters.AddWithValue("@DESCRIPTION", Description );
                SqlCmd.Parameters.AddWithValue("@APPROVEDBY", ApprovedBy );
                SqlCmd.Parameters.AddWithValue("@WORKEDMINUTES", WorkedMinutes );
                SqlCmd.Parameters.AddWithValue("@LATECOMINGEXCLUDE", LateComingExclude );
                SqlCmd.Parameters.AddWithValue("@EARLYGOINGEXCLUDE", EarlyGoingExclude );
                SqlCmd.Parameters.AddWithValue("@INCLUDEINWORKINGHOURS", IncludeInWorkingHours );
                SqlCmd.Parameters.AddWithValue("@COMPANYID", CompanyID);
                SqlParameter OutPutParameter = SqlCmd.Parameters.AddWithValue("OUTPUTPARAM", 0);
                OutPutParameter.Direction = ParameterDirection.InputOutput;
                if (SqlCmd.ExecuteNonQuery() > 0)
                {
                    // FOR LATECOMINGEXCLUDE 
                    Int64 i = 0;string SqlStr = "";
                   if(LateComingExclude)
                   {
                     i= 0;
                   }
                   else
                   {
                   SqlStr = " SELECT COUNT(*) FROM TK_ONDUTY WHERE ATTENDANCEID = " +AttendanceID  + " AND LATECOMINGEXCLUDE = 1 AND ONDUTYID <>  "+ OnDutyID ;
                   SqlCmd.CommandText = SqlStr;
                   i = Convert.ToInt64(  SqlCmd.ExecuteScalar  ());
                   if (i <= 0)
                   {
                       SqlStr = " SELECT COUNT(*) FROM TK_TIMEPERMISSION WHERE ATTENDANCEID = " + AttendanceID + " AND LATECOMINGEXCLUDE = 1 ";
                       SqlCmd.CommandText = SqlStr;
                       i = Convert.ToInt64(SqlCmd.ExecuteScalar());
                   }

                   }
                   if (i <= 0)
                    {
                        SqlStr = " UPDATE TK_ATTENDANCE SET LATECOMINGEXCLUDE = '" + LateComingExclude + "' WHERE ATTENDANCEID = " + AttendanceID;
                        SqlCmd.CommandText = SqlStr;
                        i = SqlCmd.ExecuteNonQuery();

                    }

                    //FOR EARLYGOINGEXCLUDE 
                    i = 0;  SqlStr = "";
                   if (EarlyGoingExclude)
                   {
                       i = 0;
                   }
                   else
                   {
                       SqlStr = " SELECT COUNT(*) FROM TK_ONDUTY WHERE ATTENDANCEID = " + AttendanceID + " AND EARLYGOINGEXCLUDE = 1 AND ONDUTYID <>  " + OnDutyID;
                       SqlCmd.CommandText = SqlStr;
                       i = Convert.ToInt64(SqlCmd.ExecuteScalar());
                       if (i <= 0)
                       {
                           SqlStr = " SELECT COUNT(*) FROM TK_TIMEPERMISSION WHERE ATTENDANCEID = " + AttendanceID + " AND EARLYGOINGEXCLUDE = 1 ";
                           SqlCmd.CommandText = SqlStr;
                           i = Convert.ToInt64(SqlCmd.ExecuteScalar());
                       }

                   }
                   if (i <= 0)
                   {
                    
                    if (TransType.ToUpper() == "DELETE"){
                        SqlStr = " UPDATE TK_ATTENDANCE SET EARLYGOINGEXCLUDE = 'false' , LATECOMINGEXCLUDE = 'false' , ISONOD = 0 WHERE ATTENDANCEID = " + AttendanceID;
                        
                    }
                    else {
                        SqlStr = " UPDATE TK_ATTENDANCE SET EARLYGOINGEXCLUDE = '" + EarlyGoingExclude + "' , LATECOMINGEXCLUDE = '" + LateComingExclude + "' WHERE ATTENDANCEID = " + AttendanceID;
                    }
                        
                       SqlCmd.CommandText = SqlStr;
                       i = SqlCmd.ExecuteNonQuery();
                   }


                   if (TransType.ToUpper() == "DELETE")
                   {
                       SqlCommand SqlCmd2 = new SqlCommand();
                       SqlCmd2.Connection = SqlTrans.Connection;
                       SqlCmd2.Transaction = SqlTrans;
                       SqlStr = " UPDATE TK_ATTENDANCE SET  EARLYGOINGEXCLUDE = 0 , LATECOMINGEXCLUDE = 0 WHERE ATTENDANCEID = " + AttendanceID;
                       SqlCmd.CommandText = SqlStr;
                       i = SqlCmd.ExecuteNonQuery();
                   }

                   //if (TransType.ToUpper() == "DELETE")
                   //{

                   //    SqlStr = " UPDATE TK_ATTENDANCE SET EARLYINGEXCLUDE = 'false' , LATECOMINGEXCLUDE = 'false' WHERE ATTENDANCEID = " + AttendanceID;
                   //    SqlCmd.CommandText = SqlStr;
                   //    i = SqlCmd.ExecuteNonQuery();
                   //}


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
            if (condStr.Equals("100") || condStr.Equals("500") || condStr.Equals("1000"))
            {
                StrSql = "SELECT top(" + condStr + ")  ";
                condStr = "";
            }
            else
            {
                StrSql = "SELECT  ";
            }
            StrSql += " OD.ONDUTYID, EM.EMPLOYEECODE, EM.EMPLOYEEFIRSTNAME + ' ' + EM.EMPLOYEELASTNAME AS EMPLOYEENAME , "+
                " AT.ATTENDANCEDATE , OD.FROMTIME,OD.TOTIME,OD.WORKEDMINUTES "+
                " FROM TK_ONDUTY OD "+
                " INNER JOIN TK_ATTENDANCE AT ON AT.ATTENDANCEID= OD.ATTENDANCEID "+
                " INNER JOIN TK_EMPLOYEE EM ON EM.EMPLOYEEID = AT.EMPLOYEEID  WHERE OD.COMPANYID = " + CompanyID;
            if ((condStr.Trim().Length > 3) && (condStr.Trim() != ""))
            {
                StrSql = StrSql + " AND " + condStr;
            }
            StrSql += " order by  OD.FROMTIME desc ";
            return StrSql;
        }
        //public override void SetColWidth(System.Windows.Forms.DataGridView DgVw)
        //{
        //    DgVw.Columns["OnDutyID"].Visible = false; 
        //    DgVw.Columns["EMPLOYEECODE"].Width = 100;
        //    DgVw.Columns["EMPLOYEENAME"].Width = 400;
        //    DgVw.Columns["ATTENDANCEDATE"].Width = 100;
        //    DgVw.Columns["ATTENDANCEDATE"].DefaultCellStyle.Format = "dd MMM yyyy";
        //    DgVw.Columns["FROMTIME"].Width = 100;
        //    DgVw.Columns["TOTIME"].Width = 100;
        //    DgVw.Columns["FROMTIME"].DefaultCellStyle.Format = "HH : mm";
        //    DgVw.Columns["TOTIME"].DefaultCellStyle.Format = "HH : mm";
        //    DgVw.Columns["WORKEDMINUTES"].Width = 50;
        //}
        //public override void SetColWidthStr()
        //{
        //    DisplayColumnsList = "EmployeeCode,EmployeeName,AttendanceDate,FromTime,ToTime";
        //    DisplayColWidthPerStr = "10,35,25,15,15";
        //}
        
         





        public DataRow GetDataRow()
        {

            DataRow DR = DtOnDutyTrans.NewRow();
        //    LeaveType ObjLeaveType = new LeaveType(LeaveTypeID);
            DR["ONDUTYID"] = Convert.ToInt64(OnDutyID );
            DR["ATTENDANCEID"] = Convert.ToInt64(AttendanceID );
            DR["FROMTIME"] = Convert.ToDateTime (FromTime );
            DR["TOTIME"] = Convert.ToDateTime(ToTime );
            DR["DESCRIOTION"] = Convert.ToDouble(Description );
            DR["APPROVEDBY"] = Convert.ToString(ApprovedBy );
            return DR;
        }

        public static void FilldtCl()
        {
            DtOnDutyTrans.Columns.Clear();
            DtOnDutyTrans.Columns.Add("SNo", typeof(Int16));
            DtOnDutyTrans.Columns.Add("ONDUTYID", typeof(Int64));
            DtOnDutyTrans.Columns.Add("ATTENDANCEID", typeof(Int64));
            DtOnDutyTrans.Columns.Add("FROMTIME", typeof(DateTime ));
            DtOnDutyTrans.Columns.Add("TODATE", typeof(DateTime ));
            DtOnDutyTrans.Columns.Add("DESCRIPTION", typeof(string ));
            DtOnDutyTrans.Columns.Add("APPROVEDBY", typeof(string ));
            

        }

    }

//}