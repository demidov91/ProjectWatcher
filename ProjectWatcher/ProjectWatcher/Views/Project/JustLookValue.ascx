<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>
<%@ Import Namespace="ProjectWatcher.Helpers" %>

<div class="forEditing" id="<%="property_" + Model.SystemName %>">
        <div class="value">
<<<<<<< HEAD
            <p>
            <%:Html.RenderModel(Model) %>
            </p>
=======
            <%:Html.RenderModel(Model) %>
>>>>>>> master
        </div>
        <%if (Model.IsEditable)
          { %>
        <div class="editButton">
            <%using(Ajax.BeginForm("EditValue", new{id=Model.Id}, new AjaxOptions{ HttpMethod = "post", InsertionMode= InsertionMode.Replace, UpdateTargetId = "property_"+Model.SystemName}))
              {  %>
                <input type="image" src="../../Resources/pinion.png" />        
<<<<<<< HEAD
            <%}%>
             </div>
         <% } %>
         
=======
            <%}
          } %>
        </div>
>>>>>>> master
 </div>