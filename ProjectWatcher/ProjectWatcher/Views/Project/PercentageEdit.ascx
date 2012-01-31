<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<div id="<%="property_" + Model.Id %>">
    <div>
        <%:Html.TextBoxFor(m => m.Value, new{Class="percentageEdit", id="newValue_"+Model.Id}) %>
    </div>
    <span>  %</span>
    <div style="clear:both;"></div>
    
</div>