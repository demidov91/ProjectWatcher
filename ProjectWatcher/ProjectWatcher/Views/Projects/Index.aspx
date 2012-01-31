<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeaderPlaceholder">
    
    <title>Index</title>
    
    <script type="text/javascript" src="../../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.validationEngine-en.js"></script>    
    <link rel="stylesheet" href="../../Content/validationEngine.jquery.css" type="text/css"/>
    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.11.js"></script>
    <link rel="stylesheet" href="../../Content/jquery-ui-1.8.17.custom.css"/>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/Table.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/Button.css" type="text/css" />
    <link rel="stylesheet" href="<%=Url.Content("~/Scripts/jQuerry/development-bundle/themes/start/jquery.ui.all.css")%>" />
    <script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.core.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.widget.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.mouse.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.draggable.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.position.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.resizable.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Scripts/jQuerry/development-bundle/ui/jquery.ui.dialog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Scripts/Site.js")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ProjectWatcherPlaceholder">
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
    
<script>
    $(document).ready(function () {
        $("#editTableForm").validationEngine();
    });
</script>    

</div>



<script>
    $("#projectsTable, #tableSettingButton").live('mouseover mouseout', function (event) {
        if (event.type == 'mouseover') {
            $("#tableSettingButton").css({
                "opacity": 1
            });
        } else {
            $("#tableSettingButton").css({
                "opacity": 0.3
            });
        }
    });
</script>


<script>
    function aligment() {
        var clWidth = document.body.clientWidth;
        var div = document.getElementById('projectsEverything');
        var lOffset = (clWidth - div.clientWidth) / 2;
        div.style.left = lOffset + "px";
    }
    window.onload = aligment;
    window.onresize = aligment;
</script>
</asp:Content>

