<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<div id="<%="property_" + Model.SystemName %>">
    <div>
        <%:Html.TextBoxFor(m => m.Value) %>
    </div>
    <div>
        <%Html.RenderPartial("ValueHistory"); %>
    </div>
    <div>
        <input type="image" src="../../Resources/RejectButton.png" id="<%="rejectButton_"+Model.SystemName %>" />
        <input type="image" src="../../Resources/AcceptButton.png" id="<%="rejectButton_"+Model.SystemName%>"/>
    </div>
</div>