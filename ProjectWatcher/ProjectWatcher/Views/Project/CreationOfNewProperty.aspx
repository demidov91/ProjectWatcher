<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.PropertyModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>CreationOfNewProperty</title>
    <link rel="Stylesheet" href="../../Content/Button.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
</head>
<body>
<div class="centerDiv">
    <form  method="post" action="<%=Html.AttributeEncode(Url.Action("CreationOfNewPropertyClick", new{projectId = ViewData["projectId"]})) %>">
    <div style="padding-left:30%; padding-right:30%;">
    <table>
        <tr>
            <td class="tableLayoutLeftSide">
            <%=ViewData["nameTitle"] %>
            </td>
            <td class="tableLayoutRightSide">
                <%:Html.TextBoxFor(m => m.Name)%>
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
        <tr>
            <td>
                <%:Html.Encode(TempData["errorMessage"]) %>
            </td>
            <td style="text-align: right;">
                <input  name="accept" type="image" src="../../Resources/AcceptButton.png" />
                <input  name="reject" type="image" src="../../Resources/RejectButton.png" />
            </td>
        </tr>
    </table>
    </div>
    </form>
   </div>
</body>
</html>
