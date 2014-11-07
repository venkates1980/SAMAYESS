<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="LeaveApplication.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style21
        {
            color: #000066;
            font-weight: bold;
            font-size: x-large;
            height: 17px;
            width: 379px;
        }
        .style22
        {
            height: 17px;
        }
        .style23
        {
            height: 17px;
            width: 382px;
        }
        .style26
        {
            width: 379px;
        }
        .style27
        {
            width: 382px;
        }
        .style28
        {
            width: 116px;
            font-family: Calibri;
            font-size: large;
        text-align: right;
    }
        .style29
        {
            width: 116px;
            height: 23px;
            font-family:Calibri;
            font-size:large;
        text-align: right;
    }
        .style30
        {
            width: 379px;
            height: 23px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div  style="background-color: #FFFFFF;font-family:Calibri">
        <div style="text-align: center; font-family: Calibri;">
        <h1>
            <b>
                &nbsp;&nbsp;<asp:Image ID="Image1" runat="server" ImageAlign="left" ImageUrl="~/Images/trfnote32.png"
                    Height="28px" Width="31px" />
                Leave Application
               <hr />
            </b>
        </div>
        <table style="width: 100%;">
            <tr>
                <td class="style22" align="right" colspan="3">
                    <asp:GridView ID="gvledger" runat="server" BorderStyle="Solid" Width="100%" 
                        BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" 
                        ForeColor="Black" GridLines="Vertical">
                        <RowStyle Font-Names="calibri" Font-Size="12pt" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="White" Height="20px" />
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style28">
                    &nbsp;</td>
                <td class="style26">
                    &nbsp;</td>
                <td class="style27" rowspan="9" valign=top>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style28">
                    &nbsp;</td>
                <td class="style26">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style28">
                    Applied On:
                </td>
                <td class="style26">
                    <asp:Label ID="lblAppliedOn" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29">
                    Employee&nbsp; :</td>
                <td class="style30"><b>
                    <asp:Label ID="lblEmployee" runat="server" Text="Label"></asp:Label></b>
                    <asp:Label ID="lblEmployeeID" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td class="style28">
                    From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                <td class="style26">
                    <input runat="server" name="txtAttendanceDate" onfocus="showCalendarControl(this);"
                        type="text" id="txtFromDate" style="width: 121px" />
                </td>
                </tr>
            <tr>
                <td class="style28">
                    &nbsp;&nbsp;
                </td>
                <td class="style26">
                    <asp:DropDownList ID="ddlfrom" runat="server" Width="137px">
                        <asp:ListItem Value="1">Half Day</asp:ListItem>
                        <asp:ListItem Value="2">Full Day</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style28">
                    To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                <td class="style26">
                    <input runat="server" name="txtAttendanceDate0" onfocus="showCalendarControl(this);"
                        type="text" id="txtToDate" style="width: 120px" />
                </td>
                
            </tr>
            <tr>
                <td class="style28">
                    &nbsp;
                </td>
                <td class="style26">
                    <asp:DropDownList ID="ddlto" runat="server" Width="137px">
                        <asp:ListItem Value="1">Half Day</asp:ListItem>
                        <asp:ListItem Value="2">Full Day</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style28">
                    Leave Type:
                </td>
                <td class="style26">
                    <asp:DropDownList ID="ddlLeavetype" runat="server" Height="18px" Width="149px">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style28">
                    Reason&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                <td class="style26">
                    <asp:TextBox ID="txtReason" runat="server" Width="206px" Height="35px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style28">
                    &nbsp;
                </td>
                <td class="style26">
                    &nbsp;
                </td>
                <td class="style27">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style28">
                    &nbsp;
                </td>
                <td class="style26">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td class="style27">
                    &nbsp;
                </td>
            </tr>
        </table>
        <div style="background-color: #FFFFFF">
            <asp:Button ID="btnTrans" runat="server" BackColor="#404040" BorderStyle="None" BorderWidth="2px"
                ForeColor="White" Height="21px" Text="Mode" Width="78px" BorderColor="Black"
                OnClick="Button7_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>
                <asp:Button ID="Button8" runat="server" BackColor="#404040" BorderStyle="None" BorderWidth="2px"
                    ForeColor="White" Height="21px" Text="Back" Width="78px" BorderColor="Black"
                    OnClick="Button8_Click" />
                <hr />
            </b>
            <br />
            <asp:Label ID="lblLevel" runat="server" Visible="False"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
