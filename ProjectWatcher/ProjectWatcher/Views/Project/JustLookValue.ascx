<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>
<%@ Import Namespace="ProjectWatcher.Helpers" %>

<div class="forEditing" id="<%="property_" + Model.Id %>">
        <div class="value">
            <p>
            <%:Html.RenderModel(Model) %>
            </p>
        </div>
        <%if (Model.IsEditable)
          { %>
        <div class="editButton">
            <%using(Ajax.BeginForm("EditValue", new{id=Model.Id}, new AjaxOptions{ HttpMethod = "get", InsertionMode= InsertionMode.Replace, UpdateTargetId = "property_"+Model.Id}))
              {  %>
                <input type="image" src="../../Resources/pinion.png" />        
            <%}%>
             </div>
         <% } %>
         
 </div>