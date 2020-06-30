<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TesterModel.Question>" %>

<h3 id="QuestionText"><%= Model.Text %></h3>

<%using (Html.BeginForm("Index", "Test", FormMethod.Post, new { ID = "formAnswers" }))
  {%>
<div id="HiQuestionID">
    <%= Html.Hidden("QuestionID", Model.QuestionID) %>
</div>

<div id="Answers">
    <%int count = 0;
      foreach (var answer in Model.OptionAnswers)
      { %>
    <div id="Answer">
        <input type="radio" id="NumOptionAnswer" name="NumOptionAnswer" value="<%=count %>" /><%=answer %>
    </div>
    <%count++;
      } %>
</div>
<input id="Next" type="submit" value="Далее"/>
<%} %>
<script type="text/javascript">

</script>