<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.ProjectWithValuesModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Project" %>
<%@ Import Namespace="DAL.Interface" %>
<%@ Import Namespace="ProjectWatcher.Helpers" %>



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
                
                <%=Model.Name%>
            </span><span>-edited by
                <%=ViewData["lastUser"]%>
                at
                <%=Model.LastChanged%></span>
        </div>
        <div id="historyAndDescription">
            <%=Model.GetValue("description") %>
        </div>
        <div id="properties">
            <% foreach (ValueModel value in Model.Values)
               {
                   if (value.SystemName == "history" || value.SystemName == "description")
                   {
                       continue;
                   } %>
            <div>
                <%=value.Name%>
                <% if (value.Property.Type == "String")
                   {%>
                <input type="text" id="<%= value.SystemName %>" class="propertyString" value="<%= value.Value %>" />
                <input type="button" id="btn" class="<%= value.SystemName %>" style="visibility: hidden"
                    value="Submit" />
                <% } %>
                <% if (value.Property.Type == "Number")
                   {%>
                <input type="number" id="<%= value.SystemName %>" class="propertyNumber" value="<%= value.Value %>"
                    onkeypress='validate(event)' />
                <input type="button" id="btn" class="<%= value.SystemName %>" style="visibility: hidden"
                    value="Submit" />
                <% } %>
                <% if (value.Property.Type == "Date")
                   { %>
                <input type="text" readonly="readonly" id="<%= value.SystemName %>" class="propertyDate"
                    size="40" value="<%= value.Value %>" />
                <input type="button">
                <% }%>
                <% if (value.Property.Type == "Percentage")
                   { %>
                <input type="text" id="<%= value.SystemName %>" class="propertyPercentage" value="<%= value.Value %>%" />
                <input type="button" id="btn" class="<%= value.SystemName %>" style="visibility: hidden"
                    value="Submit" />
                <% } %>
                <% if (value.Property.Type == "Select")
                   { %>
                  <select id="<%= value.SystemName %>" class="propertySelect" tabindex="1" draggable="draggable" >
                       <option selected="selected" id="<%= value.Value %>"><%= value.Value%></option>
                       <% foreach (IAvailableValue valueToSelect in value.Property.GetAvailableValues())
                          { %>
                              <option id="valueToSelect"><%= valueToSelect %></option>
                          <% } %>
                  </select>

                <% } %>
                
                <% if (value.Property.Type == "Multyselect")
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
