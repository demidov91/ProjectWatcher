<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.ProjectModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Project" %>
<%@ Import Namespace="DAL" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>AddProperties</title>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
</head>
<body>
    <div id="AddPropertiesDiv">
        <div id="forProjectProperties">
            <div>
                <%:Html.Encode(ViewData["projectProperties"]) %>
            </div>
            <div>
        <table border="1">
            <tr>
                <td>
                    <%=ViewData["nameTitle"] %>
                </td>
                <td>
                    <%=ViewData["systemNameTitle"] %>
                </td>
                <td>
                    <%=ViewData["typeTitle"] %>
                </td>
                <td>
                    <%=ViewData["isImportantTitle"] %>
                </td>
                <td>
                    <%=ViewData["removeTitle"] %>
                </td>
            </tr>
            <%foreach (PropertyModel property in Model.ProjectProperties)
                { %>
                <tr>
                <td>
                    <%=property.Name %>
                </td>
                <td>
                    <%=property.SystemName %>
                </td>
                <td>
                    <%=property.Type %>
                </td>
                <td>
                    <%
                        Value projectvalue = Value.CreateValue(property.SystemName, Model.Id, true,
                                                        property.IsImportant, "");
                        Html.RenderPartial("ChangeImportance", projectvalue); %>

                </td>
                <td>
                    <form method="post" action="<%=Html.AttributeEncode(Url.Action("DeleteProperty", new {projectId=Model.Id, systemName=property.SystemName})) %>">
                        <input type="image" src="../../Resources/DeleteButton.png" />   
                    </form>
                </td>
                </tr>
            <%} %>
            </table>
            </div>
        </div>
        <div id="forOtherProperties">
            <div id="forOtherPropertiesTitle">
                <%:Html.Encode(ViewData["availableProperties"]) %>     
            </div>
            <div>
                    <table border="1" >
                        <tr>
                            <td>
                                <%=ViewData["addTitle"] %>
                            </td>
                            <td>
                                <%=ViewData["nameTitle"] %>
                            </td>
                            <td>
                                <%=ViewData["systemNameTitle"] %>
                            </td>
                            <td>
                                <%=ViewData["typeTitle"] %>
                            </td>                            
                        </tr>
                        <%foreach (PropertyModel property in Model.OtherProperties)
                          { %>
                          <tr>
                            <td>
                                <form method="post" action="<%=Html.AttributeEncode(Url.Action("AddExistingProperty", new {projectId=Model.Id, systemName=property.SystemName})) %>">
                                    <input type="image" src="../../Resources/LeftGreenArrow.png" />   
                                </form>
                            </td>
                            <td>
                                <%=property.Name %>
                            </td>
                            <td>
                                <%=property.SystemName %>
                            </td>
                            <td>
                                <%=property.Type %>
                            </td>                            
                          </tr>
                        <%} %>
                    </table>   
                    </div>             
                
    </div>
    <div id="forErrorMessage">
        <%:Html.Encode(TempData["errorMessage"]) %>
    </div>
    <div id="footer">
                <div id="forCreatePropertyButton">
                    <form method="post" action="<%=Html.AttributeEncode(Url.Action("CreationOfNewProperty", new {projectId=Model.Id})) %>">
                        <input type="submit" value="<%=ViewData["createPropertyTitle"] %>" />
                    </form>
                </div>   
                <div id="forGoToProjectButton">
                    <form method="post" action="<%= Html.AttributeEncode(Url.Action("Index", new {projectId=Model.Id})) %>">
                        <input type="submit" value="<%=ViewData["backToProjectViewTitle"] %>" />
                    </form>
                </div> 
    </div>
    </div>
</body>
<script src="<%=Url.Content("~/Scripts/jquery-1.5.1.js")%>" type="text/javascript"></script>
<script src="<%=Url.Content("~/Scripts/Site.js")%>" type="text/javascript"></script>
</html>
