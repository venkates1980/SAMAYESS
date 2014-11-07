using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GlobalFill
/// </summary>
public static class GlobalFill
{

    static SqlCommand SqlCmd;
    static DataSet DS = new DataSet();
    static SqlDataAdapter SDA = new SqlDataAdapter();
    static SqlConnection SqlConn = new SqlConnection( System.Configuration.ConfigurationManager.ConnectionStrings["samayConnectionString"].ConnectionString);

    public static byte[] StringToByte(string input)
    {
        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        return enc.GetBytes(input);
    }

    public static string ByteToString(byte[] input)
    {
        return System.Text.ASCIIEncoding.ASCII.GetString(input);
    }

    public static object GetQueryValue(string StrSql)
    {

        SqlConnection.ClearAllPools();
        object ObjRtrn;
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
        SqlCmd = new SqlCommand(StrSql, SqlConn);

        ObjRtrn = SqlCmd.ExecuteScalar();
        SqlCmd.Dispose();
        SqlConn.Close();
        return ObjRtrn;
    }


    //GLOBAL FUNCTION IS TO FILL THE TABLE
    public static DataTable FillDataTable(string paramSql)
    {
        SqlConnection.ClearAllPools();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
        SDA = new SqlDataAdapter(paramSql, SqlConn);
        DataTable rtDT = new DataTable();
        SDA.Fill(rtDT);
        SDA.Dispose();
        SqlConn.Close();
        return rtDT;
    }

    public static void FillDataSet(string paramSql, string tblName, DataSet DS, SqlDataAdapter SDA)
    {
        SqlConnection.ClearAllPools();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }

        if (object.ReferenceEquals(DS.Tables[tblName], null) == false) { DS.Tables[tblName].Clear(); DS.Tables[tblName].Dispose(); }
        SDA = new SqlDataAdapter(paramSql, SqlConn);
        SDA.Fill(DS, tblName);
        SDA.Dispose();
        SqlConn.Close();



    }

    public static void FillDataSet(string tblName, DataSet DS, SqlDataAdapter SDA)
    {
        SqlConnection.ClearAllPools();
        if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }

        if (object.ReferenceEquals(DS.Tables[tblName], null) == false) DS.Tables[tblName].Clear();
        if (ReferenceEquals(SDA.SelectCommand.Connection, null))
        { throw new Exception("Connection not Assigned to this Adapter"); }
        SDA.Fill(DS, tblName);
        SDA.Dispose();
        SqlConn.Close();
    }



    public static void FillCombo(string paramStrSql, DropDownList paramDropDown)
    {
        try
        {
            SDA = new SqlDataAdapter(paramStrSql, SqlConn);
            DataSet DSCombo = new DataSet();
            SDA.Fill(DSCombo, paramDropDown.ID);
            if (DSCombo.Tables[paramDropDown.ID].Rows.Count > 0)
            {
                string tmpSelVal = "";
                paramDropDown.DataSource = DSCombo.Tables[paramDropDown.ID];
                paramDropDown.DataValueField = DSCombo.Tables[paramDropDown.ID].Columns[0].Caption;
                paramDropDown.DataTextField = DSCombo.Tables[paramDropDown.ID].Columns[1].Caption;
                paramDropDown.DataBind();
                if (tmpSelVal != "")
                {
                    paramDropDown.SelectedValue = tmpSelVal;
                }
                else
                {
                    paramDropDown.SelectedValue = "";
                }
                if (object.Equals(SDA, null) == false) { SDA.Dispose(); }
            }

        }
        catch (Exception E)
        {
            throw E;
        }
    }

    public static void FillDropDown(DataTable pardt, DropDownList pardd)
    {
        if (pardt.Rows.Count > 0)
        {
            string tmpSelVal = "";
            pardd.Items.Clear();
            pardd.DataSource = pardt;
            pardd.DataValueField = pardt.Columns[0].Caption;
            pardd.DataTextField = pardt.Columns[1].Caption;
            pardd.DataBind();
            if (tmpSelVal != "")
            { pardd.SelectedValue = tmpSelVal; }
            ////else
            ////{ 
            ////  //pardd.SelectedValue = ""; 
            ////}
        }
    }

    public static void FillListBox(DataTable pardt, ListBox parLst)
    {
        if (pardt.Rows.Count > 0)
        {
            parLst.Items.Clear();
            parLst.DataSource = pardt;
            parLst.DataValueField = pardt.Columns[0].Caption;
            parLst.DataTextField = pardt.Columns[1].Caption;
            parLst.DataBind();
        }
    }

    public static void FillCheckListBox(DataTable pardt, CheckBoxList parLst)
    {
        if (pardt.Rows.Count > 0)
        {
            parLst.Items.Clear();
            parLst.DataSource = pardt;
            parLst.DataValueField = pardt.Columns[0].Caption;
            parLst.DataTextField = pardt.Columns[1].Caption;
            parLst.DataBind();
        }
    }

    public static void FillMonth(DropDownList pardd)
    {
        pardd.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            System.Globalization.DateTimeFormatInfo d = new System.Globalization.DateTimeFormatInfo();
            ListItem Lst = new ListItem(d.GetMonthName(i), i.ToString());
            pardd.Items.Add(Lst);
        }
    }

    public static void FillYear(DropDownList pardd)
    {
        pardd.Items.Clear();
        for (int i = DateTime.Today.Year; i >= 2000; i--)
        {
            ListItem Lst = new ListItem(i.ToString(), i.ToString());
            pardd.Items.Add(Lst);
        }
    }

}
