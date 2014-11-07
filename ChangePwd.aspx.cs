using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        if (Convert.ToInt64(GInf .EmployeeId ) == 0)
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        if (txtconfirmpwd.Text == "")
        {
            MessageBox msgbox = new MessageBox( "/msgbox.tpl");
            msgbox.SetTitle("Information Required");
            msgbox.SetIcon("msg_icon_1.png");
            msgbox.SetMessage("Please Enter Passwords ");

            msgbox.SetOKButton("msg_button_class");
            msgboxpanel.InnerHtml = msgbox.ReturnObject();
            return;
        }

        if (txtnewpwd.Text == txtconfirmpwd.Text)
        {
            SqlConnection SqlConn = GlobalFunctions.GetConnection();
            if (SqlConn.State == ConnectionState.Closed) { SqlConn.Open(); }
            SqlTransaction SqlTrans = SqlConn.BeginTransaction();
            SqlCommand sqlcmd = new SqlCommand("", SqlConn );
            sqlcmd.Transaction = SqlTrans;
            sqlcmd.CommandText = "UPDATE TK_EMPLOYEE SET KPWD = '" + txtnewpwd.Text + "'" + " WHERE EMPLOYEEID =  " + GInf.EmployeeId .ToString ();
            sqlcmd.ExecuteNonQuery();
            SqlTrans.Commit();

            SqlConn.Dispose();
            MessageBox msgbox = new MessageBox("/msgbox.tpl");
            msgbox.SetTitle("Updated");
            msgbox.SetIcon("tick32.png");
            msgbox.SetMessage("Password Updated Sucessfully");
            msgbox.SetOKButton("msg_button_class");
            msgboxpanel.InnerHtml = msgbox.ReturnObject();
           
        }
        else
        {
            lblMessage.Text = " Passwords mismatch, Please reenter... ";
        }
    }
}
