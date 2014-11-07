<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"
    CodeFile="Profile.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style17
        {
            width: 815px;
        }
        .style21
        {
            text-align: right;
        }
        .style22
        {
            width: 588px;
        }
        .style23
        {
            width: 250px;
        }
        .style24
        {
            text-align: right;
            height: 32px;
        }
        .style25
        {
            width: 388px;
            height: 32px;
        }
        .style26
        {
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="EMPLOYEEID" DataSourceID="SqlDataSource1"
        Width="800px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
        CellPadding="1" GridLines="Both" Height="509px" Font-Size="Large" Font-Bold="false"
        Font-Names="Calibri">
        <FooterStyle BackColor="White" ForeColor="Black" />
        <RowStyle ForeColor="Black" />
        <EditItemTemplate>
            EMPLOYEEID:
            <asp:Label ID="EMPLOYEEIDLabel1" runat="server" Text='<%# Eval("EMPLOYEEID") %>' />
            <br />
            EMPLOYEENAME:
            <asp:TextBox ID="EMPLOYEENAMETextBox" runat="server" Text='<%# Bind("EMPLOYEENAME") %>' />
            <br />
            EMPLOYEECODE:
            <asp:TextBox ID="EMPLOYEECODETextBox" runat="server" Text='<%# Bind("EMPLOYEECODE") %>' />
            <br />
            EMPLOYEERFID:
            <asp:TextBox ID="EMPLOYEERFIDTextBox" runat="server" Text='<%# Bind("EMPLOYEERFID") %>' />
            <br />
            CARDNUMBER:
            <asp:TextBox ID="CARDNUMBERTextBox" runat="server" Text='<%# Bind("CARDNUMBER") %>' />
            <br />
            DESIGNATION:
            <asp:TextBox ID="DESIGNATIONTextBox" runat="server" Text='<%# Bind("DESIGNATIONNAME") %>' />
            <br />
            <br />
            REPORTING AUTHORITY1:
            <asp:TextBox ID="REPORTINGAUTHORITY1TextBox" runat="server" Text='<%# Bind("REPORTINGID1") %>' />
            <br />
            REPORTING AUTHORITY2:
            <asp:TextBox ID="REPORTINGAUTHORITY2TextBox" runat="server" Text='<%# Bind("REPORTINGID2") %>' />
            <br />
            FATHERNAME:
            <asp:TextBox ID="FATHERNAMETextBox" runat="server" Text='<%# Bind("FATHERNAME") %>' />
            <br />
            MOTHERNAME:
            <asp:TextBox ID="MOTHERNAMETextBox" runat="server" Text='<%# Bind("MOTHERNAME") %>' />
            <br />
            RESIDENTIALADDRESS:
            <asp:TextBox ID="RESIDENTIALADDRESSTextBox" runat="server" Text='<%# Bind("RESIDENTIALADDRESS") %>' />
            <br />
            PERMANENTADDRESS:
            <asp:TextBox ID="PERMANENTADDRESSTextBox" runat="server" Text='<%# Bind("PERMANENTADDRESS") %>' />
            <br />
            CONTACTNO:
            <asp:TextBox ID="CONTACTNOTextBox" runat="server" Text='<%# Bind("CONTACTNO") %>' />
            <br />
            MOBILENO:
            <asp:TextBox ID="MOBILENOTextBox" runat="server" Text='<%# Bind("MOBILENO") %>' />
            <br />
            EMAILID:
            <asp:TextBox ID="EMAILIDTextBox" runat="server" Text='<%# Bind("EMAILID") %>' />
            <br />
            POB:
            <asp:TextBox ID="POBTextBox" runat="server" Text='<%# Bind("POB") %>' />
            <br />
            BLOODGROUP:
            <asp:TextBox ID="BLOODGROUPTextBox" runat="server" Text='<%# Bind("BLOODGROUP") %>' />
            <br />
            DEPARTMENTNAME:
            <asp:TextBox ID="DEPARTMENTNAMETextBox" runat="server" Text='<%# Bind("DEPARTMENTNAME") %>' />
            <br />
            SECTIONNAME:
            <asp:TextBox ID="SECTIONNAMETextBox" runat="server" Text='<%# Bind("SECTIONNAME") %>' />
            <br />
            GRADENAME:
            <asp:TextBox ID="GRADENAMETextBox" runat="server" Text='<%# Bind("GRADENAME") %>' />
            <br />
            CATEGORYNAME:
            <asp:TextBox ID="CATEGORYNAMETextBox" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
            <br />
            LOCATIONNAME:
            <asp:TextBox ID="LOCATIONNAMETextBox" runat="server" Text='<%# Bind("LOCATIONNAME") %>' />
            <br />
            EMPLOYEETYPENAME:
            <asp:TextBox ID="EMPLOYEETYPENAMETextBox" runat="server" Text='<%# Bind("EMPLOYEETYPENAME") %>' />
            <br />
            RECRUITMENTMODENAME:
            <asp:TextBox ID="RECRUITMENTMODENAMETextBox" runat="server" Text='<%# Bind("RECRUITMENTMODENAME") %>' />
            <br />
            DOB:
            <asp:TextBox ID="DOBTextBox" runat="server" Text='<%# Bind("DOB") %>' />
            <br />
            DOJ:
            <asp:TextBox ID="DOJTextBox" runat="server" Text='<%# Bind("DOJ") %>' />
            <br />
            GENDER:
            <asp:CheckBox ID="GENDERCheckBox" runat="server" Checked='<%# Bind("GENDER") %>' />
            <br />
            INACTIVE:
            <asp:CheckBox ID="INACTIVECheckBox" runat="server" Checked='<%# Bind("INACTIVE") %>' />
            <br />
            DOR:
            <asp:TextBox ID="DORTextBox" runat="server" Text='<%# Bind("DOR") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            EMPLOYEEID:
            <asp:TextBox ID="EMPLOYEEIDTextBox" runat="server" Text='<%# Bind("EMPLOYEEID") %>' />
            <br />
            EMPLOYEENAME:
            <asp:TextBox ID="EMPLOYEENAMETextBox" runat="server" Text='<%# Bind("EMPLOYEENAME") %>' />
            <br />
            EMPLOYEECODE:
            <asp:TextBox ID="EMPLOYEECODETextBox" runat="server" Text='<%# Bind("EMPLOYEECODE") %>' />
            <br />
            EMPLOYEERFID:
            <asp:TextBox ID="EMPLOYEERFIDTextBox" runat="server" Text='<%# Bind("EMPLOYEERFID") %>' />
            <br />
            CARDNUMBER:
            <asp:TextBox ID="CARDNUMBERTextBox" runat="server" Text='<%# Bind("CARDNUMBER") %>' />
            <br />
            DESIGNATION:
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DESIGNATIONNAME") %>' />
            <br />
            <br />
            REPORTING AUTHORITY1:
            <asp:TextBox ID="REPORTINGAUTHORITY1TextBox" runat="server" Text='<%# Bind("REPORTINGID1") %>' />
            <br />
            REPORTING AUTHORITY2:
            <asp:TextBox ID="REPORTINGAUTHORITY2TextBox" runat="server" Text='<%# Bind("REPORTINGID2") %>' />
            <br />
            FATHERNAME:
            <asp:TextBox ID="FATHERNAMETextBox" runat="server" Text='<%# Bind("FATHERNAME") %>' />
            <br />
            MOTHERNAME:
            <asp:TextBox ID="MOTHERNAMETextBox" runat="server" Text='<%# Bind("MOTHERNAME") %>' />
            <br />
            RESIDENTIALADDRESS:
            <asp:TextBox ID="RESIDENTIALADDRESSTextBox" runat="server" Text='<%# Bind("RESIDENTIALADDRESS") %>' />
            <br />
            PERMANENTADDRESS:
            <asp:TextBox ID="PERMANENTADDRESSTextBox" runat="server" Text='<%# Bind("PERMANENTADDRESS") %>' />
            <br />
            CONTACTNO:
            <asp:TextBox ID="CONTACTNOTextBox" runat="server" Text='<%# Bind("CONTACTNO") %>' />
            <br />
            MOBILENO:
            <asp:TextBox ID="MOBILENOTextBox" runat="server" Text='<%# Bind("MOBILENO") %>' />
            <br />
            EMAILID:
            <asp:TextBox ID="EMAILIDTextBox" runat="server" Text='<%# Bind("EMAILID") %>' />
            <br />
            POB:
            <asp:TextBox ID="POBTextBox" runat="server" Text='<%# Bind("POB") %>' />
            <br />
            BLOODGROUP:
            <asp:TextBox ID="BLOODGROUPTextBox" runat="server" Text='<%# Bind("BLOODGROUP") %>' />
            <br />
            DEPARTMENTNAME:
            <asp:TextBox ID="DEPARTMENTNAMETextBox" runat="server" Text='<%# Bind("DEPARTMENTNAME") %>' />
            <br />
            SECTIONNAME:
            <asp:TextBox ID="SECTIONNAMETextBox" runat="server" Text='<%# Bind("SECTIONNAME") %>' />
            <br />
            GRADENAME:
            <asp:TextBox ID="GRADENAMETextBox" runat="server" Text='<%# Bind("GRADENAME") %>' />
            <br />
            CATEGORYNAME:
            <asp:TextBox ID="CATEGORYNAMETextBox" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
            <br />
            LOCATIONNAME:
            <asp:TextBox ID="LOCATIONNAMETextBox" runat="server" Text='<%# Bind("LOCATIONNAME") %>' />
            <br />
            EMPLOYEETYPENAME:
            <asp:TextBox ID="EMPLOYEETYPENAMETextBox" runat="server" Text='<%# Bind("EMPLOYEETYPENAME") %>' />
            <br />
            RECRUITMENTMODENAME:
            <asp:TextBox ID="RECRUITMENTMODENAMETextBox" runat="server" Text='<%# Bind("RECRUITMENTMODENAME") %>' />
            <br />
            DOB:
            <asp:TextBox ID="DOBTextBox" runat="server" Text='<%# Bind("DOB") %>' />
            <br />
            DOJ:
            <asp:TextBox ID="DOJTextBox" runat="server" Text='<%# Bind("DOJ") %>' />
            <br />
            GENDER:
            <asp:CheckBox ID="GENDERCheckBox" runat="server" Checked='<%# Bind("GENDER") %>' />
            <br />
            INACTIVE:
            <asp:CheckBox ID="INACTIVECheckBox" runat="server" Checked='<%# Bind("INACTIVE") %>' />
            <br />
            DOR:
            <asp:TextBox ID="DORTextBox" runat="server" Text='<%# Bind("DOR") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table style="width: 100%; border: 2">
                <tr>
                    <td align="center" colspan="4" style="height:132px">
                        <h1>Employee Personal Details</h1>
                    </td>
                    <td>
                        <asp:Image ID="Image1" runat="server" AlternateText="Photo Not Available" 
                            BorderColor="SteelBlue" BorderWidth="2px" Height="132px" ImageAlign="Left" 
                            ImageUrl='<%# "~/ShowEmployeeImage.aspx?ID=" +  Eval("EMPLOYEEID") %>'
                            Style="text-align: right" Width="126px" />
                    </td>
                </tr>
                <tr>
                    <td style="font-family: Calibri; background-color: #404040; color: White;
                        font-size: large; text-align: left" colspan="5">
                        Employee Department Details
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Employee Code&nbsp;&nbsp;&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="EMPLOYEECODELabel" runat="server" Text='<%# Bind("EMPLOYEECODE") %>'
                            Width="200px" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        RFID&nbsp;&nbsp;&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="EMPLOYEERFIDLabel" runat="server" Text='<%# Bind("EMPLOYEERFID") %>'
                            Width="200px" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Card No&nbsp;&nbsp;&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="CARDNUMBERLabel" runat="server" Text='<%# Bind("CARDNUMBER") %>' Width="200px" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Designation&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="DESIGNATIONNAMELabel" runat="server" Text='<%# Bind("DESIGNATIONNAME") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style24">
                        Department&nbsp; :
                    </td>
                    <td class="style25">
                        <asp:Label ID="DEPARTMENTNAMELabel" runat="server" Text='<%# Bind("DEPARTMENTNAME") %>' />
                    </td>
                    <td class="style26">
                    </td>
                    <td class="style26">
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Section&nbsp; :
                    </td>
                    <td class="style22">
                        <asp:Label ID="SECTIONNAMELabel" runat="server" Text='<%# Bind("SECTIONNAME") %>' />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Category&nbsp; :
                    </td>
                    <td class="style22">
                        <asp:Label ID="CATEGORYNAMELabel" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Grade&nbsp; :
                    </td>
                    <td class="style22">
                        <asp:Label ID="GRADENAMELabel" runat="server" Text='<%# Bind("GRADENAME") %>' />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Reporting Authority1&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="REPORTINGID1Label" runat="server" Text='<%# Bind("REPORTINGID1") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Reporting Authority2&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="REPORTINGID2Label" runat="server" Text='<%# Bind("REPORTINGID2") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="font-family: Calibri; background-color: #404040; color: White; font-size: large;
                        text-align: left" colspan="5">
                        Personal Information Details
                    </td>
                    
                </tr>
                <tr>
                    <td class="style21">
                        Employee Name&nbsp;&nbsp;&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <b>
                            <asp:Label ID="EMPLOYEENAMELabel" runat="server" Text='<%# Bind("EMPLOYEENAME") %>'
                                Width="200px" />
                        </b>
                    </td>
                    <td style="text-align: right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Date Of Birth :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="DOBLabel" runat="server" Text='<%# Bind("DOB", "{0:D}") %>' />
                    </td>
                    <td style="text-align: right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Father Name :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="FATHERNAMELabel" runat="server" Text='<%# Bind("FATHERNAME") %>' />
                    </td>
                    <td style="text-align: right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Mother Name :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="MOTHERNAMELabel" runat="server" Text='<%# Bind("MOTHERNAME") %>' />
                    </td>
                    <td style="text-align: right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Address 1&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="RESIDENTIALADDRESSLabel" runat="server" Text='<%# Bind("RESIDENTIALADDRESS") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Address 2&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="PERMANENTADDRESSLabel" runat="server" Text='<%# Bind("PERMANENTADDRESS") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Contact No&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="CONTACTNOLabel" runat="server" Text='<%# Bind("CONTACTNO") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Mobile No&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="MOBILENOLabel" runat="server" Text='<%# Bind("MOBILENO") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Email&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="EMAILIDLabel" runat="server" Text='<%# Bind("EMAILID") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="font-family: Calibri; background-color: #404040; color: White; font-size: large;
                        text-align: left" colspan="5">
                        Employement Details
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Employee Type&nbsp; :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="EMPLOYEETYPENAMELabel" runat="server" Text='<%# Bind("EMPLOYEETYPENAME") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Recruitment Mode :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="RECRUITMENTMODENAMELabel" runat="server" Text='<%# Bind("RECRUITMENTMODENAME") %>' />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style21">
                        Date Of join :&nbsp;
                    </td>
                    <td class="style22">
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DOJ", "{0:D}") %>' />
                    </td>
                    <td style="text-align: right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style23">
                        &nbsp;
                    </td>
                    <td class="style22">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style23">
                        &nbsp;
                    </td>
                    <td class="style22">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style23">
                        &nbsp;
                    </td>
                    <td class="style22">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style23">
                        &nbsp;
                    </td>
                    <td class="style22">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:samayConnectionString %>"
        SelectCommand="SELECT EMPLOYEEID , EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME ,EMPLOYEECODE,DESIGNATIONNAME,EMPLOYEERFID, CARDNUMBER, FATHERNAME, MOTHERNAME,
        RESIDENTIALADDRESS, PERMANENTADDRESS, CONTACTNO, MOBILENO, EMAILID, POB, BLOODGROUP,    
        DEPARTMENTNAME, SECTIONNAME, GRADENAME, CATEGORYNAME, LOCATIONNAME, EMPLOYEETYPENAME, RECRUITMENTMODENAME ,
        DOB, DOJ, GENDER, EM.INACTIVE ,DOR,photo,
        (select EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME from TK_EMPLOYEE EMP1
        where EMP1.EmployeeID=ReportingEmployeeID1) as REPORTINGID1,
        (select EMPLOYEEFIRSTNAME + ' ' + EMPLOYEELASTNAME AS EMPLOYEENAME from TK_EMPLOYEE EMP2
        where EMP2.EmployeeID=ReportingEmployeeID2) as REPORTINGID2
        FROM TK_EMPLOYEE EM
        INNER JOIN TK_SECTION SEC ON SEC.SECTIONID = EM.SECTIONID
        INNER JOIN TK_DEPARTMENT DM ON DM.DEPARTMENTID = SEC.DEPARTMENTID
        INNER JOIN TK_CATEGORY CM ON CM.CATEGORYID = EM.CATEGORYID
        INNER JOIN TK_GRADE GM ON GM.GRADEID = EM.GRADEID
        INNER JOIN TK_LOCATION LM ON LM.LOCATIONID = EM.LOCATIONID
        INNER JOIN TK_EMPLOYEETYPE ETP ON ETP.EMPLOYEETYPEID = EM.EMPLOYEETYPEID
        INNER JOIN TK_RECRUITMENTMODE RM ON RM.RECRUITMENTMODEID = EM.RECRUITMENTMODEID
        INNER JOIN TK_Designation DG ON EM.DesignationID=DG.DesignationID "></asp:SqlDataSource>
</asp:Content>
