<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.FooterModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Projects" %>

<div id="visibleFooter">
<form action="<%=Html.AttributeEncode(Url.Action("NewProject")) %>">
    <input class="leftSide" type="submit" value="<%=Model.AddProjectTitle%>" />
</form>

<form action="<%=Html.AttributeEncode(Url.Action("Export", new{headers=((TableModel)ViewData["tableModel"]).Headers, values=((TableModel)ViewData["tableModel"]).AllValues})) %>" method="post">
    <input class="rightSide" type="submit" value="<%=Model.ExportTitle %>" />
</form>

<table class="rightSide">
    <tr>
        <td>
            <%Html.RenderPartial("Upload", Model.Upload); %>
        </td>
    </tr>        
    <tr>
        <td>
            <input type="submit" value="<%=Model.UploadTitle %>" onclick="" />
        </td>
    </tr>               
</table>
</div>