<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.PropertyModel>" %>
<%@Import namespace="ProjectWatcher.Models.Project" %>


<%
        
    String changeImportanceCall = "changeImportance("+ "\'" + Model.SystemName + "\'" + "," + Model.ProjectId.ToString() + ")";
    %>
 <div id="<%="forImportance" + Model.SystemName %>">
        <%
    if (Model.IsImportant)
    { %>

    <input type="image" src="../../Resources/AcceptButton.png" onclick="<%=changeImportanceCall %>"/>

    <% }
    else
    { %>

    <input type="image" src="../../Resources/No.png" onclick="<%=changeImportanceCall %>" />

    <% } %>
</div>
