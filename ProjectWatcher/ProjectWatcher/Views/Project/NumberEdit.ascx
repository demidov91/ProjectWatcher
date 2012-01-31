<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<div id="<%="property_" + Model.Id %>">
    <div>
        <%:Html.TextBoxFor(m => m.Value, new{Class="numberEdit", id="newValue_"+Model.Id}) %>
    </div>
    
    
</div>