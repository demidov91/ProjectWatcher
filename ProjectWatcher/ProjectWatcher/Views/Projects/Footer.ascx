<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.FooterModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Projects" %>

<div id="visibleFooter">
    <% if (Model.IsAdmin)
       {%>
  
<form action="<%= Html.AttributeEncode(Url.Action("NewProject")) %>" method="post" style="margin: 10px;">
    <input class="leftSide button big blue" type="submit" value="<%= Model.AddProjectTitle %>" />
</form>
<% }%> 

<form style="margin: 10px;" action="<%=Html.AttributeEncode(Url.Action("Export", new{headers=((TableModel)ViewData["tableModel"]).Headers, values=((TableModel)ViewData["tableModel"]).AllValues})) %>" method="post">
    <input class="rightSide button big blue" type="submit" value="<%=Model.ExportTitle %>" />
</form>

    <div class="rightSide">
        <%Html.RenderPartial("Upload", Model.Upload); %>
    </div>
</div>
