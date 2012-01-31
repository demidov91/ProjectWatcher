<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.Index.ProjectWithValuesModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Project.Index" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeaderPlaceholder">
    
    <title>The Project</title>
    <link rel="Stylesheet" href="../../Content/ProjectIndex.css" type="text/css" />
    <script src="<%=Url.Content("~/Scripts/jquery-1.7.1.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/MicrosoftAjax.js") %>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/MicrosoftMvcAjax.js") %>"></script>
    <script type="text/javascript"src="<%=Url.Content("~/Scripts/Site.js")%>" ></script>
    
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ProjectWatcherPlaceholder">
<div id="ProjectWithProperties">
    <div id="forTitle">
        <span class="header"><%=Model.Name%> </span><span>-edited by <%=Model.LastUser%> at <%=Model.LastChanged%></span>
    </div>
    <div id="forProperties">
        <%foreach (ValueModel value in Model.Values)
          {
              if(value.IsVisible)
              Html.RenderPartial("BigValue", value);
          } %>
    </div>
    <div id="footer">
        <% if (Model.IsEditable)
           {%>
        <div id="forAddPropertyButton">
            <form method="post" action="<%=
                   Html.AttributeEncode(Url.Action("AddProperties", "Project", new {projectId = Model.ProjectId})) %>" >
                <input type="image" src="../../Resources/yellowPlus.png"  />
            </form>
        </div>
        <% } %>
        <div id="forBackButton" style="margin-left: 50%">
            <form method="post" action="<%=Html.AttributeEncode(Url.Action("Index", "Projects")) %>" >
                <input type="image" src="../../Resources/back.png" />
            </form>
        </div>
    </div>
</div>
</asp:Content>