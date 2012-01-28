<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectWatcher.Models.Projects.UploadModel>" %>
 <link rel="Stylesheet" href="../../Content/ChooseFile.css" type="text/css" />
 
<form action="<%=Html.AttributeEncode(Url.Action("Import")) %>" enctype="multipart/form-data" method="post" style="margin-right: 10px;">
<div>
    <input type="file" name="uploadFile" size="20"/>
  
</div>
<div>
    <input type="submit" value="<%=Model.SubmitTitle %>" id="submitUpload"  class="button big blue" style="margin-top: 5px;"/>
    <span id="errorUploadMessage"><%=Html.Encode(Model.SuccessMessage) %></span>
</div>

</form>

