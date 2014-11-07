<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="ApproveLeaves.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical">
    
    <div ><b>        &nbsp;<asp:Label ID="lblHodTitle" runat="server" 
        Text="Leaves (Approval Pending for HOD Level)"></asp:Label></b>

        &nbsp;<hr />
        <asp:GridView ID="gvHod" runat="server" BorderStyle="Solid" 
            onrowcommand="GridView1_RowCommand" BackColor="White" 
            BorderColor="#999999" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
            GridLines="Vertical" onpageindexchanging="gvHod_PageIndexChanging" 
            AllowPaging="true" PageSize="5">
            <RowStyle Font-Names="calibri" Font-Size="12pt" />
            <Columns>

                <asp:TemplateField ShowHeader="False" HeaderText="Approve">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                          CommandArgument = '<%# Eval("LeaveTransID") %>'   CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                            CommandName="Approve"  CommandArgument = '<%# Eval("LeaveTransID") %>' 
                            onclick="LinkButton1_Click" Text="Approve"></asp:LinkButton>
                        &nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("LeaveTransID") %>' CommandName="Reject" 
                            Text="Reject"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="calibri" 
                Font-Size="10pt" ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <hr />
        <br />
        </div>
         <div><b>
             &nbsp;<asp:Label ID="lblRoTitle" runat="server" 
                 Text="Leaves (Approval Pending for Reporting Level 1)"></asp:Label></b>
        <hr />
        <asp:GridView ID="gvRo" runat="server" BorderStyle="Solid" onrowcommand="gvRo_RowCommand" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" onpageindexchanging="gvRo_PageIndexChanging" OnSelectedIndexChanged="gvRo_SelectedIndexChanged" >
            <RowStyle Font-Names="calibri" Font-Size="12pt" />
            <Columns>
                                <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   

                <asp:TemplateField ShowHeader="False" HeaderText="Approve">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                          CommandArgument = '<%# Eval("LeaveTransID") %>'   CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                            CommandName="Approve"  CommandArgument = '<%# Eval("LeaveTransID") %>' 
                            onclick="LinkButton1_Click" Text="Approve"></asp:LinkButton>
                        &nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("LeaveTransID") %>' CommandName="Reject" 
                            Text="Reject"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="calibri" 
                Font-Size="10pt" ForeColor="White" Height="20px" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
             <asp:Button ID="btnapprovehod" runat="server" Text="Approve" BackColor="#999999"
                OnClick="btnapprovehod_Click" />
            &nbsp;
            <asp:Button ID="btnrejecthod" runat="server" Text="Reject" BackColor="#999999" 
                OnClick="btnrejecthod_Click" Visible="False" />
            <br />
        <hr />
        <br />
        </div>
        <div><b>
             &nbsp;<asp:Label ID="Label1" runat="server" 
                 Text="Leaves (Approval Pending for Reporting Level 2)"></asp:Label></b>
        <hr />
        <asp:GridView ID="gvRo2" runat="server" BorderStyle="Solid" 
            onrowcommand="gvRo2_RowCommand" BackColor="White" BorderColor="#999999" 
                 BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
            <RowStyle Font-Names="calibri" Font-Size="12pt" />
            <Columns>
                <asp:TemplateField ShowHeader="False" HeaderText="Approve">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                          CommandArgument = '<%# Eval("LeaveTransID") %>'   CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                            CommandName="Approve"  CommandArgument = '<%# Eval("LeaveTransID") %>' 
                            onclick="LinkButton1_Click" Text="Approve"></asp:LinkButton>
                            
                        &nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("LeaveTransID") %>' CommandName="Reject" 
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
        </asp:Panel>
</asp:Content>

