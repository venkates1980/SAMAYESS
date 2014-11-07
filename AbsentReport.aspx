<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="AbsentReport.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style21
        {
            text-align: right;
            font-family: Calibri;
            font-size:medium;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
        <div style="height: 760px">
            <div style="text-align: center; font-family: Calibri;">
                <h1>
                    <b>Absent Report </b>
                </h1>
            </div>
            <div style="background-color: #FFFFFF">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<table style="width: 100%;">
                    <tr>
                        <td class="style21">
                            Attendance Date :&nbsp;
                        </td>
                        <td>
                            <input runat="server" name="txtAttendanceDate" onfocus="showCalendarControl(this);"
                                type="text" id="txtAttendanceDate" style="width: 120px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style21">
                            Department :&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDepartment" runat="server" Height="20px" Width="227px">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="buttonpadding">
                            <asp:ImageButton ID="btnTrans" runat="server" ImageUrl="~/Images/show.png" 
                                onclick="btnTrans_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" BorderStyle="Solid" Width="100%" Height="70px"
                    BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" ForeColor="Black"
                    GridLines="Vertical" onpageindexchanging="GridView1_PageIndexChanging" AllowPaging="true" PageSize="10">
                    <RowStyle Font-Names="calibri" Font-Size="12pt" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#404040" Font-Bold="False" Font-Names="calibri" Font-Size="10pt"
                        ForeColor="White" Height="20px" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
