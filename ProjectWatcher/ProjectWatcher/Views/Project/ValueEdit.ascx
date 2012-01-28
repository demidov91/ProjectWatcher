<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<<<<<<< HEAD
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
           <input type="image" src="./../../Resources/AcceptButton.png" />
           <input type="image" src="./../../Resources/RejectButton.png" />
       </div>
   </div>
=======

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
>>>>>>> master
