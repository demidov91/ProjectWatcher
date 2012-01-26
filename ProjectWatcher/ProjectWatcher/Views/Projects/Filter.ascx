<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.FilterModel>" %>

<div id="labelFilterBlock">
        <span class="leftSide">
            <%=Html.Encode(Model.LabelBeforeFilter) %>
        </span>
        <span class="leftSide">
            <%=Html.Encode(Model.Filter) %>
        </span>
        <input type="image" class="leftSide" src="../../Resources/SettingsButton.png" alt="settings" />    
</div>
<div id="editFilterBlock">
    <form method="post" action="<%=Html.AttributeEncode(Url.Action("EditFilter", new{tableDefinition=Model.TableDefinition})) %>">
        <span class="leftSide">
            <%=Html.Encode(Model.LabelBeforeFilter) %>
        </span>
        <span class="leftSide">
            <%=Html.TextBoxFor(m => m.Filter)%>
        </span>
        <input type="image" src="../../Resources/AcceptButton.png" name="accept" class="leftSide" />
    </form>
    <input type="image" src="../../Resources/RejectButton.png" name="reject" class="leftSide" onclick="" />
</div>