<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.PropertyModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>CreationOfNewProperty</title>
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
</head>
<body>
<div class="centerDiv">
    <form  method="post" action="<%=Html.AttributeEncode(Url.Action("CreationOfNewPropertyClick", new{projectId = ViewData["projectId"]})) %>">
    <table>
    <tr>
    <td>
    <table style="width:100%">
        <tr>
            <td class="tableLayoutLeftSide">
            <%=ViewData["nameTitle"] %>
            </td>
            <td class="tableLayoutRightSide">
                <%:Html.TextBoxFor(m => m.Name) %>
            </td>
        </tr>
        <tr>
            <td class="tableLayoutLeftSide">
                <%=ViewData["sysNameTitle"] %>
            </td>
            <td class="tableLayoutRightSide">
                <%:Html.TextBoxFor(m => m.SystemName) %>
            </td>
        </tr>
        <tr>
            <td class="tableLayoutLeftSide">
                <%=ViewData["typeTitle"] %>
            </td>
            <td class="tableLayoutRightSide">
                <%:Html.DropDownListFor(m => m.Type, new SelectList((SelectListItem[])ViewData["availableTypes"], "Value", "Text")) %>
            </td>
        </tr>
        <tr>
            <td class="tableLayoutLeftSide">
                <%=ViewData["availableValuesTitle"]%>
            </td>
            <td class="tableLayoutRightSide">
                <%:Html.TextAreaFor(m => m.AvailableValues) %>
            </td>
        </tr>
    </table>
    </td>
    </tr>
    <tr>
        <td>
            <input class="okCancelButton" name="reject" type="image" src="../../Resources/RejectButton.png" />
            <input class="okCancelButton" name="accept" type="image" src="../../Resources/AcceptButton.png" />
        </td>
    </tr>
    </table>
    </form>
   </div>
</body>
</html>
