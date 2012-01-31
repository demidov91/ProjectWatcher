<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<div>
    <div>
        <% 
            switch (Model.Type)
            {
                case "String":
                    Html.RenderPartial("StringEdit", Model);
                    break;
                case "Number":
                    Html.RenderPartial("NumberEdit", Model);
                    break;
                case "Percentage":
                    Html.RenderPartial("PercentageEdit", Model);
                    break;
                case "Date":
                    Html.RenderPartial("DateEdit", Model);
                    break;
                case "Select":
                    Html.RenderPartial("SelectEdit", Model);
                    break;
                case "Multyselect":
                    Html.RenderPartial("MultySelectEdit", Model);
                    break;
            }
       %>
       </div>
       <div>
           <%Html.RenderPartial("History", Model.History); %>
       </div>
       <div class="forOkCancelButton">
           <div id="<%="error_"+Model.Id %>"></div>
           <div>
           <input type="image" src="./../../Resources/AcceptButton.png" onclick="modifyValue('<%=Html.AttributeEncode(Url.Action("AjaxValueOperation")) %>',<%=Model.Id %>)" />
           <input type="image" src="./../../Resources/RejectButton.png" onclick="backToValue('<%=Html.AttributeEncode(Url.Action("AjaxValueOperation")) %>',<%=Model.Id %>)"/>
           </div>
        </div>
       
   </div>