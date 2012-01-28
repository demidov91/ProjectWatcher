<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<div class="property" >
    <div class="valueName">
        <%:Html.Encode(Model.Name) %>
    </div>
    <%Html.RenderPartial("JustLookValue", Model); %>
</div>