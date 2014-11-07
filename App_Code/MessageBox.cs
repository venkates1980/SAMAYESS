using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;

public class MessageBox
{
    private string strLine;
    private StringBuilder msgbox;
    private StreamReader readtemplte;

    private string msgbox_title = "";
    private string msgbox_icon = "";
    private string msgbox_message = "";
    private string msgbox_ok_button = "";
    private string msgbox_buttons = "";
    private ArrayList msgbox_button;

    public MessageBox(string tpl_path)
	{
//        readtemplte = new StreamReader( tpl_path);
        msgbox = new StringBuilder();
        msgbox_button = new ArrayList();

        //while ((strLine = readtemplte.ReadLine()) != null)
        //{
        //    msgbox.Append(strLine);
        //}

        msgbox.Append (" <div style=`position:fixed;height:100%;width:100%;top:0px;left:0px;background-color:#000000;filter:alpha(opacity=55);-moz-opacity:.55;opacity:.55;z-index:50;` id=`pagedimmer`>&nbsp;</div> ");

        msgbox.Append("     <div style=`position:fixed;background-color:#888888; border:1px solid #999999; z-index:50; left:20%; right:20%; top:20%;` id=`msgbox`>");
	 msgbox.Append ("<div style=`margin:5px;`>");
		msgbox.Append ("<table width=`100%` style=`background-color:#FFFFFF; border:1px solid #999999;`>");
			msgbox.Append ("<tr>");
            msgbox.Append("<td colspan=`2` style=`font-family:calibri; font-size:11px; font-weight:bold; padding-left:5px; background-image: url(msg_title_1.jpg); color:#FFFFFF; height:22px;`>[TITLE]</td>");
			msgbox.Append ("</tr>");
			msgbox.Append ("<tr>");
				msgbox.Append ("<td style=`width:100px; text-align:center;`>[ICON]</td>");
                msgbox.Append("<td style=`font-family:calibri; font-size:11px;padding-left:5px;`>[MESSAGE]</td>");
			msgbox.Append ("</tr>");
			msgbox.Append ("<tr>");
				msgbox.Append ("<td colspan=`2` style=`border-top:1px solid #CCCCCC; padding-top:5px;text-align:right;`>[BUTTONS]</td>");
			msgbox.Append ("</tr>");
		msgbox.Append ("</table>");
	 msgbox.Append ("</div>");
     msgbox.Append ("</div>");

     msgbox = msgbox.Replace('`', '"');

	}

    public void SetTitle(string msg_title)
    {
        this.msgbox_title = msg_title;
    }

    public void SetIcon(string msg_icon)
    {
        this.msgbox_icon = "<img src=\"" + msg_icon + "\">";
    }


    public void SetMessage(string msg_message)
    {
        this.msgbox_message = msg_message;
    }

    public void SetOKButton(string msg_button_class)
    {
        this.msgbox_ok_button = "<input type=\"button\" value=\"OK\" class=\"" + msg_button_class + "\" onClick=\"document.getElementById('pagedimmer').style.visibility = 'hidden'; document.getElementById('msgbox').style.visibility = 'hidden';\">";
    }


    public void AddButton(string msg_button)
    {
        msgbox_button.Add(msg_button);
    }


    public string ReturnObject()
    {
        int i=0;

        while (i < msgbox_button.Count)
        {
            msgbox_buttons += msgbox_button[i] + "&nbsp;";
            i++;
        }

        msgbox.Replace("[TITLE]", this.msgbox_title);
        msgbox.Replace("[ICON]", this.msgbox_icon);
        msgbox.Replace("[MESSAGE]", this.msgbox_message);
        msgbox.Replace("[BUTTONS]", msgbox_buttons + this.msgbox_ok_button);
        return msgbox.ToString();
    }
}
