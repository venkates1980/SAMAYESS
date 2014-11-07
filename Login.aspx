<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style5
        {
            width: 334px;
        }

        .style6
        {
            width: 57%;
        }

        .style13
        {
            height: 10px;
            text-align: right;
        }

        .style14
        {
            padding-bottom: 20px;
            height: 28px;
        }

        .style15
        {
            text-align: left;
        }
        /* Add whatever you need to your CSS reset */ html, body, h1, form, fieldset, input
        {
            margin: 0;
            padding: 0;
            border: none;
        }

        body
        {
            font-family: calibri;
            font-size: 12px;
        }

        #registration
        {
            color: #fff;
            background: #2d2d2d;
            background: -webkit-gradient( linear, left bottom, left top, color-stop(0, rgb(60,60,60)), color-stop(0.74, rgb(43,43,43)), color-stop(1, rgb(60,60,60)) );
            background: -moz-linear-gradient( top bottom, rgb(60,60,60) 0%, rgb(43,43,43) 74%, rgb(60,60,60) 100% );
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            margin: 10px;
            width: 430px;
        }

            #registration a
            {
                color: #8c910b;
                text-shadow: 0px -1px 0px #000;
            }

            #registration fieldset
            {
                padding: 20px;
            }

        input.text
        {
            -webkit-border-radius: 15px;
            -moz-border-radius: 15px;
            border: solid 1px #444;
            font-size: 14px;
            width: 90%;
            padding: 7px 8px 7px 30px;
            -moz-box-shadow: 0px 1px 0px #777;
            -webkit-box-shadow: 0px 1px 0px #777;
            background: #ddd url( 'img/inputSprite.png' ) no-repeat 4px 5px;
            background: url( 'img/inputSprite.png' ) no-repeat 4px 5px, -moz-linear-gradient( center bottom, rgb(225,225,225) 0%, rgb(215,215,215) 54%, rgb(173,173,173) 100% );
            background: url( 'img/inputSprite.png' ) no-repeat 4px 5px, -webkit-gradient( linear, left bottom, left top, color-stop(0, rgb(225,225,225)), color-stop(0.54, rgb(215,215,215)), color-stop(1, rgb(173,173,173)) );
            color: #333;
            text-shadow: 0px 1px 0px #FFF;
        }

        input#email
        {
            background-position: 4px 5px;
            background-position: 4px 5px, 0px 0px;
        }

        input#password
        {
            background-position: 4px -20px;
            background-position: 4px -20px, 0px 0px;
        }

        input#name
        {
            background-position: 8px -46px;
            background-position: 8px -46px, 0px 0px;
        }

        input#tel
        {
            background-position: 4px -76px;
            background-position: 4px -76px, 0px 0px;
        }

        #registration h2
        {
            color: #fff;
            text-shadow: 0px -1px 0px #000;
            border-bottom: solid #181818 1px;
            -moz-box-shadow: 0px 1px 0px #3a3a3a;
            text-align: center;
            padding: 18px;
            margin: 0px;
            font-weight: normal;
            font-size: 24px;
            font-family: calibri;
        }

        #registerNew
        {
            width: 203px;
            height: 40px;
            border: none;
            text-indent: -9999px;
            background: url( 'createAccountButton.png' ) no-repeat;
            cursor: pointer;
            float: right;
        }

            #registerNew:hover
            {
                background-position: 0px -41px;
            }

            #registerNew:active
            {
                background-position: 0px -82px;
            }

        #registration p
        {
            position: relative;
        }

        fieldset label.infield /* .infield label added by JS */
        {
            color: #333;
            text-shadow: 0px 1px 0px #fff;
            position: absolute;
            text-align: left;
            top: 3px !important;
            left: 35px !important;
            line-height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <div id="page" class="container">
        <div id="sidebar">
            <div class="container">
                <div id="Div1">
                    <%-- <table>
                        <tr>
                            <td style="padding-right: 2px; padding-left:10px">
                                <div id="registration">
                                    <h1 style="color: White; height: 49px; text-align: center; padding-top: 2px">
                                        Log In</h1>
                                    <p>
                                        <label for="name" style="font-size: large">
                                            Username</label>
                                        <asp:TextBox ID="txtusername" runat="server" CssClass="text"></asp:TextBox>
                                    </p>
                                    <p>
                                        <label for="password" style="font-size: large">
                                            Password</label>
                                        <asp:TextBox ID="txtpwd" runat="server" TextMode="Password" Style="text-align: left"
                                            CssClass="text"></asp:TextBox>
                                    </p>
                                    <p style="text-align: right; left: 200px; padding-right: 161px">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/login.png" />
                                    </p>
                                    <p>
                                        <asp:Label ID="lblerror" runat="server" Font-Bold="False" Font-Size="Small" ForeColor="Red"
                                            Visible="False"></asp:Label></p>
                                </div>
                            </td>
                        </tr>
                    </table>--%>
                    <table >
                        <tr>
                            <td style="padding-left: 208px; position:relative; top: 2px; left: 2px; width: 864px;">
                                <div id="registration">
                                    <h2 style="color:White;text-align:center">
                                        Log In</h2>
                                    <form id="RegisterUserForm" style="padding-left: 40px">
                                    <fieldset>
                                        <p>
                                            <label for="name" style="font-size: small">
                                                Username</label>
                                            <asp:TextBox ID="txtusername" runat="server" CssClass="text"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtusername" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <label for="password" style="font-size: small">
                                                Password</label>
                                            <asp:TextBox ID="txtpwd" runat="server" TextMode="Password" Style="text-align: left"
                                                CssClass="text"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txtpwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </p>
                                        <p style="text-align: left; left: 153px; ">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/login.png" 
                                                onclick="ImageButton1_Click1" />
                                        </p>
                                        <p>
                                            <asp:Label ID="lblerror" runat="server" Font-Bold="False" Font-Size="Small" ForeColor="Red"
                                                Visible="False"></asp:Label></p>
                                    </fieldset>
                                    </form>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="button">
            </div>
            <!-- button -->
            <!-- content -->
        </div>
    </div>
</asp:Content>
