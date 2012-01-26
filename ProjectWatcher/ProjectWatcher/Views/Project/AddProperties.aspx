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
    <div>
        <table class="centerCell">
            <tr>
                <td>
                    <%:Html.Encode(ViewData["projectProperties"]) %>                    
                </td>
                <td>
                    <%:Html.Encode(ViewData["availableProperties"]) %>                                        
                </td>
            <tr>
                <td>
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
                                <form method="post" action="<%=Html.AttributeEncode(Url.Action("DeleteProperty", new {projectId=Model.Id, propertyName=property.SystemName})) %>">
                                    <input type="image" src="../../Resources/DeleteButton.png" />   
                                </form>
                            </td>
                          </tr>
                        <%} %>
                    </table>                    
                </td>
                <td>
                    <table border="1">
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
                                <form method="post" action="<%=Html.AttributeEncode(Url.Action("AddExistingProperty", new {projectId=Model.Id, propertyName=property.SystemName})) %>">
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
                </td>
            </tr>
            <tr>
                <td class="leftButtomAlignCell">
                    <form method="post" action="<%=Html.AttributeEncode(Url.Action("CreationOfNewProperty", new {projectId=Model.Id})) %>">
                        <input type="submit" value="<%=ViewData["createPropertyTitle"] %>" />
                    </form>
                </td>   
                <td class="rightButtomAlignCell">
                    <form method="post" action="<%= Html.AttributeEncode(Url.Action("Index", new {projectId=Model.Id})) %>">
                        <input type="submit" value="<%=ViewData["backToProjectViewTitle"] %>" />
                    </form>
                </td>         
            </tr>

        </table>
        
    </div>
</body>
</html>
