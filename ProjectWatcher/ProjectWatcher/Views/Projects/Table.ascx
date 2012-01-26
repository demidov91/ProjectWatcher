<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.TableModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Projects" %>
<%@ Import namespace="ProjectWatcher.Helpers" %>

<div id="labelTableBlock">
    <table id="projectsTable" border="1">
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
    <input type="image" src="../../Resources/SettingsButton.png"/>
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