<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>"%>

<!DOCTYPE html>


<html>
<head id="Head1" runat="server">

<style type="text/css">
    #BODY { font-family: Times New Roman; margin-left: 20px; }
    .header { font-family: Calibri; font-size: xx-large; }
    .textbox { font-family: Times New Roman; height: 48px; width: 400px; padding: 5px; border: solid 1px black;float: left;}
    .btn-slide { float: left }
    .section { margin: 20px; }
</style>

<title>The Project</title>
<script src="../../Scripts/jquery-1.5.1.js" type="text/javascript"></script>

<script type="text/javascript"> </script>



</head>
<body>
<div id="BODY">
<b class="header">THE PROJECT</b> 
<span style="color: Green; font-style: italic; margin-left: 50px;">-edited by (GetLastChange)</span>
<div style="position: static; margin-top: 20px; margin">
<div class="textbox">This will be some description...</div>
<div><img src="../../Content/pinion.png" class="btn-slide"/></div>
<div style=" clear: both;"></div>
</div>
<div class="section">Owner: (GetProjectOwner(ProjectId);)</div>
<div class="section">Platform</div>

</div>
</body>
</html>