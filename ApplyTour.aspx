<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="ApplyTour.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<script runat="server">


</script>

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
            width: 173px;
            height: 17px;
            text-align: right;
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
            width: 173px;
            text-align: right;
            font-size:large;
            font-family:Calibri;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div  style="background-color: #FFFFFF">
        <div style="text-align: center; font-family: Calibri;">
            <h1>
                <b>
                    &nbsp;&nbsp;<asp:Image ID="Image1" runat="server" ImageAlign="left" ImageUrl="~/Images/trfnote32.png"
                        Height="28px" Width="31px" />
                    Tour Application
                    <br />
                    <hr />
                </b>
        </div>
        <table style="width: 100%;">
            <tr>
                <td class="style22">
                </td>
                <td class="style21">
                </td>
            </tr>
            <tr>
                <td class="style28">
                    Applied On :&nbsp;
                </td>
                <td class="style26">
                    <asp:Label ID="lblAppliedOn" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style28">
                    Employee :&nbsp;
                </td>
                <td class="style26"><b>
                    <asp:Label ID="lblEmployee" runat="server" Text="Label"></asp:Label></b>
                    <asp:Label ID="lblEmployeeID" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td class="style27">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style28">
                    From :&nbsp;
                </td>
                <td class="style26">
                    <input runat="server" name="txtAttendanceDate" onfocus="showCalendarControl(this);"
                        type="text" id="txtFromDate" style="width: 121px" />
                </td>
                <td class="style27">
                    &nbsp;
                    <asp:DropDownList ID="ddlfrom" runat="server" Width="137px" Visible="False">
                        <asp:ListItem Value="1">Half Day</asp:ListItem>
                        <asp:ListItem Value="2">Full Day</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style28">
                    To :&nbsp;                 </td>
                <td class="style26">
                    <input runat="server" name="txtAttendanceDate0" onfocus="showCalendarControl(this);"
                        type="text" id="txtToDate" style="width: 120px" />
                </td>
                <td class="style27">
                    &nbsp;
                    <asp:DropDownList ID="ddlLeavetype" runat="server" Height="18px" Width="149px" Visible="False">
                    </asp:DropDownList>
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
                <td class="style27">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style28">
                    Reason :&nbsp;                </td>
                <td class="style26">
                    <asp:TextBox ID="txtReason" runat="server" Width="436px" Height="29px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style28">
                    &nbsp;
                </td>
                <td class="style26">
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
            </tr>
        </table>
        <div style="background-color: #FFFFFF">
            <asp:Button ID="btnTrans" runat="server" BackColor="#404040" BorderStyle="None" BorderWidth="2px"
                ForeColor="White" Height="21px" Text="Mode" Width="78px" BorderColor="Black"
                OnClick="Button7_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b style="text-align: center">
                <asp:Button ID="Button8" runat="server" BackColor="#404040" 
                BorderStyle="None" BorderWidth="2px"
                    ForeColor="White" Height="21px" Text="Back" Width="78px" BorderColor="Black"
                    OnClick="Button8_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
