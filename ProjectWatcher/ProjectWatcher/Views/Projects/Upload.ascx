<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.UploadModel>" %>
<form action="<%=Html.AttributeEncode(Url.Action("Import")) %>" enctype="multipart/form-data" method="post">
<div>
    <input type="file" name="uploadFile" size="20" />
    
</div>
<div>
    <input type="submit" value="<%=Model.SubmitTitle %>" id="submitUpload"/>
    <span id="errorUploadMessage"><%=Html.Encode(Model.SuccessMessage) %></span>
</div>

</form>
