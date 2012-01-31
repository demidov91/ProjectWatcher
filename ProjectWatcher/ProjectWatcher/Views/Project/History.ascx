<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.HistoryModel>" %>
<%@ Import namespace="ProjectWatcher.Models.Helpers" %>

<textarea readonly="readonly" class="history">
    <%=Model.ShowAll((String)ViewData["culture"]) %>
</textarea>