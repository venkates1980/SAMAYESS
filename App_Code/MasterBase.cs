using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public class MasterBase
{
    public bool Added;
    public bool Modified;
    public bool Deleted;
    protected string StrSql = "";
    protected SqlCommand SqlCmd;
    protected DataSet DS = new DataSet();
    protected SqlDataAdapter SDA = new SqlDataAdapter();
    public String EntryHeader;
    public String EntryDescription;
    public Int64 TransID;
    public Int64 CompanyID;
    public string DisplayColumnsList = "";
    public SqlConnection SqlConn;

    public Int64 Add(SqlTransaction SqlTrans, Boolean MsgboxDisplay)
    {
        TransID = GetExecuteCommand("INSERT", SqlTrans);
        if (TransID > 0)
        {
            //if (MsgboxDisplay) { SamayMsgBox .Show("Sucessfully Saved/Ref No : " + TransID.ToString(), "Add", SamayMsgBox.PIButtons.OK, SamayMsgBox.PIIcon.Saved); }
        }
        return TransID;
    }

    public Int64 Modify(SqlTransaction SqlTrans, Boolean MsgboxDisplay)
    {
        TransID = GetExecuteCommand("UPDATE", SqlTrans);
        if (TransID > 0)
        {
            //if (MsgboxDisplay) { SamayMsgBox.Show("Sucessfully Updated/Ref No : " + TransID.ToString(), "Add", SamayMsgBox.PIButtons.OK, SamayMsgBox.PIIcon.Saved); }
        }
        return TransID;
    }

    public Int64 Delete(SqlTransaction SqlTrans, Boolean MsgboxDisplay)
    {
        if (MsgboxDisplay)
        {
            //if (SamayMsgBox.Show("Are U Sure to Remove this Record", "Delete", SamayMsgBox.PIButtons.YesNo, SamayMsgBox.PIIcon.Question) == DialogResult.No)
            //{ return 0; }
        }

        TransID = GetExecuteCommand("DELETE", SqlTrans);

        if (TransID > 0)
        {
            //if (MsgboxDisplay) { SamayMsgBox.Show("Sucessfully Removed/Ref No : " + TransID.ToString(), "Add",SamayMsgBox.PIButtons.OK, SamayMsgBox.PIIcon.Saved); }
        }
        return TransID;
    }



    public virtual string GetTransQuery(string TransType)
    {
        StrSql = "";
        return StrSql;
    }


    public virtual Int64 GetExecuteCommand(string TransType, SqlTransaction SqlTrans)
    {
        return 999;
    }

    public virtual string GetViewQuery(string condStr)
    {
        StrSql = "";
        return StrSql;
    }

    public DataTable GetDataList(string condSql)
    {
        DataTable DT = new DataTable();
        DT = GlobalFill.FillDataTable(GetViewQuery(condSql));
        //SqlDataAdapter SDA = new SqlDataAdapter(StrSql, GlobalVariables.SqlConn );
        //SDA.Fill(DT);
        return DT;
    }

    public virtual void DisplayForm(string Mode, int ID)
    {

    }

    public virtual void LoadAttributes(Int64 ID)
    {

    }

}

