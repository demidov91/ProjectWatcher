<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.TableModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Projects" %>
<%@ Import namespace="ProjectWatcher.Helpers" %>

<div id="labelTableBlock">
    <%if (ViewData["errorMessage"] != null)
      {%>
    <div id="dialog">
        <%:Html.Encode(ViewData["errorMessage"])%>
    </div>
    <%} %>
    <table id="projectsTable">
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
    
    
 <input type="image" id="tableSettingButton" width="36px" height="36px" src="../../Resources/SettingsButton.png" border="1" onclick="$('#editTableBlock').css('display','block');$('#labelTableBlock').css('display','none');"/>
</div>


<div id="editTableBlock">
    <form id="editTableForm" name="mainform" action="<%=Html.AttributeEncode(Url.Action("EditTable", new {filter=Model.Filter}))%>" method="post" draggable="draggable" onsubmit="$('#editTableBlock').css('display', 'none'); $('#labelTableBlock').css('display','block');">
        <table id="editForTableDefinitionWrapper">
            <tr>
                <td>
                    <%=Html.TextAreaFor(m => m.TableDefinition, new { id = "editForTableDefinition" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <img class="okCancelButton" type="image" src="../../Resources/RejectButton.png" onclick="$(editTableBlock).css('display', 'none');$('#labelTableBlock').css('display','block');" style="cursor: pointer"/>
                    <input class="okCancelButton" id="accept" form="editTableForm" name="accept" type="image" src="../../Resources/AcceptButton.png" />
                </td>
            </tr>
        </table>        
    </form>
</div>


 

