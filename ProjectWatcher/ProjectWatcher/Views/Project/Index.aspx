<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.Index.ProjectWithValuesModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Project.Index" %>


<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    
    
    <title>The Project</title>
    <link rel="Stylesheet" href="../../Content/ProjectIndex.css" type="text/css" />
    
</head>
<body>
<div id="ProjectWithProperties">
    <div id="forTitle">
        <span class="header"><%=Model.Name%> </span><span>-edited by <%=Model.LastUser%> at <%=Model.LastChanged%></span>
    </div>
    <div id="forProperties">
        <%foreach (ValueModel value in Model.Values)
          {
              if(value.IsVisible)
              Html.RenderPartial("BigValue", value);
          } %>
    </div>
    <div id="footer">
        <div id="forAddPropertyButton">
            <form method="post" action="<%=Html.AttributeEncode(Url.Action("AddProperties", "Project", new {projectId=Model.ProjectId})) %>" >
                <input type="image" src="../../Resources/yellowPlus.png"  />
            </form>
        </div>
        <div id="forBackButton">
            <form method="post" action="<%=Html.AttributeEncode(Url.Action("Index", "Projects")) %>" >
                <input type="image" src="../../Resources/back.png" />
            </form>
        </div>
    </div>
</div>
</body>
</html>
