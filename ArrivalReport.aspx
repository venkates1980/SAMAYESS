<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="ArrivalReport.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style21
        {
            width: 172px;
            font-family: Calibri;
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div >
    <div style="text-align: center; font-family: Calibri;">
        <h1>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Arrival Report<hr />
        </b></div> 
    
    
    
    
        <div style="background-color: #FFFFFF"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<table 
                style="width:100%;">
            <tr>
                <td class="style21">
                    Attendance Date :</td>
                <td>
    <input runat ="server"   name="txtAttendanceDate"
       onfocus="showCalendarControl(this);"
       type="text" id="txtAttendanceDate" style="width: 227px"/></td>
            </tr>
            <tr>
                <td class="style21">
                    Department&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                <td>
      <asp:DropDownList ID="ddlDepartment" runat="server" Height="20px" 
          Width="227px">
    </asp:DropDownList>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style21">
                    <asp:Button ID="btnTrans" runat="server" BackColor="#404040" BorderStyle="None" 
                        BorderWidth="2px" ForeColor="White" Height="21px" Text="Show" 
                        Width="78px" BorderColor="Black" onclick="Button7_Click" />
                </td>
            </tr>
            </table>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
</div>
<div > 
        <asp:GridView ID="GridView1" runat="server" BorderStyle="Solid" 
        Width="100%" Height="70px" BackColor="White" BorderColor="#999999" 
            BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" 
            AllowPaging="true" PageSize="5" 
            onpageindexchanging="GridView1_PageIndexChanging" >
            <RowStyle Font-Names="calbri" Font-Size="12pt" />
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" 
                Font-Size="10pt" ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        </div>
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
    <br />
    <br />
    <br />
    <br />
    </div>
   
</asp:Content>

