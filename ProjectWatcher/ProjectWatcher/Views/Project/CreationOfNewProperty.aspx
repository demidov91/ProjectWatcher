<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ProjectWatcher.Models.Project.PropertyModel>" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>CreationOfNewProperty</title>
    <link rel="stylesheet" href="../../Content/validationEngine.jquery.css" type="text/css"/>
    <script type="text/javascript" src="../../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.validationEngine-en.js"></script>
    <link rel="Stylesheet" href="../../Content/Button.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/Table.css" type="text/css" />
    <link rel="Stylesheet" href="../../Content/Site.css" type="text/css" />
    
</head>
<body>
<div class="centerDiv">
    <form id="formCreationId" method="post" action="<%=Html.AttributeEncode(Url.Action("CreationOfNewPropertyClick", new{ projectId = ViewData["projectId"]})) %>">
    <div style="padding-left:30%; padding-right:30%;">
    <table>
        <tr>
            <td class="tableLayoutLeftSide">
            <%=ViewData["nameTitle"] %>
            </td>
            <td class="tableLayoutRightSide">
                <%:Html.TextBoxFor(m => m.Name, new { @class = "validate[required, maxSize[100]]" })%>
            </td>
        </tr>
        <tr>
            <td class="tableLayoutLeftSide">
                <%=ViewData["sysNameTitle"] %>
            </td>
            <td class="tableLayoutRightSide">
                <%:Html.TextBoxFor(m => m.SystemName, new { @class = "validate[required,custom[SystemName]],maxSize[50]" })%>
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
       <tr id="rowValues" class="rowValues" style="display: none;visibility: hidden" >
            <td class="tableLayoutLeftSide">
                <%=ViewData["availableValuesTitle"]%>
            </td>
            <td class="tableLayoutRightSide">
                <%: Html.TextAreaFor(m => m.AvailableValues, new { @class = "textArea" })%> 
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
   
   
   
    
   <script type="text/javascript">
       $(document).ready(function () {
           $("#formCreationId").validationEngine();
       });
   </script>
   

    <script type="text/javascript">
        $("#Type").change(function () {
            $("select option:selected").each(function () {
                var str = $(this).text();
                if (str == "Select" || str == "Multyselect") {
                    $("#rowValues").css({
                        display: "table-row",
                        visibility: "visible"
                    });
                }
                else {
                    $("#rowValues").css({
                        display: "none",
                        visibility: "hidden"
                    });
                }
            });
        })
    </script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            var awidth = $("input #Name").attr("id");
            $("#rowValues").css("width", awidth);
        })
    </script>
    

</body>
</html>

