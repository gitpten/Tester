<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TesterModel.Test>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%= Url.Content("~/Scripts/testajax.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/time.js")%>" type="text/javascript"></script>
    <title><%=Model.Name %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1>Testing</h1>
        <h2>Check the response to the test question</h2>
    </div>

    <div id="menu">
        <%=Html.ActionLink("All results", "AllResults", "Results") %>
        <%= Html.ActionLink("End & Log out", "Logout", "Account") %>
    </div>

    <div id="content">
        <h3>Name test: <%=Model.Name %>. User: <%= ViewData["UserName"] %></h3>
        <h3>Date: <%=DateTime.Now.ToString("dd.MM.yyyy") %></h3>
        <h3> Time left:</h3>
        <form name="frmTime">
            <div id="timespent">
                <%=ViewData["TimeLeft"] %>
            </div>
        </form>
        <h3>Questions left:</h3>
        <div id="Questionsleft">
            <%= Model.Questions.Count - (int)ViewData["numQuestion"] %>
        </div>
        <%Html.RenderPartial("QuestionForm", Model.Questions[(int)ViewData["numQuestion"]]); %>
    </div>
</asp:Content>

