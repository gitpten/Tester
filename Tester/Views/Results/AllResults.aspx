<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TesterModel.TestResult>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>AllResults</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="header">
        <h1>AllResults</h1>
    </div>

    <div id="menu">
        <%= Html.ActionLink("Log out", "Logout", "Account") %>
    </div>

    <div id="content">
        <table>
            <tr>
                <th><%= Ajax.ActionLink("User name","AllResults",new{page=(int)ViewData["CurrentPage"], SortField="UserName"},new AjaxOptions{UpdateTargetId="myResult"}) %></th>
                <th>Test name</th>
                <th><%= Ajax.ActionLink("Date","AllResults",new{page=(int)ViewData["CurrentPage"], SortField="Date"},new AjaxOptions{UpdateTargetId="myResult"}) %></th>
                <th><%= Ajax.ActionLink("Time","AllResults",new{page=(int)ViewData["CurrentPage"], SortField="Time"},new AjaxOptions{UpdateTargetId="myResult"}) %></th>
                <th><%= Ajax.ActionLink("Result","AllResults",new{page=(int)ViewData["CurrentPage"], SortField="Result"},new AjaxOptions{UpdateTargetId="myResult"}) %></th>
            </tr>
            <%foreach (var line in Model)
              {
                  if (!line.IsDone)
                      continue;%>
            <tr class="result">
                <td><%=line.UserName %></td>
                <td><%= ((List<TesterModel.Test>)ViewData["Tests"]).FirstOrDefault(t=>t.TestID==line.TestID).Name %></td>
                <td><%=line.BeginTime.ToString("dd.MM.yyyy") %></td>
                <td><%=(line.FinishTime-line.BeginTime).ToString(@"mm\:ss") %></td>
                <td><%=Math.Round((double)(line.Answers.Count(a => a.IsCorrect)) / ((List<TesterModel.Test>)ViewData["Tests"]).FirstOrDefault(t=>t.TestID==line.TestID).Questions.Count *100) %> %</td>
            </tr>
            <%} %>
        </table>
        <div class="pager">
            Page: <%= Html.PageLinks((int)ViewData["CurrentPage"], (int)ViewData["TotalPage"], x => Url.Action("AllResults", new { page = x, SortField = ViewData["SortField"] }))%>
        </div>
    </div>
</asp:Content>

