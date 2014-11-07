<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="ApproveTour.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div >
        <b>&nbsp;<asp:Label ID="lblHodTitle" runat="server" 
        Text="Tour (Approval Pending for HOD Level)"></asp:Label></b>
        &nbsp;<hr />
        <asp:GridView ID="gvHod" runat="server" BorderStyle="Solid" Width="100%" 
            onrowcommand="GridView1_RowCommand" 
            onselectedindexchanged="gvHod_SelectedIndexChanged" BackColor="White" 
            BorderColor="#999999" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
            GridLines="Vertical" >
            <RowStyle Font-Names="calbri" Font-Size="12pt" />
            <Columns>
                <asp:TemplateField ShowHeader="False" HeaderText="Approve">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                          CommandArgument = '<%# Eval("LEAVETRANSID") %>'   CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                            CommandName="Approve"  CommandArgument = '<%# Eval("LEAVETRANSID") %>' 
                            onclick="LinkButton1_Click" Text="Approve"></asp:LinkButton>
                        &nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("LEAVETRANSID") %>' CommandName="Reject" 
                            Text="Reject"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" 
                Font-Size="10pt" ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <hr />
        <br />
        </div>
         <div><b>
             &nbsp;<asp:Label ID="lblRoTitle" runat="server" 
                 Text="Tour (Approval Pending for Reporting Level 1)"></asp:Label></b>
        <hr />
        <asp:GridView ID="gvRo" runat="server" BorderStyle="Solid" Width="100%" 
            onrowcommand="gvRo_RowCommand" BackColor="White" BorderColor="#999999" 
                 BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" >
            <RowStyle Font-Names="calbri" Font-Size="12pt" />
            <Columns>
                <asp:TemplateField ShowHeader="False" HeaderText="Approve">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                          CommandArgument = '<%# Eval("U") %Id'   CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                            CommandName="Approve"  CommandArgument = '<%# Eval("Id") %>' 
                            onclick="LinkButton1_Click" Text="Approve"></asp:LinkButton>
                        &nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("Id") %>' CommandName="Reject" 
                            Text="Reject"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" 
                Font-Size="10pt" ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <hr />
        <br />
        </div>
        <div><b>
             &nbsp;<asp:Label ID="Label1" runat="server" 
                 Text="Tour (Approval Pending for Reporting Level 2)"></asp:Label></b>
        <hr />
        <asp:GridView ID="gvRo2" runat="server" BorderStyle="Solid" Width="100%" 
            onrowcommand="gvRo2_RowCommand" BackColor="White" BorderColor="#999999" 
                 BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" >
            <RowStyle Font-Names="calbri" Font-Size="12pt" />
            <Columns>
                <asp:TemplateField ShowHeader="False" HeaderText="Approve">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                          CommandArgument = '<%# Eval("Id") %>'   CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                            CommandName="Approve"  CommandArgument = '<%# Eval("Id") %>' 
                            onclick="LinkButton1_Click" Text="Approve"></asp:LinkButton>
                            
                        &nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("Id") %>' CommandName="Reject" 
                            Text="Reject"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" 
                Font-Size="10pt" ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <hr />
        <br />
        </div>
</asp:Content>

