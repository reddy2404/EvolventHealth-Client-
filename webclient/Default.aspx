<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webclient._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-top:15px;">
        <div style="color: blue ;">
            <h4>Simple WCF Evolvent Health Service</h4>
        </div>


        <asp:Label ID="lblError" runat="server"></asp:Label>
        <table>
            <tr>
                <td style="visibility: hidden">Id :
                </td>
                <td>
                    <asp:TextBox ID="txtId" runat="server" Enabled="false" Visible="false" />
                </td>
                
            </tr>
            <tr>
                <td>First Name :
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" Style="width: 300px" placeholder="FirstName" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="revFName" ControlToValidate="txtFirstName" runat="server" ErrorMessage="Field required" ValidationGroup="insert"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="revFirst" runat="server" ControlToValidate="txtFirstName"
                        ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Valid characters: Alphabets and space." />
                </td>
            </tr>
            <tr>
                <td>Last Name :
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" Style="width: 300px" placeholder="LastName"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="revLName" ControlToValidate="txtLastName" runat="server" ErrorMessage="Field required" ValidationGroup="insert"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="revLast" runat="server" ControlToValidate="txtLastName"
                        ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Valid characters: Alphabets and space." />
                </td>
            </tr>
            <tr>
                <td>Email :
                </td>
                <td>
                    <asp:TextBox ID="txtEamil" runat="server" Style="width: 300px" placeholder="xxxxxx@xx.xxx"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Inval id(xxxxxx@xx.xxx)"
                        ControlToValidate="txtEamil" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="insert"> </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Phone No :
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" Style="width: 300px" placeholder="xxx-xxx-xxxx"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="revPhone" runat="server"
                        ErrorMessage="Invalid Number(xxx-xxx-xxxx)" ControlToValidate="txtPhone" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ValidationGroup="insert"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Status :
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false" Width="130px" >
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>true</asp:ListItem>
                        <asp:ListItem>false</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="ButtonInsert" runat="server" Text="Add" OnClick="InsertButton_Click" ValidationGroup="insert" />
                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" ValidationGroup="insert" />
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="DeleteButton_Click" />
                    <asp:Button ID="ButtonCancel" runat="server" Text="Clear" OnClick="CancelButton_Click" />

                </td>
            </tr>
        </table>
        <div style="padding-top: 30px;">

            <asp:GridView ID="gdvDetails" DataKeyNames="ID" AutoGenerateColumns="False"
                runat="server" Width="700px" OnSelectedIndexChanged="OnSelectedIndexChanged">
                <HeaderStyle BackColor="#0A9A9A" ForeColor="White" Font-Bold="true" Height="30" />
                <AlternatingRowStyle BackColor="#f5f5f5" />
                <Columns>
                    <%--  <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" Text="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                    <asp:BoundField DataField="ID" HeaderText="id"></asp:BoundField>
                    <asp:TemplateField HeaderText="First Name">
                        <ItemTemplate>
                            <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("FirstName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name">
                        <ItemTemplate>
                            <asp:Label ID="lblLastName" runat="server" Text='<%#Eval("LastName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EmailID">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EmailID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PhoneNumber">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("PhoneNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>




</asp:Content>
