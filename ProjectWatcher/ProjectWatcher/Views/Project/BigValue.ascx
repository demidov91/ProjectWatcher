<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Project.Index.ValueModel>" %>

<div class="property" >
<<<<<<< HEAD
    <div class="forValueName">
        <p>
        <%:Html.Encode(Model.Name) %>
        </p>
    </div>
    <div class="forNameValueSeparator"><p>:</p></div>
    <div class="forBigValue">
        <%Html.RenderPartial("JustLookValue", Model); %>
    </div>
    <div style="clear: left;">
        </div>
=======
    <div class="valueName">
        <%:Html.Encode(Model.Name) %>
    </div>
    <%Html.RenderPartial("JustLookValue", Model); %>
>>>>>>> master
</div>