<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<div id="<%="property_" + Model.Id %>">
    <div>
        <%:Html.TextAreaFor(m => m.Value, new{Class="stringEdit", id="newValue_"+Model.Id}) %>
    </div>
    
    
</div>