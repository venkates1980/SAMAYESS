using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Net.Mail;
using System.Net;



public static class GlobalFunctions
{

    public static SqlConnection GetConnection()
    {
        try
        {
            SqlConnection sqlconn = new SqlConnection();
            sqlconn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["samayConnectionString"].ConnectionString;
            if (sqlconn.State == ConnectionState.Closed) { sqlconn.Open(); }
            return sqlconn;
        }
        catch (Exception E)
        {
            throw E;
        }
    }

    public static void FillCombo(string parStr, DropDownList pard, SqlConnection sqlconn)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter SDA = new SqlDataAdapter(parStr, sqlconn);
        SDA.Fill(dt);
        pard.DataSource = dt;
        pard.DataValueField = dt.Columns[0].ColumnName;
        pard.DataTextField = dt.Columns[1].ColumnName;
        pard.DataBind();

    }
    public static DateTime NullRefDate = Convert.ToDateTime("01/01/1900");
    public static void FillCombo(string parStr, DropDownList pard, SqlConnection sqlconn, string parDispText)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter SDA = new SqlDataAdapter(parStr, sqlconn);
        SDA.Fill(dt);
        pard.DataSource = dt;
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = parDispText;
        dt.Rows.InsertAt(dr, 0);
        pard.DataValueField = dt.Columns[0].ColumnName;
        pard.DataTextField = dt.Columns[1].ColumnName;
        pard.DataBind();
    }


    public static double GetColTotal(DataTable pardt, string parColName)
    {
        double dblRtrn = 0;
        foreach (DataRow dr in pardt.Rows)
        {
            dblRtrn += Convert.ToDouble(dr[parColName]);
        }
        return dblRtrn;
    }


    public static object GetQueryValue(string StrSql, SqlConnection pSqlConn)
    {

        SqlConnection.ClearAllPools();
        object ObjRtrn;
        if (pSqlConn.State == ConnectionState.Closed) { pSqlConn.Open(); }
        SqlCommand SqlCmd = new SqlCommand(StrSql, pSqlConn);

        ObjRtrn = SqlCmd.ExecuteScalar();
        SqlCmd.Dispose();
        pSqlConn.Close();
        return ObjRtrn;
    }


    public static DataTable GetQueryResult(string pSql, SqlConnection pSqlConn)
    {
        SqlDataAdapter SDA = new SqlDataAdapter(pSql, pSqlConn);
        DataTable dt = new DataTable();
        SDA.Fill(dt);
        return dt;
    }

    public static byte[] GetByte(HtmlInputFile fInput)
    {
        if (fInput.PostedFile != null)
        {
            //To create a PostedFile
            HttpPostedFile File = fInput.PostedFile;

            //Create byte Array with file len
            byte[] Data = new Byte[File.ContentLength];

            //force the control to load data in array

            File.InputStream.Read(Data, 0, File.ContentLength);

            return Data;
        }
        return null;
    }

    public static void FillMonthsInCombo(DropDownList ddL)
    {
        ddL.Items.Clear();


        ListItem lst = new ListItem();
        lst.Text = "_____Select Month_____";
        lst.Value = "0";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "January";
        lst.Value = "1";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "February";
        lst.Value = "2";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "March";
        lst.Value = "3";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "April";
        lst.Value = "4";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "May";
        lst.Value = "5";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "June";
        lst.Value = "6";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "July";
        lst.Value = "7";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "August";
        lst.Value = "8";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "September";
        lst.Value = "9";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "October";
        lst.Value = "10";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "November";
        lst.Value = "11";
        ddL.Items.Add(lst);

        lst = new ListItem();
        lst.Text = "December";
        lst.Value = "12";
        ddL.Items.Add(lst);


    }

    public static void FillYearsInCombo(DropDownList ddYear)
    {
        ddYear.Items.Clear();
        ListItem lst = new ListItem();

        for (int i = 2010; i < 2050; i++)
        {
            lst = new ListItem();
            lst.Text = i.ToString();
            lst.Value = i.ToString();
            ddYear.Items.Add(lst);
        }
    }

    public static string SendEmail(string pToAddress, string pSubject, Int64 CompanyID, string LeaveTypeName, string empno,string Reason,string typeid, string MailBody, string Employename, string Department, string Designation, string fromdate, string todate)
    {
        try
        {
            Company ObjCompany = new Company(CompanyID);
            string COMPID = "";
            string empname = "";
            string dept = "";
            string desg = "";
            string fromdt = "";
            string todt = "";
            string lt = "";
            
            string desc = "";
            string mFrom = "";
            string mTo = "";
            string mSubject = "";
            string mBody = "";
            string mSMTPServer = "";
            Int32 mSMTPPort = 587;            //for gmail - 587, yahoo - 25
            string mUserName = "";
            string mPassword = "";
            bool mSMTPSSL = ObjCompany.EnableSSL;

            COMPID = CompanyID.ToString();
            empname = Employename;
            dept = Department;
            desg = Designation;
            fromdate = fromdate;
            todate = todate;
            lt = LeaveTypeName;
            desc = Reason;
            mTo = pToAddress;
            mFrom = ObjCompany.SourceEmail;
            mSubject = pSubject;
            mBody = MailBody;
            mSMTPServer = ObjCompany.SMTPServer;
            mUserName = ObjCompany.EmailUserName;
            mPassword = ObjCompany.EmailPwd;
            mSMTPPort = ObjCompany.SMTPPort;



            MailMessage mEmail = new MailMessage();
            MailAddress MailFrom = new MailAddress(mFrom);
            mEmail.From = MailFrom;
            mEmail.To.Add(mTo);
            mEmail.Subject = mSubject;
            //mEmail.SubjectEncoding = System.Text.Encoding.UTF8;


            ////Attachment inline = new Attachment("C:\\smenu.swf");
            ////mEmail.Attachments.Add(inline);
            //mBody = "<div style=\"font-family:Arial\">This is an INLINE attachment:<br /><br /><img src=\"cid:pic1\" alt=\"\"><br /><br />Thanks for downloading this example.</div>";
            //mBody = "<html><body>" +
            //  "<br><img src=\"cid:15\"><br>" +
            //  "Dinesh What a beautiful fellow ....<br><img src=\"cid:17\"><br>" +
            // "</body></html>";
            //mBody = pESetting.BodyHtml;http://localhost:49446/SamayEss/LeaveARForm.aspx?
            string strAccept = "";
            string strReject = "";
            //string strbutton="button";
            string color = "background-color:#404040;color:white";
            string color1 = "background-color:#404040;color:white";

            // mEmail.Body = "<button type='" + strbutton + "'style='" + color + "'>Accept</button> or <button type='" + strbutton + "'style='" + color1 + "'></button>";
            //mEmail.Body = "<table><tr><td><a href='" + strAccept + "'style='" + color + "'>Accept</a></td> or <td><a href='" + strReject + "'style='" + color1 + "'>Reject</a></td></tr> ";
            //mEmail.BodyEncoding = System.Text.Encoding.UTF8;
            mEmail.Body = " <table border='0' cellpadding='5' cellspacing='5'>" +
                        "<tr>" +
                                    "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Employee Name</td>" +
                                    "<td>&nbsp;:&nbsp;</td>" +
                                    "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + empname + "</td>" +
                        "</tr>" +
                        
                        "<tr>" +
                            "<td style='width:130px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>EmployeeID</td>" +
                             "<td style='width:10px'>&nbsp;:&nbsp;</td>" +
                             "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + empno + "</td>" +
                        "</tr>" +
                        
                        "<tr>" +
                            "<td style='width:130px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Department</td>" +
                             "<td style='width:10px'>&nbsp;:&nbsp;</td>" +
                             "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + dept + "</td>" +
                        "</tr>" +
                        
                        "<tr>" +
                            "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Leave Type</td>" +
                            "<td>&nbsp;:&nbsp;</td>" +
                            "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + lt + "</td>" +
                        "</tr>" +
                        
                        "<tr>" +
                            "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Leave Dates</td>" +
                            "<td>&nbsp;:&nbsp;</td>" +
                            "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'><b>From</b>: " + fromdate + "  <b>To</b>: " + todate + "</td>" +
                        "</tr>" +

                        "<tr>" +
                            "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Reason for leave</td>" +
                            "<td>&nbsp;:&nbsp;</td>" +
                            "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'> " + desc + " </td>" +
                        "</tr>" +
                ////"<tr>" +
                ////    "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>No. of Dates  Applied.</td>" +
                ////    "<td>&nbsp;:&nbsp;</td>"+
                ////    "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>"+ lblapplied1.Text +"</td>" +
                ////"</tr>"+

                ////"<tr>" +
                ////    "<td>" +
                ////        "<a href='http://localhost:53731/SamayEss/LeaveARForm.aspx?emp=" + empno + "&&frmdate=" + fromdate + "&&todate=" + todate + "&&status=Approve&&ccode=" + COMPID + "&&Leavetype=" + lt + "&&desc="+Reason+"' style='" + color + "'>Accept</a>" +
                ////    "</td>" +
                ////    "<td>" +
                ////        "<a href='http://localhost:53731/SamayEss/LeaveARForm.aspx?emp=" + empno + "&&frmdate=" + fromdate + "&&todate=" + todate + "&&status=Reject&&ccode=" + COMPID + "&&Leavetype=" + lt + "&&desc=" + Reason + "' style='" + color1 + "'>Reject</a>" +
                ////    "</td>" +

                ////"</tr>" +

                //////"<tr>"+
                //////    "<td>"+
                //////        "Accept" +
                //////    "</td>"+
                //////    "<td>" +
                //////        "Reject" +
                //////    "</td>" +

                //////"</tr>"+
                    "</table>";
            mEmail.IsBodyHtml = true;

            // Smtp Client
            SmtpClient SmtpMail = new SmtpClient(mSMTPServer, mSMTPPort);
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Credentials = new NetworkCredential(mUserName, mPassword);
            SmtpMail.EnableSsl = mSMTPSSL;
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;


            SmtpMail.Send(mEmail);
            return "Successfully Sent Mail";
        }
        catch (Exception ex)
        {
            return "Unable to Send Mail " + ex.Message;

        }


    }
    public static string SendEmailfromHOD(string pToAddress, string pSubject, Int64 CompanyID, string LeaveTypeName, string empno, string Reason, string typeid, string MailBody, string Employename, string Department, string Designation, string fromdate, string todate)
    {
        try
        {
            Company ObjCompany = new Company(CompanyID);
            string COMPID = "";
            string empname = "";
            string dept = "";
            string desg = "";
            string fromdt = "";
            string todt = "";
            string lt = "";

            string desc = "";
            string mFrom = "";
            string mTo = "";
            string mSubject = "";
            string mBody = "";
            string mSMTPServer = "";
            Int32 mSMTPPort = 587;            //for gmail - 587, yahoo - 25
            string mUserName = "";
            string mPassword = "";
            bool mSMTPSSL = ObjCompany.EnableSSL;

            COMPID = CompanyID.ToString();
            empname = Employename;
            dept = Department;
            desg = Designation;
            fromdate = fromdate;
            todate = todate;
            lt = LeaveTypeName;
            desc = Reason;
            mTo = pToAddress;
            mFrom = ObjCompany.SourceEmail;
            mSubject = pSubject;
            mBody = MailBody;
            mSMTPServer = ObjCompany.SMTPServer;
            mUserName = ObjCompany.EmailUserName;
            mPassword = ObjCompany.EmailPwd;
            mSMTPPort = ObjCompany.SMTPPort;



            MailMessage mEmail = new MailMessage();
            MailAddress MailFrom = new MailAddress(mFrom);
            mEmail.From = MailFrom;
            mEmail.To.Add(mTo);
            mEmail.Subject = mSubject;
            //mEmail.SubjectEncoding = System.Text.Encoding.UTF8;


            ////Attachment inline = new Attachment("C:\\smenu.swf");
            ////mEmail.Attachments.Add(inline);
            //mBody = "<div style=\"font-family:Arial\">This is an INLINE attachment:<br /><br /><img src=\"cid:pic1\" alt=\"\"><br /><br />Thanks for downloading this example.</div>";
            //mBody = "<html><body>" +
            //  "<br><img src=\"cid:15\"><br>" +
            //  "Dinesh What a beautiful fellow ....<br><img src=\"cid:17\"><br>" +
            // "</body></html>";
            //mBody = pESetting.BodyHtml;http://localhost:49446/SamayEss/LeaveARForm.aspx?
            string strAccept = "";
            string strReject = "";
            //string strbutton="button";
            string color = "background-color:#404040;color:white";
            string color1 = "background-color:#404040;color:white";

            // mEmail.Body = "<button type='" + strbutton + "'style='" + color + "'>Accept</button> or <button type='" + strbutton + "'style='" + color1 + "'></button>";
            //mEmail.Body = "<table><tr><td><a href='" + strAccept + "'style='" + color + "'>Accept</a></td> or <td><a href='" + strReject + "'style='" + color1 + "'>Reject</a></td></tr> ";
            //mEmail.BodyEncoding = System.Text.Encoding.UTF8;
            mEmail.Body = " <table border='0' cellpadding='5' cellspacing='5'>" +
                        "<tr>" +
                                    "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Employee Name</td>" +
                                    "<td>&nbsp;:&nbsp;</td>" +
                                    "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + empname + "</td>" +
                        "</tr>" +

                        "<tr>" +
                            "<td style='width:130px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>EmployeeID</td>" +
                             "<td style='width:10px'>&nbsp;:&nbsp;</td>" +
                             "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + empno + "</td>" +
                        "</tr>" +

                        "<tr>" +
                            "<td style='width:130px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Department</td>" +
                             "<td style='width:10px'>&nbsp;:&nbsp;</td>" +
                             "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + dept + "</td>" +
                        "</tr>" +

                        "<tr>" +
                            "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Leave Type</td>" +
                            "<td>&nbsp;:&nbsp;</td>" +
                            "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'>" + lt + "</td>" +
                        "</tr>" +

                        "<tr>" +
                            "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Leave Dates</td>" +
                            "<td>&nbsp;:&nbsp;</td>" +
                            "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'><b>From</b>: " + fromdate + "  <b>To</b>: " + todate + "</td>" +
                        "</tr>" +

                        "<tr>" +
                            "<td style='width:120px;color:#00275E;font-size:12px;font-weight:bold;font-family:Rockwell, Bauhaus 93;height:15px;padding-left:5px;'>Reason for leave</td>" +
                            "<td>&nbsp;:&nbsp;</td>" +
                            "<td style='color:Maroon;font-size:14px;font-family:Calibri,Arial, Helvetica, Sans-Serif;font-weight:bold;font-style:oblique;'> " + desc + " </td>" +
                        "</tr>" +
                    "</table>";
            mEmail.IsBodyHtml = true;

            // Smtp Client
            SmtpClient SmtpMail = new SmtpClient(mSMTPServer, mSMTPPort);
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Credentials = new NetworkCredential(mUserName, mPassword);
            SmtpMail.EnableSsl = mSMTPSSL;
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;


            SmtpMail.Send(mEmail);
            return "Successfully Sent Mail";
        }
        catch (Exception ex)
        {
            return "Unable to Send Mail " + ex.Message;

        }


    }
}

