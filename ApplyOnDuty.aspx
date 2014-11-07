<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="ApplyOnDuty.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
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
            width: 145px;
            height: 17px;
        }
        .style26
        {
            width: 379px;
        }
        .style27
        {
            width: 382px;
            height: 34px;
        }
        .style28
        {
            width: 145px;
            text-align: right;
            font-size:large;
        }
        .style29
        {
            width: 145px;
            text-align: right;
            height: 31px;
            font-size:large;
        }
        .style30
        {
            width: 379px;
            height: 31px;
        }
        .style33
        {
            width: 382px;
            height: 31px;
        }
        .style34
        {
            width: 145px;
            text-align: right;
            height: 34px;
            font-size:large;
        }
        .style35
        {
            width: 379px;
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server">
    
    <div style="background-color: #FFFFFF;font-family:Calibri; height:760px">
        <div style="text-align: center; font-family: Calibri;">
        <h1  >
            <b>
                &nbsp;<asp:Image ID="Image1" runat="server" ImageAlign="Left" ImageUrl="~/Images/trfnote32.png"
                    Height="28px" Width="31px" />
                    
                    On Duty Application</b></h1>
        </div>
        <table style="width: 100%;">
            <tr>
                <td class="style22" align="right">
                </td>
                <td class="style21">
                </td>
            </tr>
            <tr>
                <td class="style29">
                    Applied On :&nbsp;                 </td>
                <td class="style30">
                    <asp:Label ID="lblAppliedOn" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29">
                    Employee :&nbsp;
                </td>
                <td class="style30"><b>
                    <asp:Label ID="lblEmployee" runat="server" Text="Label"></asp:Label></b>
                    <asp:Label ID="lblEmployeeID" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td class="style33">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style29">
                    &nbsp;FromTime&nbsp;:
                </td>
                <td class="style30">
                    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                    <cc1:TimeSelector ID="TimeSelector1" runat="server">
                    </cc1:TimeSelector>
                </td>
                <td class="style33">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style34">
                    To Time :&nbsp;
                </td>
                <td class="style35">
                    <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                    <cc1:TimeSelector ID="TimeSelector2" runat="server">
                    </cc1:TimeSelector>
                </td>
                <td class="style27">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style28">
                    Reason :&nbsp;                     
                    </b>
                </td>
                <td class="style26">
                    <asp:TextBox ID="txtReason" runat="server" Width="436px" Height="29px" TextMode="MultiLine"></asp:TextBox>
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
        <div style="background-color: #FFFFFF;text-align:center; padding-right:155px">
            <asp:Button ID="btnTrans" runat="server" BackColor="#404040" BorderStyle="None" BorderWidth="2px"
                ForeColor="White" Height="21px" Text="Mode" Width="78px" BorderColor="Black"
                OnClick="Button7_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b style="text-align: center">
                <asp:Button ID="Button8" runat="server" BackColor="#404040" 
                BorderStyle="None" BorderWidth="2px"
                    ForeColor="White" Height="21px" Text="Back" Width="78px" BorderColor="Black"
                    OnClick="Button8_Click" />
                <hr />
            </b>
            <br />
            <asp:Label ID="lblLevel" runat="server" Visible="False"></asp:Label>
            <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </div>
    </asp:Panel>
</asp:Content>
