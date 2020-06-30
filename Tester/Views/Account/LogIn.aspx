<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%= Url.Content("~/Scripts/textIsEmpty.js")%>" type="text/javascript"></script>
    <title>LogIn</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="header">
            <h1>Введите имя</h1>
    </div>

    <div id="menu">
        <%=Html.ActionLink("All results", "AllResults", "Results") %>
    </div>
    
    <div id="content">
        <form method="post" name="frm">
            <input type="text" name="UserName" onchange="textisEmpty()" />
            <input type="submit" name="LogIn" value="Log in" disabled="disabled" />
        </form>
    </div>
</asp:Content>
