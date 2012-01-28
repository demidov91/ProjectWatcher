<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.TableModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Projects" %>
<%@ Import namespace="ProjectWatcher.Helpers" %>

<<<<<<< HEAD
<div id="labelTableBlock">
=======
<div id="labelTableBlock" >
>>>>>>> master
    <%if (ViewData["errorMessage"] != null)
      {%>
    <div id="dialog">
        <%:Html.Encode(ViewData["errorMessage"])%>
    </div>
    <%} %>
<<<<<<< HEAD
    <table id="projectsTable" border="1">
=======
    <table id="projectsTable" border="3">
>>>>>>> master
        <tr id="tableHeader">
            <%for (int i = 0; i < Model.Headers.Length; i++)
              {%>
              <%:Html.GetWidthTd(Model.Headers[i], Model.Width[i])%>
                  <%
              } %>
        </tr>
        <%foreach (ProjectModel project in Model.Projects)
            {%>
            <tr>
            <%for (int i = 0; i < project.Properties.Length; i++)
                { %>
                <td>
                    <%=project.Properties[i] %>
                </td>
                <%} %> 
            </tr>                   
            <%} %>
    </table>
    <input type="image" id="tableSettingButton" src="../../Resources/SettingsButton.png" border="2"/>
</div>

<div id="editTableBlock">
    <form action="<%=Html.AttributeEncode(Url.Action("EditTable", new {filter=Model.Filter}))%>" method="post">
        <table id="editForTableDefinitionWrapper">
            <tr>
                <td>
                    <%=Html.TextAreaFor(m => m.TableDefinition, new {  id="editForTableDefinition"})%>
                </td>
            </tr>
            <tr>
                <td>
                    <img class="okCancelButton" src="../../Resources/RejectButton.png" alt="cancel" onclick="" />
                    <input class="okCancelButton" name="accept" type="image" src="../../Resources/AcceptButton.png" />
                </td>
            </tr>
        </table>        
    </form>
</div>


<script>
    $("#projectsTable, #tableSettingButton").mouseover(function () {
        $("#projectsTable").css("border-color", "red");
        $("#tableSettingButton").css({
            "border-color": "red",
            "opacity": 1
        });
    });
</script>


 
<script>
    $("#projectsTable, #tableSettingButton").mouseout(function () {
        $("#projectsTable").css("border-color", "black");
        $("#tableSettingButton").css({
            "border-color": "white",
            "opacity": 0.2
        });
    });
</script>
