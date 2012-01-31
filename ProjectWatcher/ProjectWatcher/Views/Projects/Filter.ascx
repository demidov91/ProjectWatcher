<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.FilterModel>" %>


<div id="labelFilterBlock">
        <span class="leftSide">
            <%=Html.Encode(Model.LabelBeforeFilter) %>
        </span>
        <span class="leftSide">
            <%=Html.Encode(Model.Filter) %>
        </span>
        <input type="image" class="leftSide" src="../../Resources/pinion.png" alt="settings" onclick="$('#labelFilterBlock').css('display','none');$('#editFilterBlock').css('display','block');"/>    
</div>
<div id="editFilterBlock" style="display: none;">
    <form id="formFilterId" method="post" action="<%=Html.AttributeEncode(Url.Action("EditFilter", new{tableDefinition = Model.TableDefinition})) %>">
        <span class="leftSide">
            <%=Html.Encode(Model.LabelBeforeFilter) %>
        </span>
        <span class="leftSide">
            <%=Html.TextBoxFor(m => m.Filter, new{@class = "validate[required, custom[FilterRegExp]] filterTextBox"})%>
        </span>
        <input type="image" height="24px" width="24px" src="../../Resources/AcceptButton.png" name="accept" class="leftSide" />
    </form>
    <input type="image" height="24px" width="24px" src="../../Resources/RejectButton.png" name="reject" class="leftSide" onclick="$('#editFilterBlock').css('display','none'); $('#labelFilterBlock').css('display','block');" />
</div>
