<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.ProjectModel>" %>
<%@ Import Namespace="ProjectWatcher.Models.Project" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <script type="text/javascript" src="../../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.validationEngine-en.js"></script>
    <script type="text/javascript" src="../../Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="../../Scripts/MicrosoftMvcAjax.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.11.js"></script>
    <script src="<%=Url.Content("~/Scripts/Site.js")%>" type="text/javascript"></script>
    <title>AddProperties</title> 
    <link rel="stylesheet" href="../../Scripts/jQuerry/development-bundle/themes/start/jquery.ui.dialog.css"/>
    <link rel="stylesheet" href="../../Content/jquery-ui-1.8.17.custom.css"/>
    <link rel="stylesheet" href="../../Content/validationEngine.jquery.css" type="text/css"/>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/Table.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/Button.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/ChooseFile.css" type="text/css" />  
</head>

<body>
    <div id="AddPropertiesDiv">
            <div id="forProjectProperties">
                <div>
                    <%:Html.Encode(ViewData["projectProperties"]) %>
                </div>
                <div>
                    <table border="2">
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
                    Html.RenderPartial("ChangeImportance", property); 
                                %>
                               
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
                <div id="forCreatePropertyButton">
                   <%using (Ajax.BeginForm("CreationOfNewProperty", "Project", new { projectId = Model.Id }, new AjaxOptions { HttpMethod = "GET", UpdateTargetId = "result", InsertionMode = InsertionMode.Replace, OnSuccess = "openPopup" })){%>
                    <input type="submit" style="margin-top: 10px" class="big blue button" tabindex="Create new property" value="<%=(String)ViewData["createPropertyTitle"]%>"/>
                    <% } %>
                    </div>
            </div>
            <div id="forOtherProperties">
                <div id="forOtherPropertiesTitle">
                    <%:Html.Encode(ViewData["availableProperties"]) %>
                </div>
                <div>
                    <table border="2">
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
                <div id="forGoToProjectButton">
                    <form method="post" action="<%= Html.AttributeEncode(Url.Action("Index", new {projectId=Model.Id})) %>">
                    <input type="submit" value="<%=ViewData["backToProjectViewTitle"] %>" class="button big blue" style="margin-top: 10px" />
                    </form>
                </div>
            </div>
            <div style="clear: both">
       </div>
  </div>
  
  <div id="result"></div>
    <script type="text/javascript">
        function openPopup() {
            $("#result").dialog({
                modal: true,
                minWidth: 400,
                minHeight: 420,
                resizable: false,
                title: "Creation of new property",
                position: "center"
            });
            $(".textArea").css("width", $("#Name").width());
            $(document).ready(function () {
                $("#formCreationId").validationEngine();
            });
            $("#Type").change(function () {
                $("select option:selected").each(function () {
                    var str = $(this).text();
                    if (str == "Select" || str == "Multyselect") {
                        $("#rowValues").css({
                            display: "table-row",
                            visibility: "visible"
                        });
                    } else {
                        $("#rowValues").css({
                            display: "none",
                            visibility: "hidden"
                        });
                    }
                });
            });
        }
    </script>
    

    <script type="text/javascript">
        function aligment() {
            var clWidth = document.body.clientWidth;
            var div = document.getElementById('AddPropertiesDiv');
            var lOffset = (clWidth - div.clientWidth) / 2;
            div.style.left = lOffset + "px";
        }
        window.onload = aligment;
        window.onresize = aligment;
  </script>
         
</body>
</html>
