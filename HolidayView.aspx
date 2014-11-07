<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="HolidayView.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--venkat--%>
       <%-- <div style="background-color: #FFFFFF">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            &nbsp;&nbsp;&nbsp;
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:samayConnectionString %>"
                SelectCommand="SELECT HolidayName,HolidayDate FROM TK_Holiday WHERE (CompanyID = @CompanyID)">
                <SelectParameters>
                    <asp:FormParameter DefaultValue="1" FormField="CompanyID" Name="CompanyID" />
                </SelectParameters>
            </asp:SqlDataSource>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
            <%-- <asp:Button ID="Button12" runat="server" Height="20px" 
          onclick="Button12_Click1" Text="Show" Width="99px" />--%>
         <%-- venkat--%>
        <%--    <br />
        </div>--%>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
    
        <div style="background-color: #FFFFFF; height: 655px;">
            <br />
            <table>
            <tr>
            <td align="center" style="padding-left:112px; font-family:Calibri"><h3><b>Company Holiday 
                List</b></h3></td>
            <td align="center" style="padding-left:90px;font-family:Calibri"><h3><b>Employee Holidays</b></h3></td></tr>
            
                <tr>
                    <td style="padding-left:138px;padding-top:20px">
                        <asp:GridView ID="GridView1" runat="server" BorderStyle="Solid" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                            BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                            <RowStyle Font-Names="calibri" Font-Size="12pt" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                ForeColor="White" Height="20px" />
                            <Columns>
                            
                            <asp:BoundField HeaderText="HolidayName" DataField="HolidayName"/>
                            <asp:BoundField HeaderText="HolidayDate" DataField="HolidayDate" DataFormatString="{0:MM/dd/yyyy}" />
                            </Columns>
                            
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            
                        </asp:GridView>
                    </td>
                    <td style="padding-left:89px;padding-top:20px">
                        <asp:GridView ID="GridView2" runat="server" BorderStyle="Solid" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                            BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" >
                            <RowStyle Font-Names="calibri" Font-Size="12pt" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                ForeColor="White" Height="20px" />
                            <Columns>
                            <asp:BoundField HeaderText="AttendanceDate " DataField="AttendanceDate" DataFormatString="{0:MM/dd/yyyy}" />
                             <asp:BoundField HeaderText="EmployeeID" DataField="EmployeeID" Visible="False"/>
                             
                            </Columns>
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
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
        
        </asp:Panel>
    </asp:Content>
