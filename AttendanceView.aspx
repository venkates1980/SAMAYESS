<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="AttendanceView.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style21
        {
            width: 100%;
            padding-left:115px;
        }
        .style22
        {
            height: 31px;
            font-family:Calibri;
            font-size:large;
        }
        .style23
        {
            height: 33px;
             font-family:Calibri;
            font-size:large;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
    
    <div style="background-color: #FFFFFF; height:760px; text-align:center;font-family:Calibri"><br />
    <h1>View Attendance</h1> <br />
        <table class="style21">
            <tr>
                <td style="text-align: right" class="style22">
                    <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>
                    &nbsp;:</td>
                <td align="left" class="style22">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="103px">
                        <asp:ListItem Value="1">Jan</asp:ListItem>
                        <asp:ListItem Value="2">Feb</asp:ListItem>
                        <asp:ListItem Value="3">Mar</asp:ListItem>
                        <asp:ListItem Value="4">Apr</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">Jun</asp:ListItem>
                        <asp:ListItem Value="7">Jul</asp:ListItem>
                        <asp:ListItem Value="8">Aug</asp:ListItem>
                        <asp:ListItem Value="9">Sep</asp:ListItem>
                        <asp:ListItem Value="10">Oct</asp:ListItem>
                        <asp:ListItem Value="11">Nov</asp:ListItem>
                        <asp:ListItem Value="12">Dec</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="style23">
                    Year&nbsp;&nbsp; &nbsp;&nbsp;:</td>
                <td align="left" class="style23">
                    <asp:DropDownList ID="dtlstYear" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="CalendarYear" DataValueField="CalendarYearID" Height="20px" 
                        Width="103px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2"; style="padding-right:195px">
                    <asp:ImageButton ID="Button12" runat="server" ImageUrl="~/Images/show.png" 
                        onclick="Button12_Click2" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:samayConnectionString %>" 
                        SelectCommand="SELECT [CalendarYearID], [CalendarYear] FROM [TK_CalendarYear] order by [CalendarYear] desc">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>

    <div style="background-color: #FFFFFF;"> 
        <br />
        
        <asp:GridView ID="GridView1" runat="server" BorderStyle="Solid" Width="100%" 
            onrowdatabound="GridView1_RowDataBound1" BackColor="White" 
            BorderColor="#999999" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
            GridLines="Vertical" >
            <RowStyle Font-Names="calibri" Font-Size="12pt" />
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" 
                Font-Size="10pt" ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
       
</div>
</div>
</asp:Panel>
</asp:Content>

