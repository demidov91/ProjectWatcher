<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.ProjectModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Project" %>
<%@ Import Namespace="DAL" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    
    
    <title>The Project</title>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
    <link rel="stylesheet" href="../../Content/jquery-ui-1.8.17.custom.css" type="text/css"/>
    <link rel="stylesheet" href="../../Content/Calendar.css" type="text/css"/>
    <script src="../../Scripts/jquery-1.5.1.js" type="text/javascript"></script>
    <script src="../../Scripts/jdate.js" type="text/javascript"></script>
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
                <%= property.Name %>
                <% if (property.Type == "String")
                   {%>
                <input type="text" id="<%= property.SystemName %>" class="propertyString" value="<%= project.GetValue(property.SystemName) %>" />
                <input type="button" id="btn" class="<%= property.SystemName %>" style="visibility: hidden"
                    value="Submit" />
                <% } %>
                <% if (property.Type == "Number")
                   {%>
                <input type="number" id="<%= property.SystemName %>" class="propertyNumber" value="<%= project.GetValue(property.SystemName) %>"
                    onkeypress='validate(event)' />
                <input type="button" id="btn" class="<%= property.SystemName %>" style="visibility: hidden"
                    value="Submit" />
                <% } %>
                <% if (property.Type == "Date")
                   { %>
                <input type="text" readonly="readonly" id="<%= property.SystemName %>" class="propertyDate"
                    size="40" value="<%= project.GetValue(property.SystemName) %>" />
                <input type="button">
                <% }%>
                <% if (property.Type == "Percentage")
                   { %>
                <input type="text" id="<%= property.SystemName %>" class="propertyPercentage" value="<%= project.GetValue(property.SystemName) %>%" />
                <input type="button" id="btn" class="<%= property.SystemName %>" style="visibility: hidden"
                    value="Submit" />
                <% } %>
                <% if (property.Type == "Select")
                   { %>
                  <select id="<%= property.SystemName %>" class="propertySelect" tabindex="1" draggable="draggable" >
                       <option selected="selected" id="<%= project.GetValue(property.SystemName) %>"><%= project.GetValue(property.SystemName) %></option>
                       <% foreach (var valueToSelect in property.AvailableValuesAsArray)
                          { %>
                              <option id="valueToSelect"><%= valueToSelect %></option>
                          <% } %>
                  </select>

                <% } %>
                
                <% if (property.Type == "Multyselect")
                   {  %>

                <% } %>                
                <% } %>
            </div>
        </div>
        <input class="okCancelButton" name="accept" type="image" src="../../Resources/AcceptButton.png" />
    </div>
    
    
    

    <script type="text/javascript">
        $(".propertyNumber").keydown(function (event) {
            if (event.shiftKey == true) {
                event.preventDefault();
            }
            if ((event.keyCode >= 48 && event.keyCode <= 57)
                || (event.keyCode >= 96 && event.keyCode <= 105)
                    || event.keyCode == 8
                        || event.keyCode == 9
                            || event.keyCode == 37
                                || event.keyCode == 39 
                                    || event.keyCode == 46) {    
            } else {
                event.preventDefault();
            }
        });
    </script>
    


    <script type="text/javascript">
        $(function() {
            $("input.propertyDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>


    <script type="text/javascript">
        $("input").change(function () {
            var val = $(this).attr("value");
            $("input#btn." + $(this).attr("id")).css("visibility", "visible");
            $("input#btn." + $(this).attr("id")).click(function () {
                $("input#btn." + $(this).attr("class")).css("visibility", "hidden");
            });
        })
    </script>
    
    
    <script type="text/javascript">
        $("select").change(function () {
            var val = $(this).attr("value");
            $(this + " :contains(" + val + ")").attr("selected", "selected");
        })
    </script>
    
    
    



</body>
</html>
