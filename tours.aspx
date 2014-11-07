<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="tours.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<%@ Register Src="WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal">
        
    <div>
        
        <table>
            <tr>
                <td style="font-family:Calibri;font-size:large">
                    <asp:Label ID="lbFromDate" runat="server" Text="From Date :"></asp:Label>
                </td>
                <td>
                    <input runat="server" name="txtFromDate" onfocus="showCalendarControl(this);"
                            type="text" id="txtFromDate" style="width: 120px" />
                </td>
               
                <td>
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td  style="font-family:Calibri;font-size:large">
                    <asp:Label ID="lblTodate" runat="server" Text="To Date :"></asp:Label>
                </td>
                <td>
                   <input runat="server" name="txtToDate" onfocus="showCalendarControl(this);"
                            type="text" id="txtToDate" style="width: 120px" />
                </td> 
                <td>
                    <asp:Button ID="btnGet" runat="server" BackColor="#404040" BorderColor="Black" 
                        BorderStyle="None" BorderWidth="2px" ForeColor="White" onclick="btnGet_Click" 
                        Text="Get" />
                </td> 
                <td>&nbsp;</td>     
                <td> <asp:Label ID="lblErrorMsg" Visible=false ForeColor="Red" runat="server" Text="From date cannot be lesser than To date"/></td>        
            </tr>
        </table>
    </div>
    <div style="background-color: #FFFFFF; height: 669px;">
        <br />
        <hr />
        <hr />
        <br />
        <b>Tour (Approval Pending)</b> 
        <hr />
        <asp:GridView ID="GridView1" runat="server" BorderStyle="Solid" Width="100%" 
            OnRowCommand="GridView1_RowCommand" BackColor="White" 
            BorderColor="#999999" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
            GridLines="Vertical" onpageindexchanging="GridView1_PageIndexChanging" 
            AllowPaging="true" PageSize="5" onrowdeleting="GridView1_RowDeleting">
            <RowStyle Font-Names="calibri" Font-Size="12pt" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandArgument='<%# Eval("LeaveTransID") %>'
                            CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("ID") %>' OnClick="LinkButton1_Click" Text="Edit"></asp:LinkButton>--%>
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"
                            CommandArgument='<%# Eval("ID") %>' CommandName="Delete" Text="Delete"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <hr />
        <asp:Button ID="Button7" runat="server" BackColor="#404040" BorderStyle="None" BorderWidth="2px"
            ForeColor="White" Height="21px" Text="Add New" Width="78px" BorderColor="Black"
            OnClick="Button7_Click" />
        <br />
        <hr />
        <br />
       <b>Tour (Approved/Rejected)<hr /></b> 
        <asp:GridView ID="gvApproved" runat="server" BorderStyle="Solid" Width="100%" 
            OnRowCommand="GridView1_RowCommand" BackColor="White" 
            BorderColor="#999999" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
            GridLines="Vertical" onpageindexchanging="gvApproved_PageIndexChanging" AllowPaging="true" PageSize="5">
            <RowStyle Font-Names="calbri" Font-Size="12pt" />
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <hr />
    </div>
    </asp:Panel>
</asp:Content>
