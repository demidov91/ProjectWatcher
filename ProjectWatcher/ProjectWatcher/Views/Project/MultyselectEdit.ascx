<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.MultyselectModel>" %>
<%@Import namespace="ProjectWatcher.Helpers" %>

<div id="<%= "property_" + Model.Id %>">
    <div id="<%= "newValue_" + Model.Id %>">
        <% for (int i = 0; i < Model.AvailableValues.Count; i++)
           {%>
               <%:Html.CheckBoxFor(m => m.AvailableValues[i].IsChecked, new{value=Model.AvailableValues[i].Id})%>
               <%=Model.AvailableValues[i].Value %><br/><%
           } %>
        
    </div>
    <div id="<%="availableValues_"+Model.Id%>">
        <textarea class="availableEdit"><%=Html.RenderModel(Model) %></textarea>
    </div>
    
    
    
</div>