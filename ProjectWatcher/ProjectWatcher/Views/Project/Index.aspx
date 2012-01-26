<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.ProjectModel>" %>

<%@ Import Namespace="ProjectWatcher.Models.Project" %>
<%@ Import Namespace="DAL" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>The Project</title>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
    <script src="../../Scripts/jquery-1.5.1.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.11.js" type="text/javascript"></script>
</head>
<body>
    <div id="ProjectWithProperties">
        <div id="title">
            <span class="header">
                <%DAL.ProjectsReader reader = new ProjectsReader();
                  DAL.Project project = reader.GetProject(Model.Id);   
                %>
                <%=project.Name%>
            </span><span>-edited by
                <%=ViewData["lastUser"]%>
                at
                <%=project.LastChanged%></span>
        </div>
        <div id="historyAndDescription">
            <%=project.GetValue("description") %>
        </div>
        <div id="properties">
            <% foreach (PropertyModel property in Model.ProjectProperties)
               {
                   if (property.SystemName == "history" || property.SystemName == "description")
                   {
                       continue;
                   } %>
            <div>
                <%=property.Name%>
                <%if (property.Type == "String")
                  {%>
                <span id="<%=property.SystemName%>" class="propertyString">
                    <%=project.GetValue(property.SystemName)%></span>
                <div>
                    <p>
                        <input type="text" id="input" value="<%=project.GetValue(property.SystemName)%>"
                            class="<%=property.SystemName %>" style="visibility: hidden" />
                        <input type="button" id="btn-string" class="<%=property.SystemName %>" style="visibility: hidden"
                            value="Submit" /></p>
                </div>
                <% } %>
                <%if (property.Type == "Number")
                  {%>
                <input type="number" id="<%=property.SystemName%>" class="propertyNumber" min="0"
                    value="<%=project.GetValue(property.SystemName)%>" onkeypress='validate(event)' />
                <p>
                    <input type="button" id="btn-number" class="<%=property.SystemName%>" style="visibility: hidden"
                        value="Submit" /></p>
                <% } %>
                <%if (property.Type == "Date")
                  { %>
                <% }%>
                <% } %>
            </div>
        </div>
        <input class="okCancelButton" name="accept" type="image" src="../../Resources/AcceptButton.png" />
    </div>
    
    
    
    
    
    <script type="text/javascript">
        $(".propertyString").click(function () {
            var sys = $(this).attr('id');
            $("." + sys).css("visibility", "visible");
            $("." + sys).css("visibility", "visible");
        })
    </script>
    
    
    

    <script type="text/javascript">
        $(".propertyNumber").click(function () {
            var sys = $(this).attr('id');
            $("." + sys).css("visibility", "visible");
        })
    </script>
    
    
    <script type="text/javascript">
        $("input#btn-number").click(function () {
            var sys = $(this).attr('class');
            var val = $("#sys").attr('value');
            $("#btn-number:not(.red)." + sys).css("visibility", "hidden");  
            
        })
    </script>
    
    


    <script type="text/javascript">
        $("input#btn-string").click(function () {
            var sys = $(this).attr('class');
            var val = $("input." + sys).attr('value');
            $("#" + sys + ".propertyString").text(val);
            $("#input:not(.red)." + sys).css("visibility", "hidden");
            $("#btn-string:not(.red)." + sys).css("visibility", "hidden");  
        })
    </script>
    
    
    

    <script type="text/javascript">
        $(".propertyNumber").keydown(function (event) {
            // Prevent shift key since its not needed
            if (event.shiftKey == true) {
                event.preventDefault();
            }
            // Allow Only: keyboard 0-9, numpad 0-9, backspace, tab, left arrow, right arrow, delete
            if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 46) {
                // Allow normal operation
            } else {
                // Prevent the rest
                event.preventDefault();
            }
        });
    </script>
</body>
</html>
