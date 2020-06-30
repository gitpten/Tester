<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TesterModel.TestResult>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Result test</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="header">
        <h1>Result test: <%=ViewData["TestName"] %></h1>
    </div>

    <div id="menu">
        <%= Html.ActionLink("All results", "AllResults", "Results") %>
        <%= Html.ActionLink("Log out", "Logout", "Account") %>
    </div>

    <div id="content">
        <table>
            <tr>
                <th>Time</th>
                <th>Result</th>
            </tr>
            <tr>
                <td><%=ViewData["Time"] %></td>
                <td><%=ViewData["ResultTest"] %> %</td>
            </tr>
        </table>
    </div>

</asp:Content>