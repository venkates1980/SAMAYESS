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
using System.IO;

public partial class ShowEmployeeImage : System.Web.UI.Page
{
   // GlobalInformation GInf;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Request.QueryString["Id"] != null)
        {
            string StrSql = "";
            StrSql = "SELECT PHOTO FROM TK_EMPLOYEE  WHERE EMPLOYEEID = " + Convert.ToString(Request.QueryString["Id"]);

            
                Response.ContentType = "image/jpg";
                if (GlobalFill.GetQueryValue(StrSql) != System.DBNull.Value)
                {
                    Response.BinaryWrite((byte[])GlobalFill.GetQueryValue(StrSql));
                }
                else
                {
                    string path = Server.MapPath("~/Images/images.png");
                    byte[] byteArray = File.ReadAllBytes(path);
                    Response.BinaryWrite(byteArray);
                }
        }
    }
}
