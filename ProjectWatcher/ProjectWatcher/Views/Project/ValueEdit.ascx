<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>


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
