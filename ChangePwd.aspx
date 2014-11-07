<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="ChangePwd.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style21
        {
            height: 22px;
        }
        .style22
        {
            height: 22px;
            width: 197px;
        }
        .style23
        {
            width: 197px;
            font-family: Calibri;
            font-size: large;
        }
        .style24
        {
            width: 197px;
            height: 56px;
            font-family: Calibri;
            font-size: large;
        }
        .style25
        {
            height: 56px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="msgboxpanel" runat="server">
    </div>
    <div style="text-align: center; font-family: Calibri;">
        <h1>
            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/key32.png" />
                &nbsp; Change Password
                <hr />
            </b>
    </div>
    <table style="width: 100%;">
        <tr>
            <td class="style22">
                &nbsp;
            </td>
            <td class="style21">
            </td>
        </tr>
        <tr>
            <td class="style23">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; New Password&nbsp;
                :</td>
            <td>
                <asp:TextBox ID="txtnewpwd" runat="server" Width="191px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style24">
                &nbsp;&nbsp; Confirm Password&nbsp;:
            </td>
            <td class="style25">
                <asp:TextBox ID="txtconfirmpwd" runat="server" Width="191px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style24">
                &nbsp;&nbsp;&nbsp;
            </td>
            <td class="style25">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <div>
        <hr />
    </div>
    <asp:Button ID="btnTrans" runat="server" BackColor="#404040" BorderStyle="None" BorderWidth="2px"
        ForeColor="White" Height="21px" Text="Apply" Width="78px" BorderColor="Black"
        OnClick="Button7_Click" />
    <hr />
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
</asp:Content>
