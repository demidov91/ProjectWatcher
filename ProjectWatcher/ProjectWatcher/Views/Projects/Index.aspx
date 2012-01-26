<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
</head>
<body>
<div id="projectsEverything">
    <div>
        <%Html.RenderPartial("Filter", ViewData["filterModel"]); %>          
    </div>
    <div>
        <%Html.RenderPartial("Table", ViewData["tableModel"]); %>
    </div>
    
    <div>
        <%Html.RenderPartial("Footer", ViewData["footerModel"]); %>
    </div>
</div>
</body>
</html>
