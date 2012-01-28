<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
    <link rel="stylesheet" href="<%=Url.Content("~/Scripts/jQuerry/development-bundle/themes/base/jquery.ui.all.css")%>" />
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/jquery-1.7.1.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.core.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.widget.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.mouse.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.draggable.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.position.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.resizable.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.dialog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Scripts/Site.js")%>" type="text/javascript"></script>

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
