<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.PropertyModel>" %>

<div class="centerDiv" id="centerDiv">
    <div>
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
            <td class="errordata">
                <%:Html.Encode(TempData["errorMessage"]) %>
            </td>
            <td style="text-align: right;">
                <input  name="accept" type="image" src="../../Resources/AcceptButton.png" onclick="xValue('<%=Html.AttributeEncode(Url.Action("CreationOfNewPropertyClick")) %>')"/>
            </td>
        </tr>
    </table>
    </div>
   </div>
   
   
    
